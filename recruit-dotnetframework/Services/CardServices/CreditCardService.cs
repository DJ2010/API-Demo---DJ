using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CreditCardValidator;
using recruit_dotnetframework.Models;
using recruit_dotnetframework.Services.Logs;
using recruit_dotnetframework.Services.PaymentServices;

namespace recruit_dotnetframework.Services
{
    public class CreditCardService : BaseCardService, ICreditCardService
    {
        public readonly IPaymentGateway _paymentGateway;
        private readonly ILogService _logger;
        public CreditCardService(IPaymentGateway paymentGateway, ILogService logger)
        {
            _paymentGateway = paymentGateway;
            _logger = logger;
        }
        public override bool ValidateCard(CreditCard card)
        {
            var detector = new CreditCardDetector(card.CardNumber);

            return detector.IsValid() &&
                   card.CVC >= 100 && card.CVC <= 9999 &&
                   card.ExpiryDate > DateTime.UtcNow && (detector.Brand == CardIssuer.MasterCard || detector.Brand == CardIssuer.Visa);
        }
        public bool RegisterCard(CreditCard creditCard, out string token, out string maskedCard)
        {
            token = null;
            maskedCard = null;
            try
            {
                if (!ValidateCard(creditCard))
                {
                    _logger.LogInfo("Validation failed for card.");
                    return false;
                }

                bool result = _paymentGateway.RegisterCard(creditCard, out token, out maskedCard);
                _logger.LogInfo($"Card registration result: {result}, Token: {token}");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during RegisterCard", ex);
                return false;
            }
        }

       
    }
}