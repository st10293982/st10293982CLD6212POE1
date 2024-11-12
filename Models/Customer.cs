using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;

namespace st10293982CLD6212POE1.Models
{
    public class Customer : ITableEntity
    {
        [Key]
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Email {  get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }

        public string? PartitionKey { get; set; }
        public string? RowKey { get; set; }
        public ETag ETag { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
    }
}
