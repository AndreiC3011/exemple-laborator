using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CristeAndreiCristian_PSSC.Domain
{
    public record EShoppingCart(string productCode, string quantity, string address, string price);
}
