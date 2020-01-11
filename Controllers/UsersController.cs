﻿using System;
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
        private readonly BazarDeLaHessEntities _db = new BazarDeLaHessEntities();

        private const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
        + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
			    [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
        + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
			    [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
        + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

        // GET: Users
        public ActionResult Connection(int? itemID)
        {
            if(Session["userid"] != null)
            {
                int id = (int)Session["userid"];
                Users user = (from i in _db.Users
                              where i.id_user == id
                              select i).First();
                ViewBag.account = user;
                return View("myAccount");
            }
            ViewBag.itemID = itemID;
            return View();
        }

        // POST : Connection
        [HttpPost]
        public ActionResult Login(Users userConnection)
        {
            Users user = null;
            if (userConnection.email != null && userConnection.pass_word != null)
            {
                user = (from i in _db.Users
                        where i.email == userConnection.email
                        where i.pass_word == userConnection.pass_word
                        select i).First();
            }

            if (user != null)
            {
                Session["userid"] = user.id_user;
                Session["userName"] = user.first_name;
                Session["userSurname"] = user.last_name;
                if( Request.Form["itemID"] != null)
                {
                    string itemId = Request.Form["itemID"];
                    return RedirectToAction("Buy","Cart", new { id = Int32.Parse(itemId) } );
                }
                else
                {
                    ViewBag.account = user;
                    return View("myAccount");
                }
            }
            else
            {
                ViewBag.itemID = Request.Form["itemID"];
                ViewBag.notif = "email ou mot de passe incorrecte";
                return View("Connection");
            }
        }

        // GET : To disconnect
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("userName");
            Session.Remove("userSurname");
            Session.Remove("userid");
            return RedirectToAction("Connection");
        }

        //New Account
        public ActionResult NewAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewAccount(Users newUser)
        {
            ModelState.Remove("id_user"); // This will remove the key 

            if (ModelState.IsValid)
            {
                ViewBag.account = newUser;
                _db.Users.Add(newUser);
                //On sauvgarde
                _db.SaveChanges();
                return View("MyAccount");
            }
            else
            {
                return View("NewAccount");
            }
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