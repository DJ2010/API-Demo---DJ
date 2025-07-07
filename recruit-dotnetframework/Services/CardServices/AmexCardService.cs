using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CreditCardValidator;
using recruit_dotnetframework.Models;
using recruit_dotnetframework.Services.PaymentServices;

namespace recruit_dotnetframework.Services
{
    public class AmexCardService : BaseCardService, ICreditCardService
    {
        public readonly IPaymentGateway _paymentGateway;

        public AmexCardService()
        {
            _paymentGateway = new MyPaymentGateway(); // we can use different Payment gateway if needed
        }
        public override bool ValidateCard(CreditCard card)
        {
            var detector = new CreditCardDetector(card.CardNumber);

            return detector.IsValid() &&
                   card.CVC >= 100 && card.CVC <= 9999 &&
                   card.ExpiryDate > DateTime.UtcNow && detector.Brand == CardIssuer.AmericanExpress;
        }

        bool ICreditCardService.RegisterCard(CreditCard creditCard, out string token, out string maskedCard)
        {
            token = null;
            maskedCard = null;

            if (!ValidateCard(creditCard))
                return false;

            Console.WriteLine("Registered card: " + creditCard.CardNumber);
            return _paymentGateway.RegisterCard(creditCard, out token, out maskedCard);
        }
      
    }
}