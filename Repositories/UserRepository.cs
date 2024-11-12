////using System.Threading.Tasks;
////using Azure.Data.Tables;
////using Azure;
////using Microsoft.Azure.Documents;

////namespace st10293982CLD6212POE1.Repositories
////{
////    public class UserRepository : IUserRepository
////    {
////        private readonly TableClient _tableClient;

////        public UserRepository(IConfiguration configuration) 
////        {
////            var connectionString = configuration.GetSection("AzureTableStorage")["ConnectionString"];
////            var tableName = configuration.GetSection("AzureTableStorage")["TableName"];
////            _tableClient = new TableClient(connectionString, tableName);
////            _tableClient.CreateIfNotExistsAsync();
////        }

////        public async Task<User> GetUserByEmailAsync(string email)
////        {
////            try
////            {
////                var response = await _tableClient.GetEntityAsync<User>("User", email);
////                return response.Value;
////            }
////            catch (RequestFailedException ex) when (ex.Status == 404)
////            {
////                return null;
////            }
////        }

////        public async Task<bool> CreateUserAsync(UserRepository user)
////        {
////            try
////            {
////                await _tableClient.AddEntityAsync(user);
////                return true;
////            }
////            catch (RequestFailedException)
////            {
////                return false;
////            }
////        }
////    }
////}
//using Azure;
//using Azure.Data.Tables;
//using Microsoft.Azure.Documents;
//using Microsoft.Extensions.Configuration;
//using System.Threading.Tasks;

//namespace st10293982CLD6212POE1.Repositories
//{
//    public class UserRepository : IUserRepository
//    {
//        private readonly TableClient _tableClient;

//        public UserRepository(IConfiguration configuration)
//        {
//            var connectionString = configuration.GetSection("AzureTableStorage")["ConnectionString"];
//            var tableName = configuration.GetSection("AzureTableStorage")["TableName"];
//            _tableClient = new TableClient(connectionString, tableName);
//            _tableClient.CreateIfNotExistsAsync().GetAwaiter().GetResult();
//        }

//        public async Task<User> GetUserByEmailAsync(string email)
//        {
//            try
//            {
//                var response = await _tableClient.GetEntityAsync<User>("User", email);
//                return response.Value;
//            }
//            catch (RequestFailedException ex) when (ex.Status == 404)
//            {
//                return null;
//            }
//        }

//        public async Task<bool> CreateUserAsync(User user)
//        {
//            try
//            {
//                await _tableClient.AddEntityAsync(user);
//                return true;
//            }
//            catch (RequestFailedException)
//            {
//                return false;
//            }
//        }
//    }
//}

