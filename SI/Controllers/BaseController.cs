using SI.Models;
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
        }

        [HttpGet]
        public string GetAvatar()
        {
            string img = db.Users.Find(User.Identity.GetUserId()).AvatarName;
            return img;
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