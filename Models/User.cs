﻿//using Azure;
//using Azure.Data.Tables;
//using System.ComponentModel.DataAnnotations;

//namespace st10293982CLD6212POE1.Models
//{
//    public class User : ITableEntity
//    {
//        [Key]
//        public string PartitionKey { get; set; }
//        [Key]
//        public string RowKey { get; set; }

//        public string PasswordHash { get; set; }    

//        public string Email { get; set; }

//        public string FullName { get; set; }    

//        public DateTimeOffset? Timestamp { get ; set ; }
//        public ETag ETag { get; set ; }

//        public User()
//        {
//            PartitionKey = "User";
//        }
//    }
//}
