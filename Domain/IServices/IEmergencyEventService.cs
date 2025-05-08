using Shared.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel;

namespace Domain.IServices
{
    public interface IEmergencyEventService
    {
        Task<ServiceReturn<EmergencyEventViewModel>> AddEmergencyEvent(EmergencyEventViewModel emergencyEventViewModel);
        Task<ServiceReturn<EmergencyEventViewModel>> GetEmergencyEventById(int emergencyEventId);
        Task<ServiceReturn<List<EmergencyEventViewModel>>> GetEmergencyEventsBySecurityStaffId(int securityStaffId);
    }
}
