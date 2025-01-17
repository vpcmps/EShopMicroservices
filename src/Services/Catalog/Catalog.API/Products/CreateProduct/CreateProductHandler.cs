
using FluentValidation;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    string ImageUrl,
    decimal Price,
    List<string> Categories) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("ImageUrl is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.Categories).NotEmpty().WithMessage("Category is required");
    }
}
internal class CreateProductCommandHandler(IDocumentSession session, IValidator<CreateProductCommand> validator, ILogger<CreateProductCommandHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var product = new Product
        {
            Name = command.Name,
            Description = command.Description,
            ImageUrl = command.ImageUrl,
            Price = command.Price,
            Categories = command.Categories
        };
        
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new CreateProductResult(product.Id);
    }
}