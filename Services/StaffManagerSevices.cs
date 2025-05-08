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
    public class StaffManagerService : IStaffManagerService
    {
        private readonly IRepository<StaffManager> _staffManagerRepository;
        private readonly IRepository<Campus> _campusRepository;
        private readonly ILogger<StaffManagerService> _logger;

        public StaffManagerService(
            IRepository<StaffManager> staffManagerRepository,
            IRepository<Campus> campusRepository,
            ILogger<StaffManagerService> logger)
        {
            _staffManagerRepository = staffManagerRepository;
            _campusRepository = campusRepository;
            _logger = logger;
        }

        public async Task<ServiceReturn<StaffManagerViewModel>> AddStaffManager(StaffManagerViewModel viewModel)
        {
            var serviceReturn = new ServiceReturn<StaffManagerViewModel>();

            try
            {
                if (!string.IsNullOrEmpty(viewModel.ContactNumber))
                {
                    if (!viewModel.ContactNumber.All(char.IsDigit) || viewModel.ContactNumber.Length != 11)
                    {
                        serviceReturn.AddError("The phone number must consist of 11 digits.");
                        return serviceReturn;
                    }

                    var existingPhone = (await _staffManagerRepository.FindBy(s => s.ContactNumber == viewModel.ContactNumber)).FirstOrDefault();
                    if (existingPhone != null)
                    {
                        serviceReturn.AddError("The phone number is already registered.");
                        return serviceReturn;
                    }
                }

                var staffManager = new StaffManager
                {
                    Name = viewModel.Name,
                    Role = viewModel.Role,
                    UserId = viewModel.UserId,
                    Password = viewModel.Password,
                    ContactNumber = viewModel.ContactNumber
                };

                await _staffManagerRepository.Insert(staffManager);
                serviceReturn.Data = viewModel;
            }
            catch (System.Exception ex)
            {
                serviceReturn.AddTechnicalInsertError();
                _logger.LogError($"Error while adding security manager: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<StaffManagerViewModel>> Login(long UserId, long Password)
        {
            var serviceReturn = new ServiceReturn<StaffManagerViewModel>();

            try
            {
                var staffManager = (await _staffManagerRepository.FindBy(s => s.UserId == UserId && s.Password == Password)).FirstOrDefault();

                if (staffManager == null)
                {
                    serviceReturn.AddError("Incorrect user ID or password.");
                    return serviceReturn;
                }

                serviceReturn.Data = new StaffManagerViewModel
                {
                    StaffManagerId = staffManager.StaffManagerId,
                    Name = staffManager.Name,
                    Role = staffManager.Role,
                    UserId = staffManager.UserId,
                    Password = staffManager.Password,
                    ContactNumber = staffManager.ContactNumber
                };
            }
            catch (System.Exception ex)
            {
                serviceReturn.AddError($"Error during login: {ex.Message}");
                _logger.LogError($"Error logging in staff manager: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<StaffManagerViewModel>> GetStaffManagerById(int staffManagerId)
        {
            var serviceReturn = new ServiceReturn<StaffManagerViewModel>();

            try
            {
                var staffManager = (await _staffManagerRepository.FindBy(sm => sm.StaffManagerId == staffManagerId)).FirstOrDefault();

                if (staffManager == null)
                {
                    serviceReturn.AddError("No staff manager found with this ID.");
                    return serviceReturn;
                }

                serviceReturn.Data = new StaffManagerViewModel
                {
                    StaffManagerId = staffManager.StaffManagerId,
                    Name = staffManager.Name,
                    Role = staffManager.Role,
                    UserId = staffManager.UserId,
                    ContactNumber = staffManager.ContactNumber
                };
            }
            catch (System.Exception ex)
            {
                serviceReturn.AddError($"Error retrieving staff manager: {ex.Message}");
                _logger.LogError($"Error retrieving staff manager: {ex.Message}");
            }

            return serviceReturn;
        }
    }
}
