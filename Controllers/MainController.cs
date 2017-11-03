
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
        [Route("bright_ideas")]
        public IActionResult bright_ideas()
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return RedirectToAction("Index", "LogReg");
            }
            //Get logged user
            int user_id = (int)HttpContext.Session.GetInt32("user_id");
            User loggedUser = _context.users.SingleOrDefault(u => u.id == user_id);
            ViewBag.loggedUser = loggedUser;

            //Get all ideas
            List<Idea> ideas = _context.ideas.Include(i => i.user).Include(i => i.likes).ThenInclude(l => l.user).OrderByDescending(i => i.likes.Count).ToList();
            ViewBag.ideas = ideas;

            return View();
        }

        [HttpGet]
        [Route("Logout")]

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "LogReg");
        }

        [HttpGet]
        [Route("bright_ideas/{id}")]

        public IActionResult Idea(int id)
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return RedirectToAction("Index", "LogReg");
            }

            //Get idea with creator and likers and send down to view bag
            Idea idea = _context.ideas.Include(i => i.user).Include(i => i.likes).ThenInclude(l => l.user).SingleOrDefault(i => i.id == id);
            ViewBag.idea = idea;
            return View();
        } 

        [HttpGet]
        [Route("users/{id}")]

        public IActionResult Users(int id)
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return RedirectToAction("Index", "LogReg");
            }
            return View();
        }

        [HttpPost]
        [Route("NewIdea")]

        public IActionResult NewIdea(IdeaViewModel model)
        {
            int user_id = (int)HttpContext.Session.GetInt32("user_id");
            User user = _context.users.SingleOrDefault(u => u.id == user_id);
            if (ModelState.IsValid)
            {
                Idea idea = new Idea() {
                    body = model.body,
                    users_id = user.id,
                    user = user,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };

                _context.ideas.Add(idea);
                _context.SaveChanges();

                return RedirectToAction("bright_ideas");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Route("Like/{id}")]

        public IActionResult Like(int id)
        {
            //get user
            int user_id = (int)HttpContext.Session.GetInt32("user_id");
            User user = _context.users.SingleOrDefault(u => u.id == user_id);

            //get idea
            Idea idea = _context.ideas.SingleOrDefault(i => i.id == id);

            //create like and add to db
            Like like = new Like() {
                users_id = user.id,
                user = user,
                ideas_id = idea.id,
                idea = idea,
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            };

            _context.likes.Add(like);
            _context.SaveChanges();
            
            return RedirectToAction("bright_ideas");
        }

        [HttpGet]
        [Route("Delete/{id}")]

        public IActionResult Delete(int id)
        {
            Idea idea = _context.ideas.SingleOrDefault(i => i.id == id);
            _context.ideas.Remove(idea);
            _context.SaveChanges();

            return RedirectToAction("bright_ideas");
        }
    }
}