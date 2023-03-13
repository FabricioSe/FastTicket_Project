using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastTicket_Project.DataSource;
using FastTicket_Project.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FastTicket_Project.Controllers
{
    public class BuyerController : Controller
    {
        private readonly Data dataSource = Data.GetInstance();

        // GET: /<controller>/
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult ForgotPwd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string pswd)
        {
            
            foreach (Buyer user in dataSource.myBuyers)
            {
                if (user.Email == email && user.Password == pswd)
                {
                    Data.buyerLogged = user;
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index");       
        }
    }
}

