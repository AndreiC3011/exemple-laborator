﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CristeAndreiCristian_PSSC.Domain
{
    public record Quantity
    {
        public int Value { get; }

        public Quantity(int value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidQuantity($"{value} is an invalid quantity value.");
            }
        }

        public override string ToString()
        {
            return $"{Value}";
        }

        public static bool TryParse(string quantityString, out Quantity quantity)
        {
            bool isValid = false;
            quantity = null;
            if (int.TryParse(quantityString, out int numericQuantity))
            {
                if (IsValid(numericQuantity))
                {
                    isValid = true;
                    quantity = new(numericQuantity);
                }
            }

            return isValid;
        }

        private static bool IsValid(int numericQuantity) => numericQuantity > 0;
    }
}
