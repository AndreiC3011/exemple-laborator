using System;
using System.Collections.Generic;
using CristeAndreiCristian_PSSC.Domain;
using static CristeAndreiCristian_PSSC.Domain.ShoppingCarts;

namespace CristeAndreiCristian_PSSC
{
    class Program
    {
        private static readonly Random random = new Random();

        static void Main(string[] args)
        {
            var cartsList = ReadListOfShoppingCarts().ToArray();
            EmptyCarts emptyCarts = new(cartsList);
            IShoppingCarts result = ValidateCarts(emptyCarts);
            result.Match(
                whenEmptyCarts: emptyResult => emptyCarts,
                whenUnvalidatedCarts: unvalidatedResult => unvalidatedResult,
                whenPaidCarts: paidResult => paidResult,
                whenValidatedCarts: validatedResult => PayCart(validatedResult)
            );

            Console.WriteLine("EXIT!");
        }

        private static List<EmptyCart> ReadListOfShoppingCarts()
        {
            List<EmptyCart> CartsList = new();
            do
            {

                var quantity = ReadValue("Cantitatea produsului: ");
                if (string.IsNullOrEmpty(quantity))
                {
                    break;
                }

                var product_code = ReadValue("Codul produsului: ");
                if (string.IsNullOrEmpty(product_code))
                {
                    break;
                }

                var address = ReadValue("Adresa: ");
                if (string.IsNullOrEmpty(address))
                {
                    break;
                }

                CartsList.Add(new(quantity, product_code, address));
            } while (true);
            return CartsList;
        }

        private static IShoppingCarts ValidateCarts(EmptyCarts emptyCarts) =>
            random.Next(100) > 50 ?
            new UnvalidatedCarts(new List<UnvalidatedCart>(), "Random errror")
            : new ValidatedCarts(new List<ValidatedCart>());

        private static IShoppingCarts PayCart(ValidatedCarts validExamGrades) =>
            new PaidCarts(new List<ValidatedCart>(), DateTime.Now);

        private static string? ReadValue(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
