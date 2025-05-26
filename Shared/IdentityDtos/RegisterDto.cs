using System.ComponentModel.DataAnnotations;

namespace Shared.IdentityDtos
{
    public record RegisterDto
    {
        [EmailAddress]
        public string Email { get; init; }
        public string Password { get; init; }
        public string? PhoneNumber { get; init; }
        public string Username { get; init; }
        public string DisplayName { get; init; }
    }
}
