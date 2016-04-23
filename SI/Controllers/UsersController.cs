using Microsoft.AspNet.Identity;
using SI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SI.Controllers
{
    public class UsersController : Controller
    {
        SIDb db = new SIDb();
        // GET: Users
        public ActionResult Index()
        {
            //var id = User.Identity.GetUserId();
            //string img = db.Users.Find(id).ToString();
            //ViewBag.ImgPath = HttpContext.Server.MapPath("~/Img/Avatars/") + img;
            //ViewBag.ImgPath = "av.jpg";
            return View();
        }

        public ActionResult SetAvatar()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SetAvatar(User model, HttpPostedFileBase file)
        {
            var id = User.Identity.GetUserId();

            string fileName = file.FileName;

            if (ModelState.IsValid)
            {
                model = db.Users.Find(id);
                model.AvatarName = fileName;
                file.SaveAs(HttpContext.Server.MapPath("~/Img/Avatars/") + fileName);
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }


            return RedirectToAction("SetAvatar", "Users");
        }

        [HttpGet]
        public string GetAvatar()
        {
            string img = db.Users.Find(User.Identity.GetUserId()).AvatarName;
            return img;
        }
    }
}