using ErrorOr;
using CatalogRestApi.Models;
using CatalogRestApi.ServiceErrors;

namespace CatalogRestApi.Services.Products
{
    public class ProductService : IProductService

        //since we are not using any database, we will simply use a dictionary to store our products
    {   private static readonly Dictionary<Guid, Product> _products = new();

        public ErrorOr<Created> CreateProduct(Product product)
        {
            _products.Add(product.Id, product);
            return Result.Created;
        }

        public ErrorOr<Deleted> DeleteProduct(Guid id)
        {
            _products.Remove(id);

            return Result.Deleted;
        }

        public ErrorOr<List<Product>> GetAllProducts()
        {
            return _products.Values.ToList();
        }

        public ErrorOr<Product> GetProduct(Guid id)
        {
            if (_products.TryGetValue(id, out var product)) 
            { return product; }
            return Errors.Products.NotFound; 
        }

        public ErrorOr<UpsertedProduct> UpsertProduct(Product product)
        {   var isNewlyCreated = !_products.ContainsKey(product.Id);
            _products[product.Id] = product;

            return new UpsertedProduct(isNewlyCreated);
        }
    }
}
