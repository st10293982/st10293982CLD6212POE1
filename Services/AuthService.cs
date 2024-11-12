//using Microsoft.Azure.Documents;
//using BCrypt.Net;
//using st10293982CLD6212POE1.Repositories;
//using st10293982CLD6212POE1.Models;

//namespace st10293982CLD6212POE1.Services
//{
//    public class AuthService : IAuthService
//    {
//        private readonly IUserRepository _userRepository;

//        public AuthService(IUserRepository userRepository)
//        {
//            _userRepository = userRepository;
//        }

//        public async Task<bool> RegisterAsync(string email, string password, string fullname)
//        {
//            var existingUser = await _userRepository.GetUserByEmailAsync(email);
//            if (existingUser != null)
            
//                return false;

//            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

//            var user = new User
//            {
//                RowKey = email,
//                Email = email,
//                FullName = fullname,
//                PasswordHash = passwordHash
//            };
//            return await _userRepository.CreateUserAsync(user);
//        }

//        public async Task<User> LoginAsync(string email, string password)
//        {
//            var user = await _userRepository.GetUserByEmailAsync(email);
//            if(user == null) return null;

//            bool isValid = BCrypt.Verify(password, user.PasswordHash);
//            return isValid ? user : null;
//        }
//    }
//}
