using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NerdDinner.Models
{
    public class Country
    {
        [Key]
        public int CountryID { get; set; }
        public string Name { get; set; }
    }
}