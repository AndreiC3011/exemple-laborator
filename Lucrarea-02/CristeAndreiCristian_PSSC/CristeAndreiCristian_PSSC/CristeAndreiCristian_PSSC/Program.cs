using System;
using CristeAndreiCristian_PSSC.Domain;
using System.Collections.Generic;
using static CristeAndreiCristian_PSSC.Domain.ShoppingCarts;

namespace CristeAndreiCristian_PSSC
{
    class Program
    {
        private static readonly Random random = new Random();
        static void Main(string[] args)
        {
            var listOfGrades = ReadListOfShoppingCarts().ToArray();
            PShoppingCartCmd command = new(listOfGrades);
            PShoppingCartWorkflow workflow = new PShoppingCartWorkflow();
            var result = workflow.Execute(command, (productCode) => true);

            result.Match(
                    whenShoppingCartsPaidFailedEvent: @event =>
                    {
                        Console.WriteLine($"Pay failed: {@event.Reason}");
                        return @event;
                    },
                    whenShoppingCartsPaidScucceededEvent: @event =>
                    {
                        Console.WriteLine($"Pay succeeded.");
                        Console.WriteLine(@event.Csv);
                        return @event;
                    }
                );

            Console.WriteLine("Exit!");
        }
        private static List<EShoppingCart> ReadListOfShoppingCarts()
        {
            List<EShoppingCart> listOfShoppingCarts = new();
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

                var price = ReadValue("Pretul: ");
                if (string.IsNullOrEmpty(price))
                {
                    break;
                }

                listOfShoppingCarts.Add(new(product_code, quantity, address, price));
            } while (true);
            return listOfShoppingCarts;
        }

        private static string? ReadValue(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
