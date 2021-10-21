using CristeAndreiCristian_PSSC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CristeAndreiCristian_PSSC.Domain.ShoppingCarts;
using static CristeAndreiCristian_PSSC.Domain.ShoppingCartsPaidEvent;
using static CristeAndreiCristian_PSSC.ShoppingCartsOperations;

namespace CristeAndreiCristian_PSSC
{
    public class PShoppingCartWorkflow
    {
        public IShoppingCartsPaidEvent Execute(PShoppingCartCmd command, Func<PCode, bool> checkProductExists)
        {
            EmptyShoppingCarts emptyShoppingCarts = new EmptyShoppingCarts(command.InputShoppingCarts);
            IShoppingCarts shoppingCarts = ValidateShoppingCarts(checkProductExists, emptyShoppingCarts);
            shoppingCarts = CalculateFinalPrice(shoppingCarts);
            shoppingCarts = PayShoppingCart(shoppingCarts);

            return shoppingCarts.Match(
                    whenEmptyShoppingCarts: emptyShoppingCarts => new ShoppingCartsPaidFailedEvent("Unexpected unvalidated state") as IShoppingCartsPaidEvent,
                    whenUnvalidatedShoppingCarts: unvalidatedShoppingCarts => new ShoppingCartsPaidFailedEvent(unvalidatedShoppingCarts.Reason),
                    whenValidatedShoppingCarts: validatedShoppingCarts => new ShoppingCartsPaidFailedEvent("Unexpected validated state"),
                    whenCalculatedShoppingCarts: calculatedShoppingCarts => new ShoppingCartsPaidFailedEvent("Unexpected calculated state"),
                    whenPaidShoppingCarts: paidShoppingCarts => new ShoppingCartsPaidScucceededEvent(paidShoppingCarts.Csv, paidShoppingCarts.PublishedDate)
                );
        }
    }
}
