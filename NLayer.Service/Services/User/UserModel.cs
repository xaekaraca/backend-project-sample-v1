using NLayer.Core.DTOs;

namespace NLayer.Service.Services.User
{
    public class UserModel :IBaseModelDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] JwtToken { get; set; }
        public byte[] RefreshToken { get; set; }
    }
}
