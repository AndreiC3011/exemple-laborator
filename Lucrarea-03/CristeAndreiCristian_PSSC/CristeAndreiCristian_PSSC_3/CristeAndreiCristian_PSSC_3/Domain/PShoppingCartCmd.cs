using System.Collections.Generic;

namespace CristeAndreiCristian_PSSC_3.Domain
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
