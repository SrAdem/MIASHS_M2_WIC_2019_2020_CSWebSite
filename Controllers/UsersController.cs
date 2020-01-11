using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using BazarDeLaHess.Models;

namespace BazarDeLaHess.Controllers
{
    public class UsersController : Controller
    {
        private BazarDeLaHessEntities _db = new BazarDeLaHessEntities();

        private const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
        + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
			    [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
        + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
			    [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
        + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

        // GET: Users
        public ActionResult Connection()
        {
            return View();
        }

        // POST : Connection
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (username.Equals("acc1") && password.Equals("123"))
            {
                Session["username"] = "username";
                return View("myAccount");
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Connection");
            }
        }

        // GET : To disconnect
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("username");
            return RedirectToAction("Connection");
        }

        //New Account
        public ActionResult NewAccount()
        {
            return View();
        }

        //Forgot password
        public ActionResult forgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult forgotPassword(string email)
        {
            if (isEmail(email))
            {
                ViewBag.notif = "Email envoyé à l'adresse : " + email;
            }
            else
            {
                ViewBag.notif = "Mauvais format d'email";
            }
            return View();
        }


        private bool isEmail (string email)
        {
            if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
            else return false;
        }
    }
}