using AutoMapper;
using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction;
using Shared;
using Shared.IdentityDtos;
using Shared.OrderDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.AuthenticationServices
{
    public class AuthenticationService(UserManager<User> _userManager, IOptions<JwtOptions> options, IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailExists(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            return user is not null;
        }

        public async Task<AddressDto> GetUserAddress(string userEmail)
        {
            var user = await _userManager.Users
                .Include(u => u.Adress)
                .FirstOrDefaultAsync(u => u.Email == userEmail) ?? throw new UserNotFoundException(userEmail);
            return _mapper.Map<AddressDto>(user.Adress);
        }

        public async Task<UserResultDto> GetUserByEmail(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail) ?? throw new UserNotFoundException(userEmail);
            return new UserResultDto(user.DisplayName, await CreateTokenAsync(user), user.Email!);
        }

        public async Task<AddressDto> UpdateUserAddress(AddressDto addressDto, string userEmail)
        {
            var user = await _userManager.Users
                .Include(u => u.Adress)
                .FirstOrDefaultAsync(u => u.Email == userEmail) ?? throw new UserNotFoundException(userEmail);
            if(user.Adress is not null)
            {
                user.Adress.FirstName = addressDto.FirstName;
                user.Adress.LastName = addressDto.LastName;
                user.Adress.Street = addressDto.Street;
                user.Adress.City = addressDto.City;
                user.Adress.Country = addressDto.Country;
            }
            else
            {
                user.Adress = _mapper.Map<Adress>(addressDto);
            }
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }
            return _mapper.Map<AddressDto>(user.Adress);
        }

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
            var jwtOptions = options.Value;
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
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));
            var siginCareds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(jwtOptions.ExpirationInDays),
                signingCredentials: siginCareds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
