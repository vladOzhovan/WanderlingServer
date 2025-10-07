using Wanderling.Domain.Dtos;
using Wanderling.Domain.Enums;

namespace Wanderling.Domain.Entities.Inventory
{
    public sealed record AddItemResult(bool Success, InventoryError Error, InventoryItemDto? Dto, int AddedQuantity, decimal NewWeight)
    {
        public static AddItemResult Ok(InventoryItemDto? dto, int addedQuantity, decimal newWeight)
                                    => new(true, InventoryError.None, dto, addedQuantity, newWeight);

        public static AddItemResult Fail(InventoryError err, decimal newWeight = 0)
                                    => new(false, err, null, 0, newWeight);
    }
}
