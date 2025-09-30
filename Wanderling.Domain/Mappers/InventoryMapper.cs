using Wanderling.Domain.Dtos;
using Wanderling.Domain.Entities.Inventory;

namespace Wanderling.Domain.Mappers
{
    public static class InventoryMapper
    {
        public static InventoryItemDto ToDto(this InventoryItem entity) => new InventoryItemDto
        {
            ItemInstanceId = entity.Id,
            DefinitionId = entity.Item.Id,
            Name = entity.Item.Name,
            Quantity = entity.Quantity,
            TotalWeight = entity.TotalWeight
        };
    }
}
