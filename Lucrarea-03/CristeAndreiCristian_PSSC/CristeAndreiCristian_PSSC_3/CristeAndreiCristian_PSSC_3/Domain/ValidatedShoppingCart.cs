namespace CristeAndreiCristian_PSSC_3.Domain
{
    public record ValidatedShoppingCart(PCode productCode, Quantity quantity, Address address, Price price);
}
