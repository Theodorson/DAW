using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping_Site.Models
{
    public class Feature
    {   
        [Key]
        public int FeatureID { get; set; }

        [MinLength(2, ErrorMessage = "Feature name cannot be less than 2"),
        MaxLength(20, ErrorMessage = "Feature name cannot be more than 20")]
        public string Name { get; set; }

        //many-to-many relationship
        public virtual ICollection<Camera> Cameras { get; set; }
    }
}