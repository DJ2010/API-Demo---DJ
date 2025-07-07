using NUnit.Framework;
using recruit_dotnetframework.Models;
using recruit_dotnetframework.Services;
using recruit_dotnetframework.Services.Logs;
using recruit_dotnetframework.Services.PaymentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardApi.Tests.Services
{
    [TestFixture]
    public class CreditCardServiceTests
    {
        private CreditCardService _service;
        private LogService _logService;
        private IPaymentGateway _paymentGateway;

        [SetUp]
        public void SetUp()
        {
            _logService = new LogService();
            _paymentGateway = new MyPaymentGateway();
            _service = new CreditCardService(_paymentGateway, _logService);
        }

        [Test]
        public void RegisterCard_WithValidCard_ReturnsTrue()
        {
            var card = new CreditCard
            {
                CardNumber = "4111111111111111",
                ExpiryDate = DateTime.UtcNow.AddYears(1),
                CVC = 123
            };
            string token, maskedCard;
            var result = _service.RegisterCard(card, out token, out maskedCard);

            Assert.IsNotEmpty(token);
            Assert.IsNotEmpty(maskedCard);
            Assert.IsTrue(result);
        }
        [Test]
        public void RegisterCard_InvalidCardNumber_ReturnsFalse()
        {
            var card = new CreditCard
            {
                CardNumber = "",  // Invalid
                ExpiryDate = DateTime.UtcNow.AddYears(1),
                CVC = 123
            };

            string token, maskedCard;
            var result = _service.RegisterCard(card, out token, out maskedCard);

            AssertCardRegistrationFailed(result, token, maskedCard);
        }
        [Test]
        public void RegisterCard_WithExpiredCard_ReturnsFalse()
        {
            var card = new CreditCard
            {
                CardNumber = "4111111111111111",
                ExpiryDate = DateTime.UtcNow.AddMonths(-1),
                CVC = 123
            };

            string token, maskedCard;
            var result = _service.RegisterCard(card, out token, out maskedCard);

            AssertCardRegistrationFailed(result, token, maskedCard);
        }
        [Test]
        public void RegisterCard_InvalidCVC_ReturnsFalse()
        {
            var card = new CreditCard
            {
                CardNumber = "4111111111111111",
                ExpiryDate = DateTime.UtcNow.AddYears(1),
                CVC = 1
            };

            string token, maskedCard;
            var result = _service.RegisterCard(card, out token, out maskedCard);

            AssertCardRegistrationFailed(result, token, maskedCard);
        }

        private void AssertCardRegistrationFailed(bool result, string token, string maskedCard)
        {
            Assert.IsFalse(result, "Expected registration to fail.");
            Assert.IsNull(token, "Expected token to be null.");
            Assert.IsNull(maskedCard, "Expected maskedCard to be null.");
        }

    }
}
