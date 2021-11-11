﻿using CristeAndreiCristian_PSSC_3.Domain;
using LanguageExt;
using static LanguageExt.Prelude;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CristeAndreiCristian_PSSC_3.Domain.ShoppingCarts;

namespace CristeAndreiCristian_PSSC_3
{
    public static class ShoppingCartsOperations
    {
        public static Task<IShoppingCarts> ValidateShoppingCarts(Func<PCode, TryAsync<bool>> checkProductExists, Func<PCode, Quantity, TryAsync<bool>> checkStock, Func<Address, TryAsync<bool>> checkAddress, EmptyShoppingCarts shoppingCarts) =>
            shoppingCarts.ShoppingCartList
                      .Select(ValidateShoppingCart(checkProductExists, checkStock, checkAddress))
                      .Aggregate(CreateEmptyValidatedShoppingCartList().ToAsync(), ReduceValidShoppingCarts)
                      .MatchAsync(
                            Right: validatedShoppingCarts => new ValidatedShoppingCarts(validatedShoppingCarts),
                            LeftAsync: errorMessage => Task.FromResult((IShoppingCarts)new UnvalidatedShoppingCarts(shoppingCarts.ShoppingCartList, errorMessage))
                      );
        private static Func<EShoppingCart, EitherAsync<string, ValidatedShoppingCart>> ValidateShoppingCart(Func<PCode, TryAsync<bool>> checkProductExists, Func<PCode, Quantity, TryAsync<bool>> checkStock, Func<Address, TryAsync<bool>> checkAddress) =>
        emptyShoppingCart => ValidateShoppingCart(checkProductExists, checkStock, checkAddress, emptyShoppingCart);

        private static EitherAsync<string, ValidatedShoppingCart> ValidateShoppingCart(Func<PCode, TryAsync<bool>> checkProductExists, Func<PCode, Quantity, TryAsync<bool>> checkStock, Func<Address, TryAsync<bool>> checkAddress, EShoppingCart emptyShoppingCart) =>
            from address in Address.TryParse(emptyShoppingCart.address)
                                    .ToEitherAsync(() => $"Invalid address ({emptyShoppingCart.productCode}, {emptyShoppingCart.address})")
            from productCode in PCode.TryParse(emptyShoppingCart.productCode)
                                    .ToEitherAsync(() => $"Invalid product code ({emptyShoppingCart.productCode})")
            from quantity in Quantity.TryParse(emptyShoppingCart.quantity)
                                    .ToEitherAsync(() => $"Invalid quantity ({emptyShoppingCart.productCode}, {emptyShoppingCart.quantity})")
            from price in Price.TryParse(emptyShoppingCart.price)
                        .ToEitherAsync(() => $"Invalid price ({emptyShoppingCart.productCode}, {emptyShoppingCart.price})")
            from productExists in checkProductExists(productCode)
                                    .ToEither(error => error.ToString())
            from stockOK in checkStock(productCode, quantity)
                                    .ToEither(error => error.ToString())
            from addressOK in checkAddress(address)
                                    .ToEither(error => error.ToString())
            select new ValidatedShoppingCart(productCode, quantity, address, price); // putem return ce anume dorimm noi din acest link syntax
        // in cazul in care o operatiune nu se executa nu succes, tot workflowul se scurtcircuiteaza. 
        // in either async poti trata mai multe tipuri de return

        private static Either<string, List<ValidatedShoppingCart>> CreateEmptyValidatedShoppingCartList() =>
            Right(new List<ValidatedShoppingCart>());

        private static EitherAsync<string, List<ValidatedShoppingCart>> ReduceValidShoppingCarts(EitherAsync<string, List<ValidatedShoppingCart>> acc, EitherAsync<string, ValidatedShoppingCart> next) =>
            from list in acc
            from nextShoppingCart in next
            select list.AppendValidShoppingCart(nextShoppingCart);

        private static List<ValidatedShoppingCart> AppendValidShoppingCart(this List<ValidatedShoppingCart> list, ValidatedShoppingCart validShoppingCart)
        {
            list.Add(validShoppingCart);
            return list;
        }

        public static IShoppingCarts CalculateFinalPrices(IShoppingCarts shoppingCarts) => shoppingCarts.Match(
            whenEmptyShoppingCarts: emptyShoppingCart => emptyShoppingCart,
            whenUnvalidatedShoppingCarts: unvalidatedShoppingCart => unvalidatedShoppingCart,
            whenCalculatedShoppingCarts: calculatedShoppingCart => calculatedShoppingCart,
            whenPaidShoppingCarts: paidShoppingCart => paidShoppingCart,
            whenValidatedShoppingCarts: CalculateFinalPrice
        );

        private static IShoppingCarts CalculateFinalPrice(ValidatedShoppingCarts validShoppingCarts) =>
            new CalculatedShoppingCarts(validShoppingCarts.ShoppingCartList
                                                          .Select(CalculateShoppingCartFinalPrice)
                                                          .ToList()
                                                          .AsReadOnly());
        private static CShoppingCart CalculateShoppingCartFinalPrice(ValidatedShoppingCart validShoppingCart) =>
            new CShoppingCart(validShoppingCart.productCode,
                                      validShoppingCart.quantity,
                                      validShoppingCart.address,
                                      validShoppingCart.price,
                                      validShoppingCart.price * validShoppingCart.quantity);
        public static IShoppingCarts PayShoppingCarts(IShoppingCarts shoppingCarts) => shoppingCarts.Match(
            whenEmptyShoppingCarts: emptyShoppingCart => emptyShoppingCart,
            whenUnvalidatedShoppingCarts: unvalidatedShoppingCart => unvalidatedShoppingCart,
            whenPaidShoppingCarts: paidShoppingCart => paidShoppingCart,
            whenValidatedShoppingCarts: validatedShoppingCart => validatedShoppingCart,
            whenCalculatedShoppingCarts: GenerateExport
        );

        private static IShoppingCarts GenerateExport(CalculatedShoppingCarts calculatedShoppingCart) =>
            new PaidShoppingCarts(calculatedShoppingCart.ShoppingCartList,
                                    calculatedShoppingCart.ShoppingCartList.Aggregate(new StringBuilder(), CreateCsvLine).ToString(),
                                    DateTime.Now);

        private static StringBuilder CreateCsvLine(StringBuilder export, CShoppingCart shoppingCart) =>
            export.AppendLine($"{shoppingCart.productCode.Code}, {shoppingCart.price}, {shoppingCart.quantity}, {shoppingCart.finalPrice}");
    }
}
