using _0_FrameWork.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
                operation.Faild(ApplicationMessages.DuplicatedRecord);

            var Slug = command.Slug.Slugify();
            var categorySlug = _productCategoryRepository.GetSlugBy(command.CategoryId);
            var path = $"{categorySlug}//{Slug}"; 
            var picturePath=_fileUploader.Upload(command.Picture,path) ;

            var product = new Product(command.Name, command.Code, command.ShortDescription, command.Description
                , picturePath, command.PictureAlt, command.PictureTitle, command.CategoryId, Slug,
                command.Keywords, command.MetaDescription);

            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetProductWithCategory(command.Id);

            if (product == null)
                return operation.Faild(ApplicationMessages.RecordNotFound);

            if (_productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Faild(ApplicationMessages.DuplicatedRecord);

            var Slug = command.Slug.Slugify();
            var path = $"{product.Category.Slug}/{Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);


            product.Edit(command.Name, command.Code, command.ShortDescription, command.Description
                , picturePath, command.PictureAlt, command.PictureTitle, command.CategoryId, Slug,
                command.Keywords, command.MetaDescription);

            _productRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }
    }
}
