using Microsoft.AspNet.Identity;
using SI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SI.Controllers
{
    public class BaseController : Controller
    {
        private SIDb db = new SIDb();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["Sections"] = db.Sections.ToList();

            // theme
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);

                if (user.ThemeDark)
                    ViewBag.Style = "dark";
                else
                    ViewBag.Style = "";
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult uptadeTheme()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);
                user.ThemeDark = true;
            }
            else
            {

            }    
            return null;
            
        }
    }
}