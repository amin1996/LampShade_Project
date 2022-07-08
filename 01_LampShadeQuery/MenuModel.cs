using _01_LampShadeQuery.Contracts.ArticleCategory;
using _01_LampShadeQuery.Contracts.ProductCategory;
using System.Collections.Generic;

namespace _01_LampShadeQuery
{
    public class MenuModel
    {
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ProductCategoryQueryModel> ProductCategories { get; set; }
    }
}
