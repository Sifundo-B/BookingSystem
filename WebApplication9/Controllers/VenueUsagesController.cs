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
    public class VenueUsagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VenueUsages
        public async Task<ActionResult> Index()
        {
            return View(await db.VenueUsages.ToListAsync());
        }

        // GET: VenueUsages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueUsage venueUsage = await db.VenueUsages.FindAsync(id);
            if (venueUsage == null)
            {
                return HttpNotFound();
            }
            return View(venueUsage);
        }

        // GET: VenueUsages/Create
        public ActionResult Create()
        {
            return View(new VenueUsage());
        }

        // POST: VenueUsages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,VenueId,GuestId,Hours,CheckIn,Date")] VenueUsage venueUsage)
        {
            if (venueUsage!=null)
            {
                venueUsage.Date = DateTime.Now;
            }
            if (ModelState.IsValid)
            {
                List<VenueUsage> usages = db.VenueUsages.ToList().Where(x => x.VenueId == venueUsage.VenueId).ToList();
                foreach (var item in usages)
                {
                    if((item.CheckIn>=venueUsage.CheckIn&&item.CheckIn<=venueUsage.CheckOut)||
                        item.CheckIn.AddHours(venueUsage.Hours) >= venueUsage.CheckIn)
                    {
                        ModelState.AddModelError("", "Venue is not available!");
                        return View(venueUsage);
                    }
                }
                db.VenueUsages.Add(venueUsage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(venueUsage);
        }

        // GET: VenueUsages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueUsage venueUsage = await db.VenueUsages.FindAsync(id);
            if (venueUsage == null)
            {
                return HttpNotFound();
            }
            return View(venueUsage);
        }

        // POST: VenueUsages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,VenueId,GuestId,Hours,CheckIn,Date")] VenueUsage venueUsage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venueUsage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(venueUsage);
        }
        public async Task<JsonResult>Venues(string VenueType)
        {
            if (!string.IsNullOrEmpty(VenueType))
            {
                List<Venue> venues = await db.Venues.ToListAsync();
                venues = venues.ToList().Where(c => c.VenueType.Trim().ToLower() ==
                          VenueType.Trim().ToLower()).ToList();
                return Json(new { Venues = venues });
            }
            return Json(new { Venues = new List<Venue>() });
        }
        // GET: VenueUsages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueUsage venueUsage = await db.VenueUsages.FindAsync(id);
            if (venueUsage == null)
            {
                return HttpNotFound();
            }
            return View(venueUsage);
        }

        // POST: VenueUsages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VenueUsage venueUsage = await db.VenueUsages.FindAsync(id);
            db.VenueUsages.Remove(venueUsage);
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
