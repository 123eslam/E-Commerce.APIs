using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.IdentityDtos;
using Shared.OrderDtos;
using System.Net;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager ServiceManager) : ApiController
    {
        [HttpPost("Register")]
        [ProducesResponseType(typeof(UserResultDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserResultDto>> Register(RegisterDto registerDto)
        {
            var user = await ServiceManager.AuthenticationService.Register(registerDto);
            return Ok(user);
        }
        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserResultDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto)
        {
            var user = await ServiceManager.AuthenticationService.Login(loginDto);
            return Ok(user);
        }
        [HttpGet("EmailExists")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            var result = await ServiceManager.AuthenticationService.CheckEmailExists(email);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(UserResultDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserResultDto>> GetUserByEmail()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await ServiceManager.AuthenticationService.GetUserByEmail(email);
            return Ok(user);
        }
        [Authorize]
        [HttpGet("Address")]
        [ProducesResponseType(typeof(AddressDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AddressDto>> GetAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await ServiceManager.AuthenticationService.GetUserAddress(email);
            return Ok(address);
        }
        [Authorize]
        [HttpPut("Address")]
        [ProducesResponseType(typeof(AddressDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await ServiceManager.AuthenticationService.UpdateUserAddress(addressDto, email);
            return Ok(address);
        }
    }
}
