using Azure.Data.Tables;
using Azure;
using System.ComponentModel.DataAnnotations;

namespace st10293982CLD6212POE1.Models
{
    public class Product : ITableEntity
    {
        [Key]
        public int Product_Id { get; set; }
        public string? Name { get; set; }
        public int? Price { get; set; }
        public string? Description { get; set; }
        public string? Image {  get; set; }

        public string? PartitionKey {  get; set; }
        public string? RowKey { get; set; }
        public ETag ETag { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
    }
}
