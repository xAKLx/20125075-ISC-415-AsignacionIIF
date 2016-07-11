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
            {
                var list = new List<string>();

                foreach (var item in userList.userList)
                {
                    if (item.Key != User.Identity.Name && item.Value.AddMinutes(5) > DateTime.Now)
                        list.Add(item.Key);
                }

                return PartialView("_Users", list);
            }

            return View(userList);
        }

        [HttpPost]
        public IActionResult Index(string type="", string userName= "", string message= "")
        {
            if(type == "sendMessage")
            {
                if (userName != null && userName.Length > 0)
                {
                    if (!userList.userMessages.ContainsKey(new Tuple<string, string>(User.Identity.Name, userName)))
                        userList.userMessages.Add(new Tuple<string, string>(User.Identity.Name, userName), new List<string>());

                    userList.userMessages[new Tuple<string, string>(User.Identity.Name, userName)].Add(message);
                    
                }

                return this.Json(string.Empty);
            }
                
            
            

            if (userList.userList.ContainsKey(User.Identity.Name))
                userList.userList[User.Identity.Name] = DateTime.Now;
            else
                userList.userList.Add(User.Identity.Name, DateTime.Now);

            if (AjaxValidator.IsAjaxRequest(Request))
            {
                if (userName != null && userName.Length != 0)
                {
                    if (!userList.userMessages.ContainsKey(new Tuple<string, string>(User.Identity.Name, userName)))
                        userList.userMessages.Add(new Tuple<string, string>(User.Identity.Name, userName), new List<string>());

                    KeyValuePair<Tuple<string, string>, List<String>> valuekey;

                    foreach (var item in userList.userMessages)
                        if (item.Key.Equals(new Tuple<string, string>(User.Identity.Name, userName)))
                            valuekey = item;
                    
                    return PartialView("_Chat", valuekey);
                }

                var list = new List<string>();

                foreach(var item in userList.userList)
                {
                    if (item.Key != User.Identity.Name && item.Value.AddMinutes(5) > DateTime.Now)
                        list.Add(item.Key);
                }
                
                return PartialView("_Users", list);
            }


            return View(userList);
        }

        //[HttpPost]
        //public IActionResult Index(string to = "", string message = "")
        //{
        //    if (!userList.userMessages.ContainsKey(new Tuple<string, string>(User.Identity.Name, to)))
        //        userList.userMessages.Add(new Tuple<string, string>(User.Identity.Name, to), new List<string>());

        //    userList.userMessages[new Tuple<string, string>(User.Identity.Name, to)].Add(message);

        //    return View();
        //}


        [HttpPost]
        public IActionResult Message(string to, string message)
        {
            if (!userList.userMessages.ContainsKey(new Tuple<string, string>(User.Identity.Name, to)))
                userList.userMessages.Add(new Tuple<string, string>(User.Identity.Name, to), new List<string>());

            userList.userMessages[new Tuple<string, string>(User.Identity.Name, to)].Add(message);

            return View();
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
