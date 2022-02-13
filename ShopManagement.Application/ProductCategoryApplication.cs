using _0_FrameWork.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation=new OperationResult();
            if (_productCategoryRepository.Exists(x=>x.Name==command.Name))
                operation.Faild("امکان ثبت رکورد تکراری وجود ندارد لطفا مجدد تلاش کنید");

            var slug = command.Slug.Slugify();
            var productCategory = new ProductCategory(command.Name, command.Description, command.Picture, command.PictureAlt
                , command.PictureTitle, command.KeyWords, command.MetaDescription, slug);

            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation=new OperationResult();
            var productCategory=_productCategoryRepository.Get(command.Id);

            if (productCategory == null)
                return operation.Faild("رکورد با اطلاعات درخواست شده یافت نشد. لطفل مجدد تلاش کنید.");

            if(_productCategoryRepository.Exists(x=>x.Name==command.Name && x.Id!=command.Id))
                return operation.Faild("امکان ثبت رکورد تکراری وجود ندارد لطفا مجدد تلاش کنید");

            var slug = command.Slug.Slugify();
            productCategory.Edit(command.Name, command.Description, command.Picture, command.PictureAlt
                , command.PictureTitle, command.KeyWords, command.MetaDescription, slug);

            _productCategoryRepository.SaveChanges();

            return operation.Succeeded();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }
    }
}
