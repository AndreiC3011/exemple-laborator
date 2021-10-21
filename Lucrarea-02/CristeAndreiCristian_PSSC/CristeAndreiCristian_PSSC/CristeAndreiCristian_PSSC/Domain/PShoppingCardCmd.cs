using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CristeAndreiCristian_PSSC.Domain
{
    public record PShoppingCartCmd
    {
        public PShoppingCartCmd(IReadOnlyCollection<EShoppingCart> inputShoppingCarts)
        {
            InputShoppingCarts = inputShoppingCarts;
        }

        public IReadOnlyCollection<EShoppingCart> InputShoppingCarts { get; }
    }
}
