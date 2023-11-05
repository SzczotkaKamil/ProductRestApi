using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogRestApi.Contracts.Products
{
    public record ProductResponse(
         Guid Id,
         string Description,
         string Code,
         string Name,
         double Price,
         DateTime LastModifiedDateTime);

}
