using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLib.Extensions
{
    public static class ProductExtensions
    {
        public static ProductResponse ToResponse(this Product prd)
        {
            return new ProductResponse()
            {
                Id = prd.Id,
                Code = prd.Code,
                Name = prd.Name,
                Category = Enum.GetName<Category>(prd.Category)
            };
        }
        public static Product ToEntity(this ProductCreateReq req)
        {
            var category = Category.None;
            Category.TryParse(req.Category, out category);
            return new Product()
            {
                Id = Guid.NewGuid().ToString(),
                Code = req.Code,
                Name = req.Name,
                Category = category,
                Created = DateTime.Now,
                LastUpdated = null
            };
        }
    }
}
