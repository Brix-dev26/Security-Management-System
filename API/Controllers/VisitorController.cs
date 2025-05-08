using Domain.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
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
    public class VisitorController : ControllerBase
    {
        private readonly ILogger<VisitorController> _logger;
        private readonly IVisitorService _visitorService;
        private readonly IConfiguration _configuration;

        public VisitorController(ILogger<VisitorController> logger, IVisitorService visitorService, IConfiguration configuration)
        {
            _logger = logger;
            _visitorService = visitorService;
            _configuration = configuration;
        }

        [HttpPost("AddVisitor")]
        public async Task<ServiceReturn<string>> AddVisitor(VisitorViewModel visitorViewModel)
        {
            var apiReturn = new ServiceReturn<string>();

            try
            {
                var result = await _visitorService.AddVisitor(visitorViewModel);

                if (result.HasErrors)
                {
                    apiReturn.Errors = result.Errors;
                }
                else
                {
                    var visitor = result.Data;

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

                    var claims = new List<Claim>
                    {
                        new Claim("VisitorId", visitor.VisitorId.ToString()),
                        new Claim("phone_no", visitor.phone_no ?? ""),
                        new Claim("name", visitor.name ?? ""),
                        new Claim("E_mail", visitor.E_mail ?? ""),
                        new Claim("nationality", visitor.nationality ?? ""),
                        new Claim("Role", "Visitor")
                    };

                    if (visitor.NationalIdCard.HasValue)
                    {
                        claims.Add(new Claim("NationalIdCard", visitor.NationalIdCard.Value.ToString()));
                    }
                    if (!string.IsNullOrEmpty(visitor.passport_no))
                    {
                        claims.Add(new Claim("passport_no", visitor.passport_no));
                    }

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.UtcNow.AddHours(2),
                        Issuer = _configuration["Jwt:Issuer"],
                        Audience = _configuration["Jwt:Issuer"],
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    apiReturn.Data = tokenHandler.WriteToken(token);

                    _logger.LogInformation($"Token generated for VisitorId: {visitor.VisitorId}");
                }
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"Error while adding visitor: {ex.Message}");
                _logger.LogError($"Error adding visitor: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpGet("GetVisitorsByPhoneNumber/{contactNumber}")]
        public async Task<ServiceReturn<List<VisitorViewModel>>> GetVisitorsByPhoneNumber(string contactNumber)
        {
            var apiReturn = new ServiceReturn<List<VisitorViewModel>>();

            try
            {
                if (string.IsNullOrWhiteSpace(contactNumber))
                {
                    apiReturn.AddError("Phone number is required.");
                    return apiReturn;
                }

                var result = await _visitorService.GetVisitorsByPhoneNumber(contactNumber);
                apiReturn.Data = result.Data;
                apiReturn.Count = result.Data?.Count ?? 0;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"Error retrieving visitors by phone number: {ex.Message}");
                _logger.LogError($"Error retrieving visitors by phone number: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpGet("GetVisitorsByNationalIdCard/{nationalIdCard}")]
        public async Task<ServiceReturn<List<VisitorViewModel>>> GetVisitorsByNationalIdCard(string nationalIdCard)
        {
            var apiReturn = new ServiceReturn<List<VisitorViewModel>>();

            try
            {
                if (string.IsNullOrWhiteSpace(nationalIdCard))
                {
                    apiReturn.AddError("National ID number is required.");
                    return apiReturn;
                }

                var result = await _visitorService.GetVisitorsByNationalIdCard(nationalIdCard);
                apiReturn.Data = result.Data;
                apiReturn.Count = result.Data?.Count ?? 0;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"Error retrieving visitors by National ID number: {ex.Message}");
                _logger.LogError($"Error retrieving visitors by National ID number: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpGet("GetVisitorsByPassportId/{passportId}")]
        public async Task<ServiceReturn<List<VisitorViewModel>>> GetVisitorsByPassportId(string passportId)
        {
            var apiReturn = new ServiceReturn<List<VisitorViewModel>>();

            try
            {
                if (string.IsNullOrWhiteSpace(passportId))
                {
                    apiReturn.AddError("Passport number is required.");
                    return apiReturn;
                }

                var result = await _visitorService.GetVisitorsByPassportId(passportId);
                apiReturn.Data = result.Data;
                apiReturn.Count = result.Data?.Count ?? 0;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"Error retrieving visitors by passport number: {ex.Message}");
                _logger.LogError($"Error retrieving visitors by passport number: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpPost("UpdateVisitorById")]
        public async Task<ServiceReturn<string>> UpdateVisitorById([FromBody] VisitorViewModel visitorViewModel)
        {
            var apiReturn = new ServiceReturn<string>();

            try
            {
                if (visitorViewModel.VisitorId <= 0)
                {
                    apiReturn.AddError("Visitor ID is invalid.");
                    return apiReturn;
                }

                var result = await _visitorService.UpdateVisitorById(visitorViewModel);
                apiReturn.Data = result.Data;
                apiReturn.Count = result.Count;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"Error updating visitor data: {ex.Message}");
                _logger.LogError($"Error updating visitor data: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpGet("GetVisitorById/{visitorId}")]
        public async Task<ServiceReturn<VisitorViewModel>> GetVisitorById(int visitorId)
        {
            var apiReturn = new ServiceReturn<VisitorViewModel>();

            try
            {
                if (visitorId <= 0)
                {
                    apiReturn.AddError("Visitor ID is invalid.");
                    return apiReturn;
                }

                var result = await _visitorService.GetVisitorById(visitorId);
                apiReturn.Data = result.Data;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"Error retrieving visitor: {ex.Message}");
                _logger.LogError($"Error retrieving visitor: {ex.Message}");
            }

            return apiReturn;
        }

        [HttpGet("GetBlacklistedVisitors")]
        public async Task<ServiceReturn<List<VisitorViewModel>>> GetBlacklistedVisitors()
        {
            var apiReturn = new ServiceReturn<List<VisitorViewModel>>();

            try
            {
                var result = await _visitorService.GetBlacklistedVisitors();
                apiReturn.Data = result.Data;
                apiReturn.Errors = result.Errors;
            }
            catch (Exception ex)
            {
                apiReturn.AddError($"Error retrieving blacklisted visitors: {ex.Message}");
                _logger.LogError($"Error retrieving blacklisted visitors: {ex.Message}");
            }

            return apiReturn;
        }
    }
}
