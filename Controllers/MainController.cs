
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GoldDuck.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GoldDuck.Controllers
{
    public class MainController : Controller
    {
        private GoldDuckContext _context;
        public MainController(GoldDuckContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Main")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("user_id") == null) //REMEMBER TO PUT THIS ON ALL OF YOUR PAGES!!!!!
            {
                return RedirectToAction("Index", "LogReg");
            }
            return View();
        }

        [HttpGet]
        [Route("Logout")]

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "LogReg");
        } 
    }
}