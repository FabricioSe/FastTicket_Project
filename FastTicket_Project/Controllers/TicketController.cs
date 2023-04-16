using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using FastTicket_Project.DataSource;
using FastTicket_Project.Models;
using FastTicket_Project.Models.DataTransferModels.Event;
using FastTicket_Project.Models.DataTransferModels.Ticket;
using FastTicket_Project.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FastTicket_Project.Controllers
{
    [Route("tickets")]
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TicketController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /tickets/checkout/success
        [HttpGet]
        //[Authorize]
        [Route("checkout/success")]
        public async Task<IActionResult> CheckoutSuccess(string session_id)
        {
            var sessionService = new SessionService();
            var session = sessionService.Get(session_id);

            if (session.Status == "complete")
            {
                var buyerId = session.Metadata["BuyerId"];
                var ticketId = session.Metadata["TicketId"];

                // get the ticket
                var ticket = await _context.Tickets.FindAsync(Convert.ToInt32(ticketId));

                // change owner and toggle on sale
                ticket!.UserID = buyerId;
                ticket!.OnSale = false;

                await _context.SaveChangesAsync();
            }

            return View("CheckoutSuccess");
        }

        // GET: /tickets/my
        [Authorize]
        [HttpGet]
        [Route("my")]
        public async Task<IActionResult> GetMyTickets()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var tickets = _context.Tickets.Where(t => t.UserID == currentUser!.Id);

            return View("Index", tickets);
        }

        // GET: /tickets/{id}/checkout
        [Authorize]
        [HttpGet]
        [Route("{id}/checkout")]
        public async Task<IActionResult> Checkout(int id)
        {
            // check if ticket exists and is available for purchase
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null || ticket.OnSale == false)
                return BadRequest("Ticket does not exist or is not on sale");

            // TODO: set on sale to false to lock the ticket

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (ticket.UserID == currentUser.Id)
                return BadRequest("Cannot buy own ticket");

            // create product
            var productOptions = new ProductCreateOptions
            {
                Name = ticket.ID.ToString(),
            };
            var productService = new ProductService();
            var product = productService.Create(productOptions);


            // create price
            var priceOptions = new PriceCreateOptions
            {
                UnitAmount = ((long)ticket.Price) * 100,
                Currency = "cad",
                Product = product.Id
            };
            var priceService = new PriceService();
            var price = priceService.Create(priceOptions);

            var baseUri = $"{Request.Scheme}://{Request.Host}";

            var sessionOptions = new SessionCreateOptions
            {
                SuccessUrl = baseUri + "/tickets/checkout/success?session_id={CHECKOUT_SESSION_ID}",

                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = price.Id,
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                Metadata = new Dictionary<string, string>()
                {
                    { "BuyerId", currentUser!.Id },
                    { "TicketId", ticket.ID.ToString() }
                }
            };

            var sessionService = new SessionService();
            var session = sessionService.Create(sessionOptions);

            return Redirect(session.Url);
        }

        // GET: /tickets/{id}
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOne(int id)
        {
            var ticket = _context.Tickets.Find(id);

            if (ticket != null)
            {
                return View("Details", ticket);
            }
            else
            {
                return BadRequest("Ticket does not exist");
            }
        }

        // GET: /tickets
        //[HttpGet]
        //[Route("/tickets")]
        //public IActionResult GetAll()
        //{
        //    var ticketList = _context.Tickets.ToList();

        //    return View("Index", ticketList);
        //}

        // GET: /tickets/create
        [HttpGet]
        [Route("create")]
        [Authorize]
        public IActionResult Create(int? eventId)
        {
            if (eventId == null)
                return BadRequest("Event param is required");

            var ev = _context.Events.Find(eventId);

            if (ev == null)
                return BadRequest("Event with this id does not exist");

            ViewBag.EventName = ev.Name;
            return View();
        }

        // POST: /tickets/create
        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<IActionResult> Create(CreateTicketData data, int? eventId)
        {
            if (eventId == null)
                return BadRequest("Event param is required");

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (currentUser == null)
                return BadRequest("Cannot find current user");

            if (ModelState.IsValid)
            {
                Ticket ticket = new()
                {
                    EventID = (int)eventId,
                    UserID = currentUser!.Id,
                    Price = data.Price,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    OnSale = data.OnSale
                };

                var ticketEntity = _context.Tickets.Add(ticket);

                try
                {
                    var res = await _context.SaveChangesAsync();

                    // TODO: Redirect to ticket page to see its infomation
                    return Redirect("/tickets/" + ticketEntity.Entity.ID);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.InnerException.Message);
                }
            }
            else
            {
                return View();
            }
        }

        // GET: /tickets/{id}/update
        [HttpGet]
        [Route("/tickets/{id}/update")]
        [Authorize]
        public IActionResult Update(int id)
        {
            var ticket = _context.Tickets.Find(id);

            if (ticket == null)
                return BadRequest("Cannot find ticket with id: " + id);

            return View(ticket);
        }

        // POST: /tickets/{id}/update
        [HttpPost]
        [Route("/tickets/{id}/update")]
        [Authorize]
        public IActionResult Update(int id, UpdateTicketData data)
        {
            var ticket = _context.Tickets.Find(id);

            if (ticket == null)
                return BadRequest("Cannot find ticket with id: " + id);

            // check if event id and user id are valid
            var evento = _context.Events.Find(data.EventId);

            if (evento == null)
                return BadRequest("Invalid event id: " + data.EventId);

            var user = _context.Users.Find(data.UserId);

            if (user == null)
                return BadRequest("Invalid user id: " + data.UserId);

            ticket.Price = data.Price;
            ticket.OnSale = data.OnSale;
            ticket.EventID = data.EventId;
            ticket.UserID = data.UserId;
            ticket.ModifiedAt = DateTime.Now;

            try
            {
                _context.SaveChanges();

                return Redirect("/events/" + id);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Route("test")]
        public IActionResult testCheckout()
        {
            return View("CheckoutSuccess");
        }
    }
}

