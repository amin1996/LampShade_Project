using _0_FrameWork.Domain;
using ShopManagement.Application.Contracts.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<long, Product>
    {
        EditProduct GetDetails(long id);
        List<ProductViewModel> GetProducts();
        Product GetProductWithCategory(long Id);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
    }
}
