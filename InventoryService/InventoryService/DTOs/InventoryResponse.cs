namespace InventoriesService.DTOs
{
    public class InventoryResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int QuantityAvailable { get; set; }
        public int QuantityReserved { get; set; }
    }
}
