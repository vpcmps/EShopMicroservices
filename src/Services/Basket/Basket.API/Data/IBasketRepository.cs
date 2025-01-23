namespace Basket.API.Data;

public interface IBasketRepository
{
    internal Task<ShoppingCart> GetBaket(string userName, CancellationToken cancellationToken = default);
    internal Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default);
    internal Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
}

