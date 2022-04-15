using System.Collections.Generic;

namespace _01_LampShadeQuery.Contracts.ProductCategory
{
	public interface IProductCategoryQuery
	{
        List<ProductCategoryQueryViewModel> GetProductCategories();
	}
}
