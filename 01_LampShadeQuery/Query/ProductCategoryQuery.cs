using _01_LampShadeQuery.Contracts.ProductCategory;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampShadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _shopcontext;

        public ProductCategoryQuery(ShopContext shopcontext)
        {
            _shopcontext = shopcontext;
        }

        public List<ProductCategoryQueryViewModel> GetProductCategories()
        {
            return _shopcontext.ProductCategories.Select(c => new ProductCategoryQueryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Picture = c.Picture,
                PictureAlt = c.PictureAlt,
                PictureTitle = c.PictureTitle,
                Slug = c.Slug,
            }).ToList();
        }
    }
}
