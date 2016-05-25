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
    public class PostController : BaseController
    {
        private SIDb db = new SIDb();

        // GET: Post
        public ActionResult Index()
        {
            var model = db.Posts.OrderBy(p => p.Date).ToList();

            return View(model);
        }

        // GET: <sectionName>
        public ActionResult Section(string sectionName)
        {
            var sections = db.Sections.Where(s => s.Name == sectionName).ToList();

            if (sections.Count == 0)
                return HttpNotFound();

            var posts = sections[0].Posts.OrderBy(p => p.Date).ToList();

            return View("index", posts);
        }

        // GET: Post/Details/5
        public ActionResult Details(string id)
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

                if (newPost.SelectedSections != null)
                {
                    var sections = db.Sections;
                    foreach (var sectionId in newPost.SelectedSections)
                        post.Sections.Add(sections.Find(sectionId));
                }
                    
                byte[] buf = new byte[6];
                Random rand = new Random();

                db.Posts.Add(post);

                //generate random 42-bit ID (7 base64 characters) until unique
                do
                {
                    rand.NextBytes(buf);
                    post.Id = Convert.ToBase64String(buf).Substring(0, 7);
                    post.ImgName = post.Id + "." + newPost.File.FileName.Split('.').Last();
                }
                while (!db.TrySaveChanges());

                newPost.File.SaveAs(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["postImgsPath"]) + post.ImgName);

                return RedirectToAction("Details", new { id = post.Id });
            }

            newPost.AllSections = new SelectList(db.Sections.ToList(), "Id", "Name");
            return View(newPost);
        }

        // GET: Post/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Post post = db.Posts.Find(id);
            if (post == null)
                return HttpNotFound();

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

                if (editedPost.SelectedSections != null)
                {
                    foreach (var sectionId in editedPost.SelectedSections)
                        post.Sections.Add(sections.Find(sectionId));
                }

                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = post.Id });
            }
            return View(editedPost);
        }

        // GET: Post/Delete/5
        public ActionResult Delete(string id)
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
        public ActionResult DeleteConfirmed(string id)
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
