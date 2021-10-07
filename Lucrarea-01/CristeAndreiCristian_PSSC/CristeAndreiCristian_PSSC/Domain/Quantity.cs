namespace CristeAndreiCristian_PSSC.Domain
{
    public record Quantity
    {
        public int QuantityV { get; }

        public Quantity(int _quantity)
        {
            if (_quantity > 0)
            {
                QuantityV = _quantity;
            }
            else
            {
                throw new InvalidQuantity($"{_quantity} is an invalid quantity value.");
            }
        }

        public override string ToString()
        {
            return QuantityV.ToString();
        }
    }
}
