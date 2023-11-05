using ErrorOr;
using CatalogRestApi.Models;

namespace CatalogRestApi.Services.Products
{
    public interface IProductService 
    {
        ErrorOr<Created> CreateProduct(Product product);
        ErrorOr<Deleted> DeleteProduct(Guid id);
        ErrorOr<List<Product>> GetAllProducts();
        ErrorOr<Product> GetProduct(Guid id);
        ErrorOr<UpsertedProduct> UpsertProduct(Product product);
    }
}
