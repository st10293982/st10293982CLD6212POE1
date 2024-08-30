//using Azure;
//using Azure.Data.Tables;
//using System.ComponentModel.DataAnnotations;

//namespace st10293982CLD6212POE1.Models
//{
//    public class Order : ITableEntity
//    {
//        public int? Order_Id { get; set; }

//        [Required(ErrorMessage = "Please select a product.")]
//        public int Product_Id { get; set; }

//        [Required(ErrorMessage = "Please select a customer.")]
//        public int Customer_Id { get; set; }
//        public string Order_Location { get; set; }
//        public DateTime Order_Date {  get; set; }

//        public string? PartitionKey { get; set; }
//        public string? RowKey { get; set; }
//        public ETag ETag { get; set; }
//        public DateTimeOffset? Timestamp { get; set; }
//    }
//}
using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;

namespace st10293982CLD6212POE1.Models
{
    public class Order : ITableEntity
    {
        public int? Order_Id { get; set; } = 0;

        [Required(ErrorMessage = "Please select a product.")]
        public int Product_Id { get; set; } = 0;

        [Required(ErrorMessage = "Please select a customer.")]
        public int Customer_Id { get; set; } = 0;

        public string Order_Location { get; set; }
        public DateTime Order_Date { get; set; }

        public string? PartitionKey { get; set; }
        public string? RowKey { get; set; }
        public ETag ETag { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
    }
}

