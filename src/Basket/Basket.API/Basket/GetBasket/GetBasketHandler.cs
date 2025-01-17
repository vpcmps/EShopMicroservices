﻿namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string Username) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);

public class GetBasketHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}