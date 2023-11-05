using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogRestApi.Contracts.Products
{
    public record CreateProductRequest(
         string Name,
         string Description,
         string Code,
         double Price);
}
