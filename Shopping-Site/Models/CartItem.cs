using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shopping_Site.Models
{
    public class CartItem
    {

   
        public Phone Product { get; set; }
        public int Quantity { get; set; }




    }
}