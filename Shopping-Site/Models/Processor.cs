using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Shopping_Site.Models.MyValidation;

namespace Shopping_Site.Models
{
    public class Processor
    {
        [Key]
        public int ProcessorID { get; set; } 

        [MinLength(3, ErrorMessage = "Model name cannot be less than 3"),
         MaxLength(150, ErrorMessage = "Model name cannot be more than 150")]
        public string Model { get; set; }

        [Range(1, 8, ErrorMessage ="The number of cores must be in the range 1 - 8")]
        [EvenNumberValidator]
        public int Cores_number { get; set; }

        [Range(1.0f, 3.0f, ErrorMessage = "The frequency must be in the range 1.0 GHz - 3.0 GHz")]
        public float Frequency { get; set; }

        // one-to-many relationship 
        public virtual ICollection<Phone> Phones { get; set; }
    }
}