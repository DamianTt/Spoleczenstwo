﻿using SI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace SI.Controllers
{
    public class BaseController : Controller
    {
        private SIDb db = new SIDb();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["Sections"] = db.Sections.ToList();
            var user = db.Users.Find(User.Identity.GetUserId());
            if (user != null)
                ViewBag.nsfw = user.AllowNSFW;
            else
                ViewBag.nsfw = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}