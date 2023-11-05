using ErrorOr;

namespace CatalogRestApi.ServiceErrors
{
    public static class Errors
    {public static class Products

        {
            public static Error NotFound => Error.NotFound(
                code: "Product.NotFound",
                description: "Product not found.");

            public static Error InvalidName => Error.Validation(
                code: "Product.InvalidName",
                description: $"Product name must be atleast {Models.Product.MinNameLength} characters long and at most {Models.Product.MaxNameLength} characters long.");

            public static Error InvalidDescription => Error.Validation(
                code: "Product.InvalidDescription",
                description: $"Product name must be at most {Models.Product.MaxDescriptionLength} characters long.");
        }
                


    
}
}
