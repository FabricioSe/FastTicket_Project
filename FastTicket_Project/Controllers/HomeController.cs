using FastTicket_Project.DataSource;
using FastTicket_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FastTicket_Project.Controllers
{
    public class HomeController : Controller
    {
        public Data dataSource = Data.GetInstance();

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var newEvents = _context.Events.OrderBy(e => e.CreatedAt).Take(4).ToList();
            var featuredEvents = _context.Events.Take(4).ToList();
            var popularEvents = _context.Events.OrderBy(e => e.Clicks).Take(4).ToList();

            ViewBag.New = newEvents;
            ViewBag.Featured = featuredEvents;
            ViewBag.Popular = popularEvents;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}