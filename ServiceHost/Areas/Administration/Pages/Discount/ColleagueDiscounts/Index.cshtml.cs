using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Application.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;
namespace ServiceHost.Areas.Administration.Pages.Discount.ColleagueDiscounts
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public ColleagueDiscountSearchModel searchModel;
        public List<ColleagueDiscountViewModel> ColleagueDiscounts;
        public SelectList Products;
        private readonly IProductApplication _productApplication;
        private readonly IColleagueDiscountApplication _coleagueDiscountApplication;

        public IndexModel(IProductApplication productApplication, IColleagueDiscountApplication coleagueDiscountApplication)
        {
            _productApplication = productApplication;
            _coleagueDiscountApplication = coleagueDiscountApplication;
        }

        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ColleagueDiscounts = _coleagueDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount
            {
                Products = _productApplication.GetProducts()
            };

            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
            var result = _coleagueDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var coleagueDiscount = _coleagueDiscountApplication.GetDetails(id);
            coleagueDiscount.Products = _productApplication.GetProducts();
            return Partial("Edit", coleagueDiscount);
        }

        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = _coleagueDiscountApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            _coleagueDiscountApplication.Remove(id);
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetRestore(long id)
        {
            _coleagueDiscountApplication.Restore(id);
            return RedirectToPage("./Index");
        }
    }
}
