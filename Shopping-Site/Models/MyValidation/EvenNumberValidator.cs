using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping_Site.Models.MyValidation
{
    public class EvenNumberValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var processor = (Processor)validationContext.ObjectInstance;
            int cores_number = processor.Cores_number;
            bool cond = true;

            if (cores_number % 2 != 0)
                cond = false;

            return cond ? ValidationResult.Success : new ValidationResult("This is not a even number!");
        }
    }
}