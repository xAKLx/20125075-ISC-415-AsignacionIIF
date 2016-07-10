using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using _20125075_ISC_415_AsignacionIIF.Data;
using _20125075_ISC_415_AsignacionIIF.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace _20125075_ISC_415_AsignacionIIF.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        Users userList = Users.getUniqueInstance();

        

        public IActionResult Index()
        {
            if (userList.userList.ContainsKey(User.Identity.Name))
                userList.userList[User.Identity.Name] = DateTime.Now;
            else
                userList.userList.Add(User.Identity.Name, DateTime.Now);

            if (AjaxValidator.IsAjaxRequest(Request))
                return PartialView("_Users", userList);
            
            return View(userList);
        }

        [HttpPost]
        public IActionResult Index(string message= "")
        {
            if (userList.userList.ContainsKey(User.Identity.Name))
                userList.userList[User.Identity.Name] = DateTime.Now;
            else
                userList.userList.Add(User.Identity.Name, DateTime.Now);

            if (AjaxValidator.IsAjaxRequest(Request))
                return PartialView("_Users", userList);

            return View(userList);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
