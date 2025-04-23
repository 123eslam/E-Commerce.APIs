using Shared.IdentityDtos;
using Shared.OrderDtos;

namespace Services.Abstraction
{
    public interface IAuthenticationService
    {
        public Task<UserResultDto> Login(LoginDto loginDto);
        public Task<UserResultDto> Register(RegisterDto registerDto);
        //Get current user
        public Task<UserResultDto> GetUserByEmail(string userEmail);
        //Check if email exists
        public Task<bool> CheckEmailExists(string userEmail);
        //Update user address
        public Task<AddressDto> UpdateUserAddress(AddressDto addressDto, string userEmail);
        //Get user address
        public Task<AddressDto> GetUserAddress(string userEmail);
    }
}
