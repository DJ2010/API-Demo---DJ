using recruit_dotnetframework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recruit_dotnetframework.Services
{
    public interface ICreditCardService
    {
        bool RegisterCard(CreditCard creditCard, out string token, out string maskedCard);
    }
}
