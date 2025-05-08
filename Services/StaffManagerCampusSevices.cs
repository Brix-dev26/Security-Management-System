using Microsoft.Extensions.Logging;
using Shared.Domain;
using System.Linq;
using System.Threading.Tasks;
using ViewModel;
using Infrastructure;
using Domain.Entities;
using Domain.IServices;

namespace Services
{
    public class StaffManagerCampusService : IStaffManagerCampusSevices
    {
        private readonly IRepository<StaffManagerCampus> _staffManagerCampusRepository;
        private readonly IRepository<StaffManager> _staffManagerRepository;
        private readonly IRepository<Campus> _campusRepository;
        private readonly ILogger<StaffManagerCampusService> _logger;

        public StaffManagerCampusService(
            IRepository<StaffManagerCampus> staffManagerCampusRepository,
            IRepository<StaffManager> staffManagerRepository,
            IRepository<Campus> campusRepository,
            ILogger<StaffManagerCampusService> logger)
        {
            _staffManagerCampusRepository = staffManagerCampusRepository;
            _staffManagerRepository = staffManagerRepository;
            _campusRepository = campusRepository;
            _logger = logger;
        }

        public async Task<ServiceReturn<StaffManagerCampusViewModel>> AddStaffManagerCampus(StaffManagerCampusViewModel viewModel)
        {
            var serviceReturn = new ServiceReturn<StaffManagerCampusViewModel>();

            try
            {
                var staffManager = await _staffManagerRepository.FindBy(s => s.StaffManagerId == viewModel.StaffManagerId);
                if (staffManager == null || !staffManager.Any())
                {
                    serviceReturn.AddError("The specified security manager does not exist.");
                    return serviceReturn;
                }

                var campus = await _campusRepository.FindBy(c => c.CampusId == viewModel.CampusId);
                if (campus == null || !campus.Any())
                {
                    serviceReturn.AddError("The specified campus does not exist.");
                    return serviceReturn;
                }

                var staffManagerCampus = new StaffManagerCampus
                {
                    StaffManagerId = viewModel.StaffManagerId,
                    CampusId = viewModel.CampusId
                };

                await _staffManagerCampusRepository.Insert(staffManagerCampus);
                serviceReturn.Data = viewModel;
            }
            catch (System.Exception ex)
            {
                serviceReturn.AddTechnicalInsertError();
                _logger.LogError($"Error while linking the security manager to the campus: {ex.Message}");
            }

            return serviceReturn;
        }
    }
}
