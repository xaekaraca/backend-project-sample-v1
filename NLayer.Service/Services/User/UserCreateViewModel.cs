using NLayer.Core.DTOs;

namespace NLayer.Service.Services.User
{
    public class UserCreateViewModel :IBaseViewDto
    {
        public int Passoword { get; set; }
        public string Email { get; set; }
    }
}
