using CreditCardValidator;
using recruit_dotnetframework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace recruit_dotnetframework.Services
{
    public abstract class BaseCardService
    {
        public virtual bool ValidateCard(CreditCard card)
        {
            var detector = new CreditCardDetector(card.CardNumber);

            return detector.IsValid() &&
                   card.CVC >= 100 && card.CVC <= 9999 &&
                   card.ExpiryDate > DateTime.UtcNow;
        }
    }
}