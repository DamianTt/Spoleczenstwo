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

        public ActionResult Index()
        {
            var model = db.Posts.OrderByDescending(p => p.Date).ToList();
            return View(model);
        }


        [Authorize]
        public ActionResult Vote(string id, string submit)
        {
            var post = db.Posts.Find(id);
            var user = db.Users.Find(User.Identity.GetUserId());
            var model = db.Posts.OrderByDescending(p => p.Date).ToList();

            PostVote theVote = db.PostVotes.Find(user.Id, post.Id);

            if (theVote == null)
            {
                PostVote newVote = new PostVote();

                newVote.PostId = post.Id;
                newVote.Date = DateTime.Now;
                newVote.UserId = user.Id;
                newVote.IsUpvote = (submit == "UpVote");

                if (submit == "UpVote")
                    post.Score++;
                else
                    post.Score--;

                db.Entry(post).State = EntityState.Modified;
                db.PostVotes.Add(newVote);
                db.SaveChanges();
            }
            else
            {
                if(submit == "UpVote")
                {
                    if (theVote.IsUpvote)
                    {
                        return PartialView("_Score", post);
                    }
                    else
                    {
                        theVote.IsUpvote = true;
                        post.Score += 2;

                        db.Entry(theVote).State = EntityState.Modified;
                        db.Entry(post).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    if (!theVote.IsUpvote)
                    {
                        return PartialView("_Score", post);
                    }
                    else
                    {
                        theVote.IsUpvote = false;
                        post.Score -= 2;

                        db.Entry(theVote).State = EntityState.Modified;
                        db.Entry(post).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                
            }

            return PartialView("_Score", post);
        }


        // GET: <sectionName>
        public ActionResult Section(string sectionName)
        {
            Section section;
            try
            {
                section = db.Sections.Single(s => s.Name == sectionName);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }

            var posts = section.Posts.OrderByDescending(p => p.Date).ToList();

            if (posts.Count() < 1)
            {
                db.Sections.Remove(section);
                db.SaveChanges();
                return HttpNotFound();
            }

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
            var newPostViewModel = new NewPostViewModel();
            return View(newPostViewModel);
        }

        // POST: Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Title, File, NSFW, Tags")] NewPostViewModel newPost)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post
                {
                    Score = 0,
                    Title = newPost.Title,
                    NSFW = newPost.NSFW,
                    AuthorId = User.Identity.GetUserId(),
                    Date = DateTime.Now,
                    Sections = new List<Section>()
                };

                var tags = newPost.Tags.Split(',');

                var sections = db.Sections;
                foreach (string t in tags)
                {
                    var tag = FirstToUpper(t.Trim());
                    Section section;
                    try
                    {
                        section = db.Sections.Single(s => s.Name == tag);
                    }
                    catch (Exception)
                    {
                        section = new Section { Name = tag };
                        db.Sections.Add(section);
                    }
                    post.Sections.Add(section);
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

                //return RedirectToAction("Details", new { id = post.Id });
                return RedirectToAction("Index", "Home");
            }

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
                Tags = String.Join(", ", post.Sections.Select(s => s.Name))
            };

            return View(editPostViewModel);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Title, NSFW, Tags")] EditPostViewModel editedPost)
        {
            if (ModelState.IsValid)
            {
                var post = db.Posts.Find(editedPost.Id);
                post.Title = editedPost.Title;
                post.NSFW = post.NSFW;

                var tags = editedPost.Tags.Split(',');
                post.Sections.Clear();
                var sections = db.Sections;
                foreach (string t in tags)
                {
                    var tag = FirstToUpper(t.Trim());
                    Section section;
                    try
                    {
                        section = db.Sections.Single(s => s.Name == tag);
                    }
                    catch (Exception)
                    {
                        section = new Section { Name = tag };
                        db.Sections.Add(section);
                    }
                    post.Sections.Add(section);
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



        
        public static string FirstToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
    }
}
