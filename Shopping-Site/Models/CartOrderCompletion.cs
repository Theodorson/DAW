using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping_Site.Models
{
    public class CartOrderCompletion
    {   
        [Key]
        public int CartOrderCompletionID { get; set; }

        [Required]
        [RegularExpression("^([a-zA-Z]{2,}\\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\\s?([a-zA-Z]{1,})?)", ErrorMessage = "This is not a valid first name!")]
        public string First_name { get; set; }

        [RegularExpression("^([a-zA-Z]{3,})", ErrorMessage = "This is not a valid last name!")]
        [Required]
        public string Last_name { get; set; }
        
        [Required]
        [RegularExpression(@"^07(\d{8})$", ErrorMessage = "This is not a valid phone number!")]
        public string Phone_number { get; set; }

        [Required(ErrorMessage = "Choose one region")]
        public string Region { get; set; }

        [Required]
        [RegularExpression("^([a-zA-Z]{3,})", ErrorMessage = "This is not a valid city name!")]
        public string City { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Address cannot be less than 5"),
        MaxLength(100, ErrorMessage = "Address cannot be more than 100")]
        public string Address { get; set; }

        public string Data { get; set; }

        public string Products { get; set; }


        [Required]
        public virtual Birth Birth { get; set; }

        public enum RegionName
        {
            Alba = 1,
            Arad = 2,
            Arges = 3,
            Bacau = 4,
            Bihor = 5,
            Bistrita_Nasaud = 6,
            Botosani = 7,
            Brasov = 8,
            Braila = 9,
            Bucuresti = 10,
            Buzau = 11,
            Caras_Severin = 12,
            Calarasi = 13,
            Cluj = 14,
            Constanta = 15,
            Covasna = 16,
            Dambovita = 17,
            Dolj = 18,
            Galati = 19,
            Giurgiu = 20,
            Gorj = 21,
            Harghita = 22,
            Hunedoara = 23,
            Ialomita = 24,
            Iasi = 25,
            Ilfov = 26,
            Maramures = 27,
            Mehedinti = 28,
            Mures = 29,
            Neamt = 30,
            Olt = 31,
            Prahova = 32,
            Satu_Mare = 33,
            Salaj = 34,
            Sibiu = 35,
            Suceava = 36,
            Teleorman = 37,
            Timis = 38,
            Tulcea = 39,
            Vaslui = 40,
            Valcea = 41,
            Vrancea = 42
        }

    }
}