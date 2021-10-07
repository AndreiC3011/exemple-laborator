using CSharp.Choices;
using System;
using System.Collections.Generic;

namespace CristeAndreiCristian_PSSC.Domain
{
    [AsChoice]
    public static partial class ShoppingCarts
    {
        public interface IShoppingCarts { }

        public record EmptyCarts(IReadOnlyCollection<EmptyCart> ShoppingCarts) : IShoppingCarts;

        public record UnvalidatedCarts(IReadOnlyCollection<UnvalidatedCart> ShoppingCarts, string reason) : IShoppingCarts;

        public record ValidatedCarts(IReadOnlyCollection<ValidatedCart> ShoppingCarts) : IShoppingCarts;

        public record PaidCarts(IReadOnlyCollection<ValidatedCart> ShoppingCarts, DateTime PublishedDate) : IShoppingCarts;
    }
}
