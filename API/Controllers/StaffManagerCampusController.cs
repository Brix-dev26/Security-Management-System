using Domain.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shared.Domain;
using System;
using System.Threading.Tasks;
using ViewModel;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffManagerCampusController : ControllerBase
    {
        private readonly ILogger<StaffManagerCampusController> _logger;
        private readonly IStaffManagerCampusSevices _staffManagerCampusService;
        private readonly IConfiguration _configuration;

        public StaffManagerCampusController(
            ILogger<StaffManagerCampusController> logger,
            IStaffManagerCampusSevices staffManagerCampusService,
            IConfiguration configuration)
        {
            _logger = logger;
            _staffManagerCampusService = staffManagerCampusService;
            _configuration = configuration;
        }

        [HttpPost("AddStaffManagerCampus")]
        public async Task<ServiceReturn<StaffManagerCampusViewModel>> AddStaffManagerCampus(StaffManagerCampusViewModel staffManagerCampusViewModel)
        {
            var apiReturn = new ServiceReturn<StaffManagerCampusViewModel>();

            try
            {
                var result = await _staffManagerCampusService.AddStaffManagerCampus(staffManagerCampusViewModel);

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
                apiReturn.AddError($"Error while adding security manager-campus association: {ex.Message}");
                _logger.LogError($"Error adding staff manager campus association: {ex.Message}");
            }

            return apiReturn;
        }
    }
}
