using InventoryService.Data;
using InventoryService.DTOs;
using InventoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Services
{
  
        public class InventoryService : IInventoryService
        {
            private readonly InventoryDbContext _context;

            public InventoryService(
                InventoryDbContext context)
            {
                _context = context;
            }

            public async Task<InventoryResponse?>
                GetByProductIdAsync(int productId)
            {
                var inventory = await _context.Inventories
                    .FirstOrDefaultAsync(x =>
                        x.ProductId == productId);

                if (inventory == null)
                    return null;

                return Map(inventory);
            }

            public async Task<InventoryResponse>
                AddInventoryAsync(InventoryRequest request)
            {
                var inventory =
                    await _context.Inventories
                        .FirstOrDefaultAsync(x =>
                            x.ProductId == request.ProductId);

                if (inventory == null)
                {
                    inventory = new Inventory
                    {
                        ProductId = request.ProductId,
                        QuantityAvailable = request.Quantity,
                        QuantityReserved = 0,
                        UpdatedAt = DateTime.UtcNow
                    };

                    _context.Inventories.Add(inventory);
                }
                else
                {
                    inventory.QuantityAvailable += request.Quantity;
                    inventory.UpdatedAt = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();

                return Map(inventory);
            }

            public async Task<bool> ReserveInventoryAsync(
                int productId,
                int quantity)
            {
                var inventory =
                    await _context.Inventories
                        .FirstOrDefaultAsync(x =>
                            x.ProductId == productId);

                if (inventory == null)
                    return false;

                if (inventory.QuantityAvailable < quantity)
                    return false;

                inventory.QuantityAvailable -= quantity;
                inventory.QuantityReserved += quantity;
                inventory.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return true;
            }

            public async Task<bool> ReleaseInventoryAsync(
                int productId,
                int quantity)
            {
                var inventory =
                    await _context.Inventories
                        .FirstOrDefaultAsync(x =>
                            x.ProductId == productId);

                if (inventory == null)
                    return false;

                if (inventory.QuantityReserved < quantity)
                    return false;

                inventory.QuantityReserved -= quantity;
                inventory.QuantityAvailable += quantity;
                inventory.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return true;
            }

            private static InventoryResponse Map(
                Inventory inventory)
            {
                return new InventoryResponse
                {
                    Id = inventory.Id,
                    ProductId = inventory.ProductId,
                    QuantityAvailable =
                        inventory.QuantityAvailable,
                    QuantityReserved =
                        inventory.QuantityReserved
                };
            }
        }
}
