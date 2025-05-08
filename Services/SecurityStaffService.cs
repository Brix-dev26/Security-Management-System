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
    public class SecurityStaffService : ISecurityStaffService
    {
        private readonly IRepository<SecurityStaff> _securityStaffRepository;
        private readonly IRepository<Campus> _campusRepository;
        private readonly IRepository<Gate> _gateRepository;
        private readonly ILogger<SecurityStaffService> _logger;

        public SecurityStaffService(
            IRepository<SecurityStaff> securityStaffRepository,
            IRepository<Campus> campusRepository,
            IRepository<Gate> gateRepository,
            ILogger<SecurityStaffService> logger)
        {
            _securityStaffRepository = securityStaffRepository;
            _campusRepository = campusRepository;
            _gateRepository = gateRepository;
            _logger = logger;
        }

        public async Task<ServiceReturn<SecurityStaffViewModel>> AddSecurityStaff(SecurityStaffViewModel securityStaffViewModel)
        {
            var serviceReturn = new ServiceReturn<SecurityStaffViewModel>();

            try
            {
                if (securityStaffViewModel.NationalIdCard.ToString().Length != 14)
                {
                    serviceReturn.AddError("National ID must be exactly 14 digits.");
                    return serviceReturn;
                }

                var existingNationalId = (await _securityStaffRepository.FindBy(s => s.NationalIdCard == securityStaffViewModel.NationalIdCard)).FirstOrDefault();
                if (existingNationalId != null)
                {
                    serviceReturn.AddError("This National ID is already registered with another security staff member.");
                    return serviceReturn;
                }

                if (!string.IsNullOrEmpty(securityStaffViewModel.ContactNumber))
                {
                    if (!securityStaffViewModel.ContactNumber.All(char.IsDigit) || securityStaffViewModel.ContactNumber.Length != 11)
                    {
                        serviceReturn.AddError("Phone number must be exactly 11 digits.");
                        return serviceReturn;
                    }

                    var existingPhoneStaff = (await _securityStaffRepository.FindBy(s => s.ContactNumber == securityStaffViewModel.ContactNumber)).FirstOrDefault();
                    if (existingPhoneStaff != null)
                    {
                        serviceReturn.AddError("This phone number is already registered with another security staff member.");
                        return serviceReturn;
                    }
                }

                var campus = (await _campusRepository.FindBy(c => c.CampusId == securityStaffViewModel.CampusId)).FirstOrDefault();
                if (campus == null)
                {
                    serviceReturn.AddError("Selected campus does not exist.");
                    return serviceReturn;
                }

                var gate = (await _gateRepository.FindBy(g => g.GateId == securityStaffViewModel.GateId)).FirstOrDefault();
                if (gate == null)
                {
                    serviceReturn.AddError("Selected gate does not exist.");
                    return serviceReturn;
                }

                var securityStaff = new SecurityStaff
                {
                    Sec_ID = securityStaffViewModel.Sec_ID,
                    NationalIdCard = securityStaffViewModel.NationalIdCard,
                    Name = securityStaffViewModel.Name,
                    Role = securityStaffViewModel.Role,
                    ShiftTime = securityStaffViewModel.ShiftTime,
                    ContactNumber = securityStaffViewModel.ContactNumber,
                    UserId = securityStaffViewModel.UserId,
                    Password = securityStaffViewModel.Password,
                    CampusId = campus.CampusId,
                    GateId = gate.GateId
                };

                await _securityStaffRepository.Insert(securityStaff);
                serviceReturn.Data = securityStaffViewModel;
            }
            catch (System.Exception ex)
            {
                serviceReturn.AddTechnicalInsertError();
                _logger.LogError($"Error while adding security staff: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<SecurityStaffViewModel>> GetSecurityStaffById(int Sec_ID)
        {
            var serviceReturn = new ServiceReturn<SecurityStaffViewModel>();

            try
            {
                var staff = (await _securityStaffRepository.FindBy(s => s.Sec_ID == Sec_ID)).FirstOrDefault();

                if (staff == null)
                {
                    serviceReturn.AddError("No security staff found with this ID.");
                    return serviceReturn;
                }

                var campus = (await _campusRepository.FindBy(c => c.CampusId == staff.CampusId)).FirstOrDefault();
                if (campus == null)
                {
                    serviceReturn.AddError("Staff's associated campus does not exist.");
                    return serviceReturn;
                }

                var gate = (await _gateRepository.FindBy(g => g.GateId == staff.GateId)).FirstOrDefault();
                if (gate == null)
                {
                    serviceReturn.AddError("Staff's associated gate does not exist.");
                    return serviceReturn;
                }

                serviceReturn.Data = new SecurityStaffViewModel
                {
                    Sec_ID = staff.Sec_ID,
                    NationalIdCard = staff.NationalIdCard,
                    Name = staff.Name,
                    Role = staff.Role,
                    ShiftTime = staff.ShiftTime,
                    ContactNumber = staff.ContactNumber,
                    UserId = staff.UserId,
                    CampusId = campus.CampusId,
                    GateId = gate.GateId
                };
            }
            catch (System.Exception ex)
            {
                serviceReturn.AddError($"Error retrieving security staff: {ex.Message}");
                _logger.LogError($"Error retrieving security staff: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<SecurityStaffViewModel>> Login(long UserId, long Password)
        {
            var serviceReturn = new ServiceReturn<SecurityStaffViewModel>();

            try
            {
                var staff = (await _securityStaffRepository.FindBy(s => s.UserId == UserId && s.Password == Password)).FirstOrDefault();

                if (staff == null)
                {
                    serviceReturn.AddError("Incorrect User ID or Password.");
                    return serviceReturn;
                }

                var campus = (await _campusRepository.FindBy(c => c.CampusId == staff.CampusId)).FirstOrDefault();
                if (campus == null)
                {
                    serviceReturn.AddError("Staff's associated campus does not exist.");
                    return serviceReturn;
                }

                var gate = (await _gateRepository.FindBy(g => g.GateId == staff.GateId)).FirstOrDefault();
                if (gate == null)
                {
                    serviceReturn.AddError("Staff's associated gate does not exist.");
                    return serviceReturn;
                }

                serviceReturn.Data = new SecurityStaffViewModel
                {
                    Sec_ID = staff.Sec_ID,
                    NationalIdCard = staff.NationalIdCard,
                    Name = staff.Name,
                    Role = staff.Role,
                    ShiftTime = staff.ShiftTime,
                    ContactNumber = staff.ContactNumber,
                    UserId = staff.UserId,
                    CampusId = campus.CampusId,
                    GateId = gate.GateId
                };
            }
            catch (System.Exception ex)
            {
                serviceReturn.AddError($"Error during login: {ex.Message}");
                _logger.LogError($"Error logging in security staff: {ex.Message}");
            }

            return serviceReturn;
        }

        public async Task<ServiceReturn<SecurityStaffViewModel>> UpdateGateBySecurityId(int secId, int gateId)
        {
            var serviceReturn = new ServiceReturn<SecurityStaffViewModel>();

            try
            {
                var staff = (await _securityStaffRepository.FindBy(s => s.Sec_ID == secId)).FirstOrDefault();
                if (staff == null)
                {
                    serviceReturn.AddError("Security staff not found.");
                    return serviceReturn;
                }

                var gate = (await _gateRepository.FindBy(g => g.GateId == gateId)).FirstOrDefault();
                if (gate == null)
                {
                    serviceReturn.AddError("Selected gate does not exist.");
                    return serviceReturn;
                }

                staff.GateId = gate.GateId;
                await _securityStaffRepository.Update(staff);

                serviceReturn.Data = new SecurityStaffViewModel
                {
                    Sec_ID = staff.Sec_ID,
                    Name = staff.Name,
                    Role = staff.Role,
                    ShiftTime = staff.ShiftTime,
                    ContactNumber = staff.ContactNumber,
                    UserId = staff.UserId,
                    CampusId = staff.CampusId,
                    GateId = staff.GateId
                };
            }
            catch (Exception ex)
            {
                serviceReturn.AddError($"Error while updating staff gate: {ex.Message}");
                _logger.LogError($"Error while updating staff gate: {ex.Message}");
            }

            return serviceReturn;
        }
    }
}
