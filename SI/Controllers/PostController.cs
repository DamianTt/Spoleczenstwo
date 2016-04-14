using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SI.Models;
using System.Configuration;
using Microsoft.AspNet.Identity;

namespace SI.Controllers
{
    public class PostController : Controller
    {
        private SIDb db = new SIDb();

        // GET: Post
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }

        // GET: Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Post/Create
        [Authorize]
        public ActionResult Create()
        {
            var newPostViewModel = new NewPostViewModel
            {
                AllSections = new SelectList(db.Sections.ToList(), "Id", "Name"),
                SelectedSections = new List<int>()
            };
            
            return View(newPostViewModel);
        }

        // POST: Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Title, File, NSFW, SelectedSections")] NewPostViewModel newPost)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post
                {
                    Title = newPost.Title,
                    NSFW = newPost.NSFW,
                    AuthorId = User.Identity.GetUserId(),
                    Date = DateTime.Now,
                    Sections = new List<Section>()
            };

                var sections = db.Sections;
                foreach (var sectionId in newPost.SelectedSections)
                    post.Sections.Add(sections.Find(sectionId));

                int id = 0;
                try
                {
                    Post lastPost = db.Posts.OrderBy(p => p.Id).AsEnumerable().Last();
                    id = lastPost.Id + 1;
                }
                catch (InvalidOperationException) { }
                    
                post.ImgName = id.ToString() + "." + newPost.File.FileName.Split('.').Last();

                newPost.File.SaveAs(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["postImgsPath"]) + post.ImgName);

                db.Posts.Add(post);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = post.Id });
            }

            return View(newPost);
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            var editPostViewModel = new EditPostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                ImgName = post.ImgName,
                NSFW = post.NSFW,
                AllSections = new SelectList(db.Sections.ToList(), "Id", "Name"),
                SelectedSections = new List<int>()
            };
            foreach (var section in post.Sections)
                editPostViewModel.SelectedSections.Add(section.Id);

            return View(editPostViewModel);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Title, NSFW, SelectedSections")] EditPostViewModel editedPost)
        {
            if (ModelState.IsValid)
            {
                var post = db.Posts.Find(editedPost.Id);
                post.Title = editedPost.Title;
                post.NSFW = post.NSFW;

                post.Sections.Clear();
                var sections = db.Sections;
                foreach (var sectionId in editedPost.SelectedSections)
                    post.Sections.Add(sections.Find(sectionId));

                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = post.Id });
            }
            return View(editedPost);
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            string filePath = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["postImgsPath"]) + post.ImgName;
            System.IO.File.Delete(filePath);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
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
