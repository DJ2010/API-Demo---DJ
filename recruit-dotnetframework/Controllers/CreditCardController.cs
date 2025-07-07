using recruit_dotnetframework.Models;
using recruit_dotnetframework.Services;
using recruit_dotnetframework.Services.Logs;
using recruit_dotnetframework.Services.PaymentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace recruit_dotnetframework.Controllers
{
    public class CreditCardController : ApiController
    {
        private readonly ICreditCardService _creditCardService;
        private readonly ILogService _logger;
        public CreditCardController()
        {
            _logger = new LogService(); // can use Dependency injection if needed
            _creditCardService = new CreditCardService(new MyPaymentGateway(), _logger);
        }

        public CreditCardController(ICreditCardService cardService, ILogService logger)
        {
            _creditCardService = cardService;
            _logger = logger;
        }
        [HttpPost]
        [Route("api/register-card")]
        public IHttpActionResult RegisterCard([FromBody] CreditCard card)
        {
            try
            {

                if (card == null)
                {
                    //we dont necessarily have to log validation error etc if returning the response to Front end. Depends on the business requirements
                    // _logger.LogError("Card registration failed: request body is null.");
                    return BadRequest("Request body is missing.");
                }

                string token, maskedCard;

                if (_creditCardService.RegisterCard(card, out token, out maskedCard))
                {
                    _logger.LogInfo($"Card registered successfully. Token: {token}, Masked: {maskedCard}");
                    return Ok("Card registered successfully.");
                }
                else
                {
                    _logger.LogInfo("Card registration failed: invalid card details.");
                    return BadRequest("Invalid card details.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error during card registration.", ex);
                return InternalServerError(); 
            }
        }

    }
}
