using Wanderling.Domain.Dtos;
using Wanderling.Domain.Enums;

namespace Wanderling.Domain.Entities.Inventory
{
    public sealed record AddItemResult(bool Success, InventoryError Error, InventoryItemDto? Item, int AddedQuantity, float NewWeight)
    {
        public static AddItemResult Fail(InventoryError error, float newWeight = 0) 
                                         => new(false, error, null, 0, newWeight);
        public static AddItemResult Ok(InventoryItemDto? dto, int addedQuantity, float newWeight) 
                                       => new(true, InventoryError.None, dto, addedQuantity, newWeight);
    }
}
