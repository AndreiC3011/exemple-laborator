using CSharp.Choices;
using System;
using System.Collections.Generic;

namespace CristeAndreiCristian_PSSC_3.Domain
{
    [AsChoice]
    public static partial class ShoppingCarts
    {
        public interface IShoppingCarts { }

        public record EmptyShoppingCarts : IShoppingCarts
        {
            public EmptyShoppingCarts(IReadOnlyCollection<EShoppingCart> shoppingCartList)
            {
                ShoppingCartList = shoppingCartList;
            }

            public IReadOnlyCollection<EShoppingCart> ShoppingCartList { get; }
        }

        public record UnvalidatedShoppingCarts : IShoppingCarts
        {
            internal UnvalidatedShoppingCarts(IReadOnlyCollection<EShoppingCart> shoppingCartList, string reason)
            {
                ShoppingCartList = shoppingCartList;
                Reason = reason;
            }

            public IReadOnlyCollection<EShoppingCart> ShoppingCartList { get; }
            public string Reason { get; }
        }

        public record ValidatedShoppingCarts : IShoppingCarts
        {
            internal ValidatedShoppingCarts(IReadOnlyCollection<ValidatedShoppingCart> shoppingCartList)
            {
                ShoppingCartList = shoppingCartList;
            }

            public IReadOnlyCollection<ValidatedShoppingCart> ShoppingCartList { get; }
        }

        public record CalculatedShoppingCarts : IShoppingCarts
        {
            internal CalculatedShoppingCarts(IReadOnlyCollection<CShoppingCart> shoppingCartList)
            {
                ShoppingCartList = shoppingCartList;
            }

            public IReadOnlyCollection<CShoppingCart> ShoppingCartList { get; }
        }

        public record PaidShoppingCarts : IShoppingCarts
        {
            internal PaidShoppingCarts(IReadOnlyCollection<CShoppingCart> shoppingCartsList, string csv, DateTime publishedDate)
            {
                ShoppingCartList = shoppingCartsList;
                PublishedDate = publishedDate;
                Csv = csv;
            }

            public IReadOnlyCollection<CShoppingCart> ShoppingCartList { get; }
            public DateTime PublishedDate { get; }
            public string Csv { get; }
        }
    }
}
