using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;
namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public InventorySearchModel searchModel;
        public List<InventoryViewModel> Inventory;
        public SelectList Products;
        private readonly IProductApplication _ProductApplication;
        private readonly IInventoryApplication _InventoryApplication;

        public IndexModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _ProductApplication = productApplication;
            _InventoryApplication = inventoryApplication;
        }

        public void OnGet(InventorySearchModel searchModel)
        {
            Products = new SelectList(_ProductApplication.GetProducts(), "Id", "Name");
            Inventory = _InventoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory
            {
                Products = _ProductApplication.GetProducts()
            };

            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = _InventoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var inventory = _InventoryApplication.GetDetails(id);
            inventory.Products = _ProductApplication.GetProducts();
            return Partial("Edit", inventory);
        }

        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _InventoryApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetIncrease(long id)
        {
            var command = new IncreaseInventory
            {
                InventoryId = id
            };
            return Partial("Increase", command);
        }

        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            var result = _InventoryApplication.Increase(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetReduce(long id)
        {
            var command = new ReduceInventory
            {
                InventoryId = id
            };
            return Partial("Reduce", command);
        }

        public JsonResult OnPostReduce(ReduceInventory command)
        {
            var result = _InventoryApplication.Reduce(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetLog(long id)
        {
            var log = _InventoryApplication.GetOperationLog(id);
            return Partial("OperationLog", log);
        }
    }
}
