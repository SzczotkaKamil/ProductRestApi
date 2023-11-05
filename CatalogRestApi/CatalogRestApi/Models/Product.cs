using ErrorOr;
using CatalogRestApi.Contracts.Products;
using CatalogRestApi.ServiceErrors;
using System.Xml.Linq;

namespace CatalogRestApi.Models
{
    public class Product
    {
        public const int MinNameLength = 3;
        public const int MaxNameLength = 50;
        public const int MaxDescriptionLength = 150;
        public string Name { get; }
        public Guid Id { get; }
        public string? Description { get; }
        public string Code { get; }
        public double Price { get; }
        public DateTime LastModifiedDateTime { get; }

        private Product(
            Guid id,
            string name,
            string? description,
            string code,
            double price,
            DateTime lastModifiedDateTime)
        {


            Id = System.Guid.NewGuid();
            Name = name;
            Description = description;
            Code = code;
            Price = price;
            LastModifiedDateTime = lastModifiedDateTime;
        }

        //enforce invariants so that name, id, code price is always set

        public static ErrorOr<Product> Create
            (
            string name,
            string? description,
            string code,
            double price,
            Guid? id = null)

        {
            List<Error> errors = new();
            if (name.Length is < MinNameLength or > MaxNameLength)
            {
                errors.Add(Errors.Products.InvalidName);
            }
            if (description.Length > MaxDescriptionLength)
            {
                errors.Add(Errors.Products.InvalidDescription);
            }
            if (errors.Count > 0)
            {
                return errors;
            }
            return new Product(id ?? Guid.NewGuid(), name, description, code, price, System.DateTime.Now);
        }
        public static ErrorOr<Product> From (CreateProductRequest request) {
            return Create(
                request.Name,
                request.Description,
                request.Code,
                request.Price);
        }
        public static ErrorOr<Product> From(Guid id, UpsertProductRequest request)
        {
            return Create(
                request.Name,
                request.Description,
                request.Code,
                request.Price,
                id);
        }

    }
}