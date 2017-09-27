using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NerdDinner.Models
{
    public class RSVP
    {
        [Key]
        public int RsvpId { get; set; }
        public int DinnerId { get; set; }
        [Required]
        [StringLength(30)]
        public string AttendeeName { get; set; }
    }
}