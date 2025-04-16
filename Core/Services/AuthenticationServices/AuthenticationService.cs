using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction;
using Shared.IdentityDtos;

namespace Services.AuthenticationServices
{
    public class AuthenticationService(UserManager<User> _userManager) : IAuthenticationService
    {
        public async Task<UserResultDto> Login(LoginDto loginDto)
        {
            //Check email exists
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new Exception();
            //Check password
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result) throw new Exception();
            return new UserResultDto(user.DisplayName, "Token", user.Email);
        }

        public Task<UserResultDto> Register(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
