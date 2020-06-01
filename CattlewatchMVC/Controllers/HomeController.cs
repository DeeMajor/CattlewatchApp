﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CattlewatchMVC.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult AdminHome()
        {
            return View();
        }

        public ActionResult LoggedIn()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}