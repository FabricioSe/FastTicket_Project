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
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        [Route("/tickets")]
        public IActionResult GetAll()
        {
            var ticketList = _context.Tickets.ToList();

            return View("Index", ticketList);
        }

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
    }
}

