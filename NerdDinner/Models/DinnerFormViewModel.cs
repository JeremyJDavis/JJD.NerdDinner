using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace NerdDinner.Models
{
    public class DinnerFormViewModel
    {
        public int DinnerId { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public DateTime EventDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int CountryID { get; set; }
        public SelectList Countries { get; set; }

        public virtual ICollection<RSVP> RSVPs { get; set; } = new List<RSVP>();

        public bool IsUserRegistered(string userEmail)
        {
            return RSVPs.Any(r => r.AttendeeName.Equals(userEmail, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}