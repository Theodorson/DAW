using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping_Site.Models
{
    public class CartOrderCompletionBirthViewModel
    {
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

        [Required, RegularExpression(@"^[1-9](\d{3})$", ErrorMessage = "This is not a valid year!")]
        public int BirthYear { get; set; }

        [Required, RegularExpression(@"^(0[1-9])|(1[012])$", ErrorMessage = "This is not a valid month!")]
        public string BirthMonth { get; set; }

        [Required, RegularExpression(@"^((0[1-9])|([12]\d)|(3[01]))$", ErrorMessage = "This is not a valid day number!")]
        public string BirthDay { get; set; }
    }
}