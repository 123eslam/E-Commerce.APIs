using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.IdentityDtos;
using System.Net;

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
    }
}
