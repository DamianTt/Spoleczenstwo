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
    public class UserController : BaseController
    {
        SIDb db = new SIDb();
        // GET: Users
        public ActionResult Index()
        {
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


            return RedirectToAction("Index" , "Manage");
        }
    }
}