using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shopping_Site.Models
{
    public class Battery
    {
        [Key]
        public int BatteryID { get; set; }

        [MinLength(2, ErrorMessage = "Battery name cannot be less than 2"),
        MaxLength(20, ErrorMessage = "Battery name cannot be more than 20")]
        public string Name { get; set; }

        [Range(600, 8000, ErrorMessage = "Capacity must be in the range 600 mAh - 8000 mAh")]
        public int Capacity { get; set; }


        //one-to-many relationship
        public virtual ICollection<Phone> Phones { get; set; }
    }
}