using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        [Display(Name="Venue Type")]
        public string VenueType { get;set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public virtual string[] VenueTypes => new string[] { "Indoor", "OutDoor" };
    }
}