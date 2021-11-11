using CristeAndreiCristian_PSSC_3.Domain;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CristeAndreiCristian_PSSC_3
{
    class Program
    {
        private static readonly Random random = new Random();
        static void Main(string[] args)
        {
            Task.Run(async () => { await Start(args); })
                            .GetAwaiter()
                            .GetResult();
        }
        static async Task Start(string[] args)
        {
            var listOfGrades = ReadListOfShoppingCarts().ToArray();
            PShoppingCartCmd command = new(listOfGrades);
            PShoppingCartWorkflow workflow = new PShoppingCartWorkflow();
            var result = await workflow.ExecuteAsync(command, CheckProductExists, CheckStock, CheckAddress);

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
        }

        private static List<EShoppingCart> ReadListOfShoppingCarts()
        {
            List<EShoppingCart> listOfShoppingCarts = new();
            do
            {
                //read registration number and grade and create a list of greads
                var quantity = ReadValue("Cantitatea produsului comandat: ");
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
        private static TryAsync<bool> CheckProductExists(PCode product) => async () => true;
        private static TryAsync<bool> CheckStock(PCode product, Quantity quantity) => async () => true;
        private static TryAsync<bool> CheckAddress(Address address) => async () => true;
    }
}
