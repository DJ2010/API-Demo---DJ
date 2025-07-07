using recruit_dotnetframework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recruit_dotnetframework.Services.PaymentServices
{
    public interface IPaymentGateway
    {
        bool RegisterCard(CreditCard card, out string token, out string maskedCard);
    }
}
