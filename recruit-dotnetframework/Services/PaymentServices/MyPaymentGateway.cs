using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using recruit_dotnetframework.Models;

namespace recruit_dotnetframework.Services.PaymentServices
{
    /// <summary>
    /// Stubbed methods to mimic payment gateway Integration
    /// </summary>
    public class MyPaymentGateway : IPaymentGateway
    {
        public bool RegisterCard(CreditCard card, out string token, out string maskedCard)
        {
            token = $"stub_tok_{Guid.NewGuid().ToString().Substring(0, 8)}";
            maskedCard = MaskCard(card.CardNumber);
            return true;
        }
        private string MaskCard(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || number.Length < 4)
                return "****";
            return $"**** **** **** {number.Substring(number.Length - 4)}";
        }
    }
}