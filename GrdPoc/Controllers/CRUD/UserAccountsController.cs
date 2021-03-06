﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GrdPoc.Models;
using GrdPoc.Models.Entities;

namespace GrdPoc.Controllers
{
    [Authorize]
    public class UserAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserAccounts
        public ActionResult Index()
        {
            var userAccounts = db.UserAccounts.Include(u => u.MunicipalEntity).Include(u => u.PersonaType);
            return View(userAccounts.ToList());
        }

        // GET: UserAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount userAccount = db.UserAccounts.Find(id);
            if (userAccount == null)
            {
                return HttpNotFound();
            }
            return View(userAccount);
        }

        // GET: UserAccounts/Create
        public ActionResult Create()
        {
            ViewBag.MunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName");
            ViewBag.PersonaTypeId = new SelectList(db.PersonaTypes, "PersonaTypeId", "PersonaTypeName");
            return View();
        }

        // POST: UserAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserAccountId,UserId,UserName,UserTitle,PersonaTypeId,MunicipalEntityId")] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                db.UserAccounts.Add(userAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName", userAccount.MunicipalEntityId);
            ViewBag.PersonaTypeId = new SelectList(db.PersonaTypes, "PersonaTypeId", "PersonaTypeName", userAccount.PersonaTypeId);
            return View(userAccount);
        }

        // GET: UserAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount userAccount = db.UserAccounts.Find(id);
            if (userAccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.MunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName", userAccount.MunicipalEntityId);
            ViewBag.PersonaTypeId = new SelectList(db.PersonaTypes, "PersonaTypeId", "PersonaTypeName", userAccount.PersonaTypeId);
            return View(userAccount);
        }

        // POST: UserAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserAccountId,UserId,UserName,UserTitle,PersonaTypeId,MunicipalEntityId")] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName", userAccount.MunicipalEntityId);
            ViewBag.PersonaTypeId = new SelectList(db.PersonaTypes, "PersonaTypeId", "PersonaTypeName", userAccount.PersonaTypeId);
            return View(userAccount);
        }

        // GET: UserAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount userAccount = db.UserAccounts.Find(id);
            if (userAccount == null)
            {
                return HttpNotFound();
            }
            return View(userAccount);
        }

        // POST: UserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAccount userAccount = db.UserAccounts.Find(id);
            db.UserAccounts.Remove(userAccount);
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
