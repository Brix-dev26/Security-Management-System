using Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Shared.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityStaffController : ControllerBase
    {
        private readonly ILogger<SecurityStaffController> _logger;
        private readonly ISecurityStaffService _securityStaffService;
        private readonly IConfiguration _configuration;

        public SecurityStaffController(
            ILogger<SecurityStaffController> logger,
            ISecurityStaffService securityStaffService,
            IConfiguration configuration)
        {
            _logger = logger;
            _securityStaffService = securityStaffService;
            _configuration = configuration;
        }

        [Authorize(Roles = "Computer Security Officer")]
        [HttpPost("AddSecurityStaff")]
        public async Task<ServiceReturn<SecurityStaffViewModel>> AddSecurityStaff(SecurityStaffViewModel securityStaffViewModel)
        {
            var apiReturn = new ServiceReturn<SecurityStaffViewModel>();

            try
            {
                var result = await _securityStaffService.AddSecurityStaff(securityStaffViewModel);

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
                apiReturn.AddError($"An error occurred while adding security staff: {ex.Message}");
                _logger.LogError($"Error adding security staff: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpGet("GetSecurityStaffById/{secId}")]
        public async Task<ServiceReturn<SecurityStaffViewModel>> GetSecurityStaffById(int secId)
        {
            var apiReturn = new ServiceReturn<SecurityStaffViewModel>();

            try
            {
                var result = await _securityStaffService.GetSecurityStaffById(secId);

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
                apiReturn.AddError($"An error occurred while retrieving security staff: {ex.Message}");
                _logger.LogError($"Error retrieving security staff: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpPost("Login")]
        public async Task<ServiceReturn<string>> Login([FromBody] SecurityStaffLoginViewModel loginModel)
        {
            var apiReturn = new ServiceReturn<string>();

            try
            {
                var result = await _securityStaffService.Login(loginModel.user_id, loginModel.password);

                if (result.HasErrors)
                {
                    apiReturn.Errors = result.Errors;
                }
                else
                {
                    var staff = result.Data;

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Convert.FromHexString(_configuration["Jwt:Key"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                            new Claim("Sec_ID", staff.Sec_ID.ToString()),
                            new Claim("Name", staff.Name ?? staff.Sec_ID.ToString()),
                            new Claim("UserRole", staff.Role)
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
                _logger.LogError($"Error logging in security staff: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpPost("UpdateGateBySecurityId/{secId}/{gateId}")]
        public async Task<ServiceReturn<bool>> UpdateGateBySecurityId(int secId, int gateId)
        {
            _logger.LogInformation($"Received secId: {secId}, gateId: {gateId}");
            var apiReturn = new ServiceReturn<bool>();

            try
            {
                var result = await _securityStaffService.UpdateGateBySecurityId(secId, gateId);

                if (result.HasErrors)
                {
                    apiReturn.Errors = result.Errors;
                }
                else
                {
                    apiReturn.Data = true;
                    apiReturn.Count = 1;
                }
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"An error occurred while updating the gate: {ex.Message}");
                _logger.LogError($"Error updating gate: {ex.Message}");
            }

            return apiReturn;
        }
    }
}
