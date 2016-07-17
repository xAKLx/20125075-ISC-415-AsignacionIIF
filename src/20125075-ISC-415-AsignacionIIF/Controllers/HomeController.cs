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
using ClassLibrary1;

namespace _20125075_ISC_415_AsignacionIIF.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        Users userList = Users.getUniqueInstance();
        MessageContext db = null;

        private IHostingEnvironment hostingEnv;

        public HomeController(IHostingEnvironment env, MessageContext db)
        {
            this.hostingEnv = env;
            this.db = db;
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
        public IActionResult Index(string type = "", string userName = "", string message = "")
        {
            if (type == "sendMessage")
            {
                if (userName != null && userName.Length > 0)
                {

                    userList.addMessage(User.Identity.Name, userName, message, db);
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
                    return PartialView("_Chat", userList.getOrderedMessages(User.Identity.Name, userName, db));
                }

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

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
