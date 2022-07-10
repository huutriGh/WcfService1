using BaseProject.DTO.User.Response;
using BaseProject.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Interface
{
    public interface IJwtService
    {
        Task<LoginResponse> GenerateJwtToken(User user);
        Task<dynamic> VerifyAndGenerateToken(string token, string refreshToken);
    }
}
