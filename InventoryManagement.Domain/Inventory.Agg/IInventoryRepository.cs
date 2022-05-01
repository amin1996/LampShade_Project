using _0_FrameWork.Domain;
using InventoryManagement.Application.Contract.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Inventory.Agg
{
    public interface IInventoryRepository : IRepository<long, Inventory>
    {
        EditInventory GetDetails(long Id);
        Inventory GetBy(long ProductId);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        List<InventoryOperationViewModel> GetOperationLog(long InventoryId);
    }
}
