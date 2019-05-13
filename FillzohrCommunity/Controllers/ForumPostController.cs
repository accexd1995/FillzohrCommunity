using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FillzohrCommunity.Models;

namespace FillzohrCommunity.Controllers
{
    public class ForumPostController : Controller
    {
        private FillzohrDbEntities db = new FillzohrDbEntities();

        // GET: ForumPost
        public ActionResult Index()
        {
            var forumPost = db.ForumPost.Include(f => f.User);
            return View(forumPost.ToList());
        }

        // GET: ForumPost/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumPost forumPost = db.ForumPost.Find(id);
            if (forumPost == null)
            {
                return HttpNotFound();
            }
            return View(forumPost);
        }

        // GET: ForumPost/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.User, "UserId", "Username");
            return View();
        }

        // POST: ForumPost/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ForumPostId,PostText,UserId")] ForumPost forumPost)
        {
            if (ModelState.IsValid)
            {
                forumPost.UserId = (int)Session["userId"];
                db.ForumPost.Add(forumPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.User, "UserId", "Username", forumPost.UserId);
            return View(forumPost);
        }

        // GET: ForumPost/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumPost forumPost = db.ForumPost.Find(id);
            if (forumPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.User, "UserId", "Username", forumPost.UserId);
            return View(forumPost);
        }

        // POST: ForumPost/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ForumPostId,PostText,UserId")] ForumPost forumPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forumPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.User, "UserId", "Username", forumPost.UserId);
            return View(forumPost);
        }

        // GET: ForumPost/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumPost forumPost = db.ForumPost.Find(id);
            if (forumPost == null)
            {
                return HttpNotFound();
            }
            return View(forumPost);
        }

        // POST: ForumPost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ForumPost forumPost = db.ForumPost.Find(id);
            db.ForumPost.Remove(forumPost);
            db.SaveChanges();
            return RedirectToAction("Index");
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
