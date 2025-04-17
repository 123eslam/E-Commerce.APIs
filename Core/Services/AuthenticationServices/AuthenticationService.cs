using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction;
using Shared.IdentityDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.AuthenticationServices
{
    public class AuthenticationService(UserManager<User> _userManager) : IAuthenticationService
    {
        public async Task<UserResultDto> Login(LoginDto loginDto)
        {
            //Check email exists
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UnAuthorizedException();
            //Check password
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result) throw new UnAuthorizedException();
            return new UserResultDto(user.DisplayName, await CreateTokenAsync(user), user.Email!);
        }

        public async Task<UserResultDto> Register(RegisterDto registerDto)
        {
            var user = new User
            {
                Email = registerDto.Email,
                UserName = registerDto.Username,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }
            return new UserResultDto(user.DisplayName, await CreateTokenAsync(user), user.Email);
        }

        private async Task<string> CreateTokenAsync(User user)
        {
            //Private claims
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.Name, user.DisplayName)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //e7d3b21ae21ddcaa8fc19cb08129f96247dfb79fccc46ebb1504e39cf919672d10442dd89fdc2aeb2c642d03e26b163cb78631da45753da66cb5c2a5bff70a84c4d1f01ec49a19c0845128ca7e2483c224b3f3146645f02c16f44bce111a69bbd83ff231c89315eb063863998b02450b5bee0535fafeabef14d8518ec3759bc748e75727432d34ea2a99ca1315a27a15af8f43badeb1413eed5a7addaeea921c91d9a68707c6801b120b6c2bb06fc29c03e9de15bd745195d0690b100d3bd3b430d278b3a5faec83f0f287931fb9a39d22677243c7f640d850590a761b1b1f116d9e83cf0e9b3ec54490e2550faf093003fdd7eac331972f716ecde0e24929d1
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("e7d3b21ae21ddcaa8fc19cb08129f96247dfb79fccc46ebb1504e39cf919672d10442dd89fdc2aeb2c642d03e26b163cb78631da45753da66cb5c2a5bff70a84c4d1f01ec49a19c0845128ca7e2483c224b3f3146645f02c16f44bce111a69bbd83ff231c89315eb063863998b02450b5bee0535fafeabef14d8518ec3759bc748e75727432d34ea2a99ca1315a27a15af8f43badeb1413eed5a7addaeea921c91d9a68707c6801b120b6c2bb06fc29c03e9de15bd745195d0690b100d3bd3b430d278b3a5faec83f0f287931fb9a39d22677243c7f640d850590a761b1b1f116d9e83cf0e9b3ec54490e2550faf093003fdd7eac331972f716ecde0e24929d1"));
            var siginCareds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "https://localhost:44353/",
                audience: "My auidence",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: siginCareds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
