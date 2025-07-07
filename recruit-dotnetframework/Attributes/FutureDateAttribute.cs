using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace recruit_dotnetframework.Attributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateValue)
            {
                return dateValue > DateTime.UtcNow;
            }
            return false;
        }
    }
}