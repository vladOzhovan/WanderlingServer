using Wanderling.Domain.Enums;
using Wanderling.Domain.Mappers;

namespace Wanderling.Domain.Entities.Inventory
{
    /// <summary>
    /// Represents a collection of User items, Domain entity
    /// </summary>
    public class Inventory
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<InventoryItem> Items { get; set; } = new();
        public float MaxWeight { get; set; } = 100;
        public float CurrentWeight => Items.Sum(i => i.TotalWeight);
        public bool Overencumbered => CurrentWeight > MaxWeight;

        public AddItemResult AddItem(ItemDefinition definition, int quantity)
        {
            // Guard clauses
            if (definition is null)
                return AddItemResult.Fail(InventoryError.InvalidDefinition);

            if (quantity < 0)
                return AddItemResult.Fail(InventoryError.InvalidQuantity);

            // Weight check
            var deltaWeight = definition.Weight * quantity;
            var prospectiveWeight = CurrentWeight + deltaWeight;

            if (prospectiveWeight > MaxWeight)
                return AddItemResult.Fail(InventoryError.ExceedsWeightLimit, prospectiveWeight);

            // Merge with an existing stack (same definition)
            var stack = Items.FirstOrDefault(i => i.Item.Id == definition.Id);

            if (stack is not null)
            {
                // TODO: enforce MaxStack policy here
                checked { stack.Quantity += quantity; } // prevent silent overflow
                // TODO: raise domain event ItemAdded / InventoryChanged
                return AddItemResult.Ok(stack.ToDto(), quantity, prospectiveWeight);
            }

            // Create new stack
            var newItem = new InventoryItem
            {
                Id = Guid.NewGuid(),
                UserId = UserId,
                Item = definition,
                Quantity = quantity
            };

            Items.Add(newItem);
            // TODO: raise domain event ItemAdded / InventoryChanged
            return AddItemResult.Ok(newItem.ToDto(), quantity, prospectiveWeight);
        }        
    }
}
