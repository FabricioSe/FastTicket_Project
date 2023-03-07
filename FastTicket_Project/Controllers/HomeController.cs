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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(dataSource.myListCat);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}