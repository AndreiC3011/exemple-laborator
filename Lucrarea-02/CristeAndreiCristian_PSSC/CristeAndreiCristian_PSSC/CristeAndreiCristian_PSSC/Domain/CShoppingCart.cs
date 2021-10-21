using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CristeAndreiCristian_PSSC.Domain
{
    public record CShoppingCart(PCode productCode, Quantity quantity, Address address, Price price, Price finalPrice);
}
