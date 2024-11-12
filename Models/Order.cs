using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;

namespace st10293982CLD6212POE1.Models
{
    public class Order : ITableEntity
    {
        public int Order_Ids { get; set; } = 0;

        [Required(ErrorMessage = "Please select a product.")]
        public int Product_Ids { get; set; } = 0;

        [Required(ErrorMessage = "Please select a customer.")]
        public int Customer_Ids { get; set; } = 0;

        public string Order_Locations { get; set; } = string.Empty;
        public DateTime Order_Dates { get; set; } = DateTime.UtcNow;

        public string PartitionKeys { get; set; } = "OrdersPartition";
        public string RowKeys { get; set; } = Guid.NewGuid().ToString();
        public ETag ETag { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
    }
}
