using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync(cancellation))
            return;

        session.Store(GetPreconfiguresProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<Product> GetPreconfiguresProducts() =>
    [
        new()
        {
            Id = Guid.Parse("f4f3e6e0-e5e9-11ec-a56a-0e2d9c1f8c8e"),
            Name = "Apple",
            Description = "Apple",
            ImageUrl = "https://www.apple.com/v/mac/home/ar/images/overview/desktop-mac-app-store-icon.png",
            Price = 1.99m,
            Categories = new List<string> { "Fruits" }
        },
        new()
        {
            Id = Guid.Parse("a4f3e6e1-e5e9-11ec-a56a-0e2d9c1f8c8e"),
            Name = "Banana",
            Description = "Banana",
            ImageUrl =
                "https://upload.wikimedia.org/wikipedia/commons/thumb/2/21/Bananas_-_source.jpg/220px-Bananas_-_source.jpg",
            Price = 0.99m,
            Categories = new List<string> { "Fruits" }
        },
        new()
        {
            Id = Guid.Parse("b4f3e6e2-e5e9-11ec-a56a-0e2d9c1f8c8e"),
            Name = "Orange",
            Description = "Orange",
            ImageUrl =
                "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3a/Oranges_-_source.jpg/220px-Oranges_-_source.jpg",
            Price = 0.99m,
            Categories = new List<string> { "Fruits" }
        },
        new()
        {
            Id = Guid.Parse("c4f3e6e3-e5e9-11ec-a56a-0e2d9c1f8c8e"),
            Name = "Grapes",
            Description = "Grapes",
            ImageUrl =
                "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f8/Grapes_-_source.jpg/220px-Grapes_-_source.jpg",
            Price = 0.99m,
            Categories = new List<string> { "Fruits" }
        },
        new()
        {
            Id = Guid.Parse("d4f3e6e4-e5e9-11ec-a56a-0e2d9c1f8c8e"),
            Name = "Strawberry",
            Description = "Strawberry",
            ImageUrl =
                "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1a/Strawberries_-_source.jpg/220px-Strawberries_-_source.jpg",
            Price = 0.99m,
            Categories = new List<string> { "Fruits" }
        },
        new()
        {
            Id = Guid.Parse("e4f3e6e5-e5e9-11ec-a56a-0e2d9c1f8c8e"),
            Name = "Watermelon",
            Description = "Watermelon",
            ImageUrl =
                "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8a/Watermelon_-_source.jpg/220px-Watermelon_-_source.jpg",
            Price = 0.99m,
            Categories = new List<string> { "Fruits" }
        },
        new()
        {
            Id = Guid.Parse("g4f3e6e6-e5e9-11ec-a56a-0e2d9c1f8c8e"),
            Name = "Peach",
            Description = "Peach",
            ImageUrl =
                "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3a/Peach_-_source.jpg/220px-Peach_-_source.jpg",
            Price = 0.99m,
            Categories = new List<string> { "Fruits" }
        }
    ];
}