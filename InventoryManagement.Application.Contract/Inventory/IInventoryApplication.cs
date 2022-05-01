using _0_FrameWork.Application;
using System.Collections.Generic;

namespace InventoryManagement.Application.Contract.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        OperationResult Increase(IncreaseInventory command);
        OperationResult Reduce(List<ReduceInventory> command);
        OperationResult Reduce(ReduceInventory command);
        EditInventory GetDetails(long Id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        List<InventoryOperationViewModel> GetOperationLog(long inventoryId);
    }
}
