using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Shopping_Site.Models;

namespace Shopping_Site.Models
{
    public class Camera
    {   
        [Key]
        public int CameraID { get; set; }
        [Range(2, 120, ErrorMessage = "Resolution must be in the range 2 MP - 120 MP")]
        public int Resolution { get; set; }
        
        [Required(ErrorMessage = "Choose one type of camera")]
        public string Type { get; set; }
        public bool Flash { get; set; }

        //many-to-many relationship
        public virtual ICollection<Feature> Features { get; set; }

        //many-to-many relationship
        public virtual ICollection<Phone> Phones { get; set; }

        [NotMapped]
        public List<CheckBoxViewModel> FeaturesList { get; set; }

        public enum TypeName
        {
            MainCamera = 1,
            FrontCamera =2
        }
    }
}