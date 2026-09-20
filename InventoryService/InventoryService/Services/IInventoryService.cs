using InventoriesService.DTOs;

namespace InventoriesService.Services
{
    public interface IInventoriesService
    {
        Task<InventoryResponse?> GetByProductIdAsync(
       int productId);

        Task<InventoryResponse> AddInventoryAsync(
            InventoryRequest request);

        Task<bool> ReserveInventoryAsync(
            int productId,
            int quantity);

        Task<bool> ReleaseInventoryAsync(
            int productId,
            int quantity);
    }
}
