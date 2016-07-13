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
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using System.IO;

namespace _20125075_ISC_415_AsignacionIIF.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        Users userList = Users.getUniqueInstance();

        private IHostingEnvironment hostingEnv;

        public HomeController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }

        public IActionResult UploadFiles()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult UploadFiles(IList<IFormFile> files)
        {
            long size = 0;
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                size += file.Length;

                var finalFileName = hostingEnv.WebRootPath + $@"\{filename}";
                for (int count = 1; System.IO.File.Exists(finalFileName); count++)
                    finalFileName = hostingEnv.WebRootPath + $@"\{count.ToString() + filename}";

                using (FileStream fs = System.IO.File.Create(finalFileName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            ViewBag.Message = $"{files.Count} file(s) / { size} bytes uploaded successfully!";
            return View();
        }

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
                    bool added = false;

                    foreach (var item in userList.userMessages)
                    {
                        if (item.Key.Equals(new Tuple<string, string>(userName, User.Identity.Name)))
                        {
                            userList.userMessages[new Tuple<string, string>(userName, User.Identity.Name)].Add(new Message(userName, User.Identity.Name, message));
                            added = true;
                        }
                        else if(item.Key.Equals(new Tuple<string, string>(User.Identity.Name, userName)))
                        {
                            userList.userMessages[new Tuple<string, string>(User.Identity.Name, userName)].Add(new Message(userName, User.Identity.Name, message));
                            added = true;
                        }
                    }

                    if (!added)
                    {
                        userList.userMessages.Add(new Tuple<string, string>(User.Identity.Name, userName), new List<Message>());
                        userList.userMessages[new Tuple<string, string>(User.Identity.Name, userName)].Add(new Message(userName, User.Identity.Name, message));
                    }
                }

                return this.Json(string.Empty);
            }
                
            
            

            if (userList.userList.ContainsKey(User.Identity.Name))
                userList.userList[User.Identity.Name] = DateTime.Now;
            else
                userList.userList.Add(User.Identity.Name, DateTime.Now);

            if (!userList.userImages.ContainsKey(User.Identity.Name))
                userList.userImages.Add(User.Identity.Name, "/images/super_smash_bros_3ds_wii_u_03.jpg");

            if (AjaxValidator.IsAjaxRequest(Request))
            {
                if (userName != null && userName.Length != 0)
                {
                    Nullable<KeyValuePair<Tuple<string, string>, List<Message>>> messageList = null;

                    if( !userList.userMessages.ContainsKey(new Tuple<string,string>(User.Identity.Name, userName)) && !userList.userMessages.ContainsKey(new Tuple<string, string>(userName, User.Identity.Name)))
                    {
                        userList.userMessages.Add(new Tuple<string, string>(User.Identity.Name, userName), new List<Message>());
                    }

                    foreach (var item in userList.userMessages)
                        if (item.Key.Equals(new Tuple<string, string>(User.Identity.Name, userName)) || item.Key.Equals(new Tuple<string, string>(userName, User.Identity.Name)))
                        {
                            messageList = item;
                            break;
                        }


                    return PartialView("_Chat", messageList.Value.Value);
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

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}
