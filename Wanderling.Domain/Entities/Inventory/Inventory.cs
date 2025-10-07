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
        public decimal MaxWeight { get; set; } = 100;
        public decimal CurrentWeight => Items.Sum(i => i.TotalWeight);
        public bool Overloaded => CurrentWeight > MaxWeight;

        public AddItemResult AddItem(ItemDefinition definition, int quantity)
        {
            // Guard clauses
            if (definition is null)
                return AddItemResult.Fail(InventoryError.InvalidDefinition);

            if (quantity < 0)
                return AddItemResult.Fail(InventoryError.InvalidQuantity);

            // Weight check
            var addWeight = definition.Weight * quantity;
            var prospectiveWeight = CurrentWeight + addWeight;

            if (prospectiveWeight > MaxWeight)
                return AddItemResult.Fail(InventoryError.ExceedsWeightLimit, prospectiveWeight);

            // Merge with an existing stack
            var stack = Items.FirstOrDefault(i => i.Item.Id == definition.Id);

            if (stack is not null)
            {
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

        public RemoveItemResult RemoveItem(Guid itemInstanceId, int quantity)
        {
            // Guard clauses
            if (quantity < 0)
                return RemoveItemResult.Fail(InventoryError.InvalidQuantity);

            var stack = Items.FirstOrDefault(i => i.Id == itemInstanceId);

            if (stack is null)
                return RemoveItemResult.Fail(InventoryError.ItemNotFound);

            // Policy: clamp removal to available quantity
            var removeQuantity = Math.Min(quantity, stack.Quantity);

            // Compute new weight BEFORE mutating collection
            var prospectiveWeight = CurrentWeight - (stack.Item.Weight * removeQuantity);
            if (prospectiveWeight < 0) prospectiveWeight = 0;

            if (removeQuantity == stack.Quantity)
            {
                // remove whole stack
                Items.Remove(stack);

                // no DTO because instance no longer exists
                return RemoveItemResult.Ok(null, removeQuantity, prospectiveWeight);
            }

            // decrease quantity (no overflow risk because 0 <= removeQty <= stack.Quantity)
            stack.Quantity -= removeQuantity;

            // NOTE: domain events (ItemRemoved/InventoryChanged) will be added later
            return RemoveItemResult.Ok(stack.ToDto(), removeQuantity, prospectiveWeight);
        }
    }
}
