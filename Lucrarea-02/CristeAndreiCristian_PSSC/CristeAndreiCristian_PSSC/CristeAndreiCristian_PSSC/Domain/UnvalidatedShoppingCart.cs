﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CristeAndreiCristian_PSSC.Domain
{
    public record UnvalidatedShoppingCart(PCode productCode, Quantity quantity, Address address, Price price);
}
