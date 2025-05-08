using Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Shared.Domain;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using ViewModel;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffManagerController : ControllerBase
    {
        private readonly ILogger<StaffManagerController> _logger;
        private readonly IStaffManagerService _staffManagerService;
        private readonly IConfiguration _configuration;

        public StaffManagerController(
            ILogger<StaffManagerController> logger,
            IStaffManagerService staffManagerService,
            IConfiguration configuration)
        {
            _logger = logger;
            _staffManagerService = staffManagerService;
            _configuration = configuration;
        }

        [HttpPost("AddStaffManager")]
        public async Task<ServiceReturn<StaffManagerViewModel>> AddStaffManager(StaffManagerViewModel staffManagerViewModel)
        {
            var apiReturn = new ServiceReturn<StaffManagerViewModel>();

            try
            {
                var result = await _staffManagerService.AddStaffManager(staffManagerViewModel);

                if (result.HasErrors)
                {
                    apiReturn.Errors = result.Errors;
                }
                else
                {
                    apiReturn.Data = result.Data;
                    apiReturn.Count = 1;
                }
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"Error while adding staff manager: {ex.Message}");
                _logger.LogError($"Error adding staff manager: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpPost("Login")]
        public async Task<ServiceReturn<string>> Login([FromBody] StaffManagerViewModel loginModel)
        {
            var apiReturn = new ServiceReturn<string>();

            try
            {
                var result = await _staffManagerService.Login(loginModel.UserId, loginModel.Password);

                if (result.HasErrors)
                {
                    apiReturn.Errors = result.Errors;
                }
                else
                {
                    var staffManager = result.Data;

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Convert.FromBase64String(_configuration["Jwt:Key"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                            new Claim("StaffManagerId", staffManager.StaffManagerId.ToString()),
                            new Claim("Name", staffManager.Name),
                            new Claim("UserRole", staffManager.Role),
                        }),

                        Expires = DateTime.UtcNow.AddHours(2),
                        Issuer = _configuration["Jwt:Issuer"],
                        Audience = _configuration["Jwt:Issuer"],
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    apiReturn.Data = tokenString;
                }
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"Error: {ex.Message}");
                _logger.LogError($"Error logging in staff manager: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpGet("GetStaffManagerById/{staffManagerId}")]
        public async Task<ServiceReturn<StaffManagerViewModel>> GetStaffManagerById(int staffManagerId)
        {
            var apiReturn = new ServiceReturn<StaffManagerViewModel>();

            try
            {
                var result = await _staffManagerService.GetStaffManagerById(staffManagerId);

                if (result.HasErrors)
                {
                    apiReturn.Errors = result.Errors;
                }
                else
                {
                    apiReturn.Data = result.Data;
                    apiReturn.Count = 1;
                }
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"Error while searching for staff manager: {ex.Message}");
                _logger.LogError($"Error retrieving staff manager: {ex.Message}");
            }

            return apiReturn;
        }
    }
}
