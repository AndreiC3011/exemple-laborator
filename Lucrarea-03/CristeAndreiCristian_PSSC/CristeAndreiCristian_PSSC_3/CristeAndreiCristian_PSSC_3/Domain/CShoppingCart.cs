namespace CristeAndreiCristian_PSSC_3.Domain
{
    public record CShoppingCart(PCode productCode, Quantity quantity, Address address, Price price, Price finalPrice);
}
