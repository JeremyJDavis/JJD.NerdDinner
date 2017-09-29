using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;

namespace NerdDinner.Models
{
    public class Dinner
    {
        [Key]
        public int DinnerId { get; set; }
        [StringLength(50, ErrorMessage = "Make a shorter title")]
        [Required(ErrorMessage = "Name your dinner")]
        public string Title { get; set; }
        [Required(ErrorMessage = "When is your dinner?")]
        public DateTime EventDate { get; set; }
        [Required(ErrorMessage = "What's all this then?")]
        [StringLength(256)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Who's in charge here?")]
        [StringLength(20, ErrorMessage = "Surely the name can be shorter...")]
        public string HostedBy { get; set; }
        [Required(ErrorMessage = "Enter a phone number please")]
        [StringLength(20, ErrorMessage = "Phone numbers are shorter than that...")]
        public string ContactPhone { get; set; }
        [Required(ErrorMessage = "Where are we eating?")]
        [StringLength(50, ErrorMessage = "Can you abreviate that?")]
        public string Address { get; set; }
        [Required]
        [StringLength(30)]
        public string Country { get; set; }
        [Required]
        public float Latitude { get; set; }
        [Required]
        public float Longitude { get; set; }
        public virtual ICollection<RSVP> RSVPs { get; set; }

    }

    
}