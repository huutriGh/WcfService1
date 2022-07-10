using BaseProject.DTO.JWT;
using BaseProject.DTO.User.Request;
using BaseProject.DTO.User.Response;
using BaseProject.Interface;
using BaseProject.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJwtService _JwtService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(IJwtService jwtService, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._JwtService = jwtService;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        [HttpPost]
        [Route("Login")]
        [SwaggerOperation(
                    Summary = "Login to System",
                    Description = "Login to System",
                    OperationId = "UserController.Login",
                    Tags = new[] { "UserController" })]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _userManager.Users.Where(u => u.UserName == request.UserName).FirstOrDefault();


                if (existingUser == null)
                {
                    return BadRequest(new LoginResponse());
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, request.Password);


                if (!isCorrect)
                {
                    return BadRequest();
                }

                var jwtToken = await _JwtService.GenerateJwtToken(existingUser);

                return Ok(jwtToken);
            }

            return BadRequest(new LoginResponse());
        }
        [HttpPost]
        [Route("Register")]
        [SwaggerOperation(
        Summary = "Register New Account",
        Description = "Register New Account",
        OperationId = "UserController.Register",
        Tags = new[] { "UserController" })
    ]
        public async Task<ActionResult<LoginResponse>> HandleAsync(RegisterRequest request)
        {
            if (ModelState.IsValid)
            {

                var existingUser = await _userManager.FindByNameAsync(request.Username);
                if (existingUser != null)
                {
                    return BadRequest(new LoginResponse());
                }

                var newUser = new User() { UserName = request.Username };
                var isCreated = await _userManager.CreateAsync(newUser, request.Password);

                if (isCreated.Succeeded)
                {


                    bool x = await _roleManager.RoleExistsAsync("GUEST");
                    if (!x)
                    {
                        // first we create Admin rool    
                        var role = new IdentityRole
                        {
                            Name = "GUEST"
                        };
                        await _roleManager.CreateAsync(role);

                    }

                    var result1 = await _userManager.AddToRoleAsync(newUser, "GUEST");
                    existingUser = _userManager.Users.Where(u => u.UserName == request.Username).FirstOrDefault();
                    var jwtToken = await _JwtService.GenerateJwtToken(existingUser);
                    return Ok(jwtToken);




                }
                else
                {
                    return BadRequest(new LoginResponse());
                }

            }

            return BadRequest(new LoginResponse());
        }
        [HttpPost]
        [Route("RefreshToken")]
        [SwaggerOperation(
         Summary = "Get new Access Token",
         Description = "Get new Access Token",
         OperationId = "Auth.RefreshToken",
         Tags = new[] { "AuthEndpoints" })
     ]
        public async  Task<ActionResult<dynamic>> RefreshToken(RenewToken request)
        {
            if (ModelState.IsValid)
            {
                var result = await _JwtService.VerifyAndGenerateToken(request.Token, request.RefreshToken);

                if (result == null)
                {
                    return BadRequest(new
                    {
                        Errors = new List<string>() {
                            "Invalid tokens"
                        },
                        Success = false
                    });
                }

                return Ok(result);
            }

            return BadRequest(new
            {
                Errors = new List<string>() {
                    "Invalid payload"
                },
                Success = false
            });
        }
    }
}
