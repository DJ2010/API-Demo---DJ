using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace recruit_dotnetframework.Models
{
    public class CreditCard
    {
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }  
        public int CVC  { get; set; }
    }
}