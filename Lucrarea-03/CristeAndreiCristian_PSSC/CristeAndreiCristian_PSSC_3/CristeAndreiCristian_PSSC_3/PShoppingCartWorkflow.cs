using CristeAndreiCristian_PSSC_3.Domain;
using LanguageExt;
using System;
using System.Threading.Tasks;
using static CristeAndreiCristian_PSSC_3.Domain.ShoppingCarts;
using static CristeAndreiCristian_PSSC_3.Domain.ShoppingCartsPaidEvent;
using static CristeAndreiCristian_PSSC_3.ShoppingCartsOperations;

namespace CristeAndreiCristian_PSSC_3
{
    public class PShoppingCartWorkflow
    {
        public async Task<IShoppingCartsPaidEvent> ExecuteAsync(PShoppingCartCmd command, Func<PCode, TryAsync<bool>> checkProductExists, Func<PCode, Quantity, TryAsync<bool>> checkStock, Func<Address, TryAsync<bool>> checkAddress)
        {
            EmptyShoppingCarts emptyShoppingCarts = new EmptyShoppingCarts(command.InputShoppingCarts);
            IShoppingCarts shoppingCarts = await ValidateShoppingCarts(checkProductExists, checkStock, checkAddress, emptyShoppingCarts);
            shoppingCarts = CalculateFinalPrices(shoppingCarts);
            shoppingCarts = PayShoppingCarts(shoppingCarts);

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
