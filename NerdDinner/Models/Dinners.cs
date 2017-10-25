using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace NerdDinner.Models
{
    [Bind(Include = "Title, EventDate, ContactEmail, ContactPhone, Address, CountryID, Country, RSVPs, Latitude, Longitude")]
    public class Dinner
    {
        [Key]
        public int DinnerId { get; set; }

        [StringLength(60, ErrorMessage = "Make a shorter title")]
        [Required(ErrorMessage = "Name your dinner")]
        public string Title { get; set; }

        [Required(ErrorMessage = "When is your dinner?")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Need email please")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address")]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "Enter a phone number please")]
        [StringLength(20, ErrorMessage = "Phone numbers are shorter than that...")]
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "Where are we eating?")]
        [StringLength(50, ErrorMessage = "Can you abreviate that?")]
        public string Address { get; set; }

        //[Required(ErrorMessage = "What country is this?")]
        //[DisplayName("Country")]
        public IEnumerable<SelectListItem> CountryID { get; set; }

        public virtual Country Country { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public virtual ICollection<RSVP> RSVPs { get; set; } = new List<RSVP>();

        public bool IsHostedBy(string userEmail)
        {
            return ContactEmail.Equals(userEmail, StringComparison.InvariantCultureIgnoreCase);
        }

        public bool IsUserRegistered(string userEmail)
        {
            return RSVPs.Any(r => r.AttendeeName.Equals(userEmail, StringComparison.InvariantCultureIgnoreCase));
        }
    }

    
}