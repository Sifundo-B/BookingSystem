using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class VenueUsage
    {
        public int Id { get; set; }
        public int VenueId { get; set; }
        public string GuestId { get; set; }
        public double Hours { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public virtual Venue Venue => new ApplicationDbContext().Venues.Find(VenueId);
        [NotMapped]
        public virtual ApplicationUser Guest => new ApplicationDbContext().Users.Find(GuestId);
        [NotMapped]
        public virtual string[] VenueTypes => new string[] { "Indoor", "OutDoor" };
        [NotMapped]
        public virtual List<ApplicationUser> Guests => getGuestUsers();
        private List<ApplicationUser> getGuestUsers()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<ApplicationUser> users = db.Users.ToList();
            List<ApplicationUser> newUsers = new List<ApplicationUser>();

            var role = db.Roles.Where(r => r.Name == "Guest").FirstOrDefault();
            foreach (var item in users)
            {
                if (item.Roles.FirstOrDefault().RoleId == role.Id)
                {
                    newUsers.Add(item);
                }
            }
            return newUsers;
        }
        [NotMapped]
        public virtual DateTime CheckOut => CheckIn.AddHours(Hours);
    }
}