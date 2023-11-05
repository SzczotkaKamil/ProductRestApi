using ErrorOr;
using CatalogRestApi.Contracts.Products;
using CatalogRestApi.Models;
using CatalogRestApi.ServiceErrors;
using CatalogRestApi.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace CatalogRestApi.Controllers
{
    public class ProductsController: ApiController
    {   private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductRequest request)
        {
            ErrorOr<Product> requestToProductResult  = Product.Create(
                request.Name,
                request.Description,
                request.Code,
                request.Price);

            if (requestToProductResult.IsError)
            {
                return Problem(requestToProductResult.Errors);
            }

            var product = requestToProductResult.Value;
            ErrorOr<Created> createdProductResult = _productService.CreateProduct(product);

            return createdProductResult.Match(
                               created => CreatedAtGetProduct(product),
                                              errors => Problem(errors));

        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            ErrorOr<List<Product>> getAllProductsResult = _productService.GetAllProducts();
            return 
                getAllProductsResult.Match(products => Ok(products.Select(MapProductResponse)),
                                              errors => Problem(errors));
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetProduct(Guid id)
        {
            ErrorOr<Product> getProductResult = _productService.GetProduct(id);
            return getProductResult.Match(product => Ok(MapProductResponse(product)),
                               errors => Problem(errors));
        }


        [HttpPut("{id:guid}")]
        public IActionResult UpsertProduct(Guid id, UpsertProductRequest request)
        {
            ErrorOr<Product> requestToProductResult =  Product.From(id, request);
            if (requestToProductResult.IsError)
            {
                return Problem(requestToProductResult.Errors);
            }
            var product = requestToProductResult.Value;
            ErrorOr<UpsertedProduct> upsertProductResult =  _productService.UpsertProduct(product);

            return upsertProductResult.Match(
                               upserted => upserted.IsNewlyCreated ? CreatedAtGetProduct(product) : NoContent(),
                                              errors => Problem(errors));
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteProduct(Guid id)
        {
            ErrorOr<Deleted> deleteProductResult = _productService.DeleteProduct(id);
            return deleteProductResult.Match(
                   deleted => NoContent(),
                   errors => Problem(errors));
        }
        private static ProductResponse MapProductResponse(Product product)
        {
            return new ProductResponse(
                product.Id,
                product.Description,
                product.Code,
                product.Name,
                product.Price,
                product.LastModifiedDateTime);
        }
        private IActionResult CreatedAtGetProduct(Product product)
        {
            return CreatedAtAction(
                actionName: nameof(GetProduct),
                routeValues: new { id = product.Id },
                value: MapProductResponse(product));
        }
    }
}
    