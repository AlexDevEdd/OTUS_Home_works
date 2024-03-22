namespace _Project.Scripts.Inventory
{
    public readonly struct RemoveItemsFromInventoryResult
    {
        public readonly string InventoryOwnerId;
        public readonly int ItemsToRemoveAmount;
        public readonly bool IsSuccess;

        public RemoveItemsFromInventoryResult(string inventoryOwnerId, int itemsToRemoveAmount, bool isSuccess)
        {
            InventoryOwnerId = inventoryOwnerId;
            ItemsToRemoveAmount = itemsToRemoveAmount;
            IsSuccess = isSuccess;
        }
    }
}