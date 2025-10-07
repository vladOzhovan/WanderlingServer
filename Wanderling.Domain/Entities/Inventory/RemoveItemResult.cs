using Wanderling.Domain.Dtos;
using Wanderling.Domain.Enums;

namespace Wanderling.Domain.Entities.Inventory
{
    public sealed record RemoveItemResult(bool Success, InventoryError Error, InventoryItemDto? Dto, int RemoveQuantity, decimal NewWeight)
    {
        public static RemoveItemResult Ok(InventoryItemDto? dto, int removeQuantity, decimal newWeight)
                    => new(true, InventoryError.None, dto, removeQuantity, newWeight);

        public static RemoveItemResult Fail(InventoryError err, decimal newWeight = 0) 
                    => new(false, err, null, 0, newWeight);
    }
}
