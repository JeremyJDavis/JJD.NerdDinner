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
        NerdDinnersDBContext nerdDinnerDB = new NerdDinnersDBContext();

        public Dinner Dinner{ get; set; }
        public DropDownList Countries { get; set; }

        public DinnerFormViewModel(Dinner dinner)
        {
            Dinner = dinner;
            Countries = new DropDownList();
        }
    }
}