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
                CreatedOn = DateTime.Now,
                LastUpdatedOn = null
            };
        }
        public static void Copy(this Product prd, ProductUpdateReq req)
        {
            var category = Category.None;
            Category.TryParse(req.Category,out category);
            prd.Name = req.Name;
            prd.Category = category;
        }
        public static Product Clone(this Product prd)
        {
            return new Product()
            {
                Id = prd.Id,
                Code = prd.Code,
                Name = prd.Name,
                Category = prd.Category,
                CreatedOn = prd.CreatedOn,
                LastUpdatedOn = prd.LastUpdatedOn,
            };
        }
        public static void Copy(this Product prd, Product other)
        {
            prd.Id = other.Id;
            prd.Code = other.Code;
            prd.Name = other.Name;
            prd.Category = other.Category;
            prd.CreatedOn = other.CreatedOn;
            prd.LastUpdatedOn = other.LastUpdatedOn;
        }
    }
}
