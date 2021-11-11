namespace CristeAndreiCristian_PSSC_3.Domain
{
    public record UnvalidatedShoppingCart(PCode productCode, Quantity quantity, Address address, Price price);
}
