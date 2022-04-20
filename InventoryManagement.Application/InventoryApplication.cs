using _0_FrameWork.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.Inventory.Agg;
using System;
using System.Collections.Generic;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operation = new OperationResult();
            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId))
                operation.Faild(ApplicationMessages.DuplicatedRecord);

            var inventory = new Inventory(command.ProductId, command.UnitPrice);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.Id);
            if (inventory != null)
                operation.Faild(ApplicationMessages.RecordNotFound);

            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
                operation.Faild(ApplicationMessages.DuplicatedRecord);

            inventory.Edit(command.ProductId, command.UnitPrice);
            _inventoryRepository.SaveChanges();

            return operation.Succeeded();
        }

        public EditInventory GetDetails(long Id)
        {
            return _inventoryRepository.GetDetails(Id);
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory != null)
                operation.Faild(ApplicationMessages.RecordNotFound);

            const int operatorId = 1;

            inventory.Increase(command.Count, operatorId, command.Description);

            return operation.Succeeded();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory != null)
                operation.Faild(ApplicationMessages.RecordNotFound);

            const int operatorId = 1;

            inventory.Reduce(command.Count,operatorId,command.Description,0);

            return operation.Succeeded();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            const int operatorId = 1;

            var operation = new OperationResult();

            foreach (var item in command)
            {
                var inventory = _inventoryRepository.GetBy(item.ProductId);
                inventory.Reduce(item.Count, operatorId, item.Description, item.OrderId);
            }

            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);
        }
    }
}

