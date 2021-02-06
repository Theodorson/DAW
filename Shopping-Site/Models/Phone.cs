using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping_Site.Models
{
    public class Phone
    {   
        [Key]
        public int PhoneID { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Phone name cannot be less than 5"),
        MaxLength(100, ErrorMessage = "Phone name cannot be more than 200")]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Brand cannot be less than 3"),
        MaxLength(100, ErrorMessage = "Brand cannot be more than 100")]
        public string Brand { get; set; }

    
        [Required]
        [Range(50f, 3000f, ErrorMessage = "The price must be in the range 50.0 $ - 3000.0 $")]
        public float Price { get; set; }

        [Required]
        [Range(1, 512, ErrorMessage = "The capacity must be in the range 1 gb - 512 gb")]
        public int Capacity { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "The memory size must be in the range 1 gb - 12 gb")]
        public int Memory { get; set; }

        [Required(ErrorMessage = "Choose one color")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Choose one type of phone")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Choose one type of SIM slots")]
        public string SIM_slots { get; set; }

        [Required(ErrorMessage = "Choose one type of operating system")]
        public string Operating_system { get; set; }


        [MinLength(5, ErrorMessage = "Description cannot be less than 5"),
        MaxLength(500, ErrorMessage = "Description cannot be more than 200")]
        public string Description { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Image link cannot be less than 5"),
        MaxLength(3000, ErrorMessage = "Image link cannot be more than 200")]
        public string Image { get; set; }

        [Required]
        public bool Stock { get; set; }


        //one-to-many relationship
        [Required(ErrorMessage = "Choose one processor")]
        [Column("ProcessorID")]
        public int ProcessorID { get; set; }

        public virtual Processor Processor { get; set; }


        //one-to-many relationship
        [Required(ErrorMessage = "Choose one battery")]
        [Column("BatteryID")]
        public int BatteryID { get; set; }

        public virtual Battery Battery { get; set; }


        //many-to-many relationship
        public virtual ICollection<Camera> Cameras { get; set; }

        [NotMapped]
        public List<CheckBoxViewModel> CamerasList { get; set; }

        // enums 
        public enum TypeName
        {
            Classic = 1,
            Smartphone = 2
        }

        public enum SimSlotsName
        {
            SingleSim = 1,
            DualSim = 2,
            HybridSim = 3
        }

        public enum OperatingSystemName 
        {
            Android = 1,
            IOS = 2,
            ThreadX = 3
        }

        public enum ColorName
        {
            Black = 1,
            White = 2,
            Red = 3, 
            Blue = 4,
            Yellow = 5
        }
    }


}