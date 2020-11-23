using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication9.Models;

namespace WebApplication9.Controllers
{
    public class VenuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Venues
        public async Task<ActionResult> Index()
        {
            return View(await db.Venues.ToListAsync());
        }

        // GET: Venues/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = await db.Venues.FindAsync(id);
            if (venue == null)
            {
                return HttpNotFound();
            }
            return View(venue);
        }

        // GET: Venues/Create
        public ActionResult Create()
        {
            return View(new Venue());
        }

        // POST: Venues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Location,VenueType")] Venue venue)
        {
            if (venue != null)
            {
                venue.IsActive = true;
                venue.Date = DateTime.Now;
            }
            if (ModelState.IsValid)
            {
                db.Venues.Add(venue);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(venue);
        }

        // GET: Venues/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = await db.Venues.FindAsync(id);
            if (venue == null)
            {
                return HttpNotFound();
            }
            return View(venue);
        }

        // POST: Venues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Location,VenueType,IsActive,Date")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venue).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(venue);
        }

        // GET: Venues/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venue venue = await db.Venues.FindAsync(id);
            if (venue == null)
            {
                return HttpNotFound();
            }
            return View(venue);
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Venue venue = await db.Venues.FindAsync(id);
            db.Venues.Remove(venue);
            await db.SaveChangesAsync();
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
