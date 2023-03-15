using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using FastTicket_Project.DataSource;
using FastTicket_Project.Models;
using FastTicket_Project.Models.DataTransferModels.Event;
using FastTicket_Project.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FastTicket_Project.Controllers
{
    [Route("event")]
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /event/{id}
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOne(int id)
        {
            var ev = _context.Events.Find(id);

            if (ev != null)
            {
                return View("Details", ev);
            }
            else
            {
                return BadRequest("Event does not exist");
            }
        }

        // GET: /events
        [HttpGet]
        [Route("/events")]
        public IActionResult GetAll(EventCategory? category, int? year, int? month, int? day, string? country, string? city)
        {
            var eventList = _context.Events
                .Where(e => category != null ? e.Category == category : true)
                .Where(e => year != null ? e.Time.Year == year : true)
                .Where(e => month != null ? e.Time.Month == month : true)
                .Where(e => day != null ? e.Time.Day == day : true)
                .Where(e => country != null ? e.Country == country : true)
                .Where(e => city != null ? e.City == city : true)
                .ToList();

            return View("Index", eventList);
        }

        // GET: /event/create
        [HttpGet]
        [Route("create")]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /event/create
        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<IActionResult> Create(CreateEventData data)
        {
            if (ModelState.IsValid)
            {
                Event e = new()
                {
                    Name = data.Name,
                    Description = data.Description,
                    Time = data.Time,
                    StreetNumber = data.StreetNumber,
                    StreetName = data.StreetName,
                    PostalCode = data.PostalCode,
                    City = data.City,
                    Country = data.Country,
                    Category = data.Category,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                };

                var eEntity = _context.Events.Add(e);

                try
                {
                    var res = await _context.SaveChangesAsync();

                    // TODO: Redirect to event page to see its infomation
                    return Redirect("/event/" + eEntity.Entity.ID);
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

        // GET: /event/{id}/update
        [HttpGet]
        [Route("/event/{id}/update")]
        [Authorize]
        public IActionResult Update(int id)
        {
            var ev = _context.Events.Find(id);

            if (ev == null)
                return BadRequest("Cannot find event with id: " + id);

            return View(ev);
        }

        // POST: /event/{id}/update
        [HttpPost]
        [Route("/event/{id}/update")]
        [Authorize]
        public IActionResult Update(int id, UpdateEventData data)
        {
            var ev = _context.Events.Find(id);

            if (ev == null)
                return BadRequest("Cannot find event with id: " + id);
           
            ev.Name = data.Name;
            ev.Description = data.Description;
            ev.Time = data.Time;
            ev.StreetNumber = data.StreetNumber;
            ev.StreetName = data.StreetName;
            ev.PostalCode = data.PostalCode;
            ev.City = data.City;
            ev.Country = data.Country;
            ev.Category = data.Category;
            ev.ModifiedAt = DateTime.Now;

            try
            {
                _context.SaveChanges();

                return Redirect("/event/" + id);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}

