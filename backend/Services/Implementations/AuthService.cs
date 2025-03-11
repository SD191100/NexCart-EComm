using BCrypt.Net;
using NexCart.Helpers;
using NexCart.Models;
using NexCart.Repositories.Interfaces;
using NexCart.Services.Interfaces;
using NexCart.Repositories.Implementations;

namespace NexCart.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly JwtHelper _jwtHelper;

        public AuthService(IUserRepository userRepository, JwtHelper jwtHelper, ISellerRepository sellerRepository)
        {
            _userRepository = userRepository;
            _sellerRepository = sellerRepository;

            _jwtHelper = jwtHelper;
        }

        // Registration function
        public void Register(string email, string password, string role, string firstName, string lastName, string? contactNumber, string? companyName, string? gstNumber)
        {
            if (_userRepository.GetUserByEmail(email) != null)
                throw new InvalidOperationException("User already exists");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Email = email,
                PasswordHash = hashedPassword,
                Role = role,
                FirstName = firstName,
                LastName = lastName,
                ContactNumber = contactNumber
            };

            if (role == "Seller")
            {
                var seller = new Seller
                {
                    CompanyName = companyName,
                    GSTNumber = gstNumber,
                    User = user // Link the seller to the user
                };

                _sellerRepository.AddSeller(seller);
            }
            else
            {
                _userRepository.Add(user);
            }
        }

        public void Register(string email, string password, string role, string firstName, string lastName)
        {
            if (_userRepository.GetUserByEmail(email) != null)
                throw new InvalidOperationException("User already exists");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Email = email,
                PasswordHash = hashedPassword,
                Role = role,
                FirstName = firstName,
                LastName = lastName,
                //ContactNumber = contactNumber
            };
            _userRepository.Add(user);
        }


        // Login function
        public string Authenticate(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            return _jwtHelper.GenerateToken(user.UserId, user.Role);
        }
    }
}

