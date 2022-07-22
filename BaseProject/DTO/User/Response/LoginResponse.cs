using BaseProject.Models;
using BaseProject.Models.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.DTO.User.Response
{
    public class LoginResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
       

        public string RefreshToken { get; set; }

        public IEnumerable<string> UserRoles { get; set; }
    }
}
