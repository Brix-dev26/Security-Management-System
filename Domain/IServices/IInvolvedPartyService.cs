
using Shared.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel;

namespace Domain.IServices
{
    public interface IInvolvedPartyService
    {
        public Task<ServiceReturn<InvolvedPartyViewModel>> AddInvolvedParty(InvolvedPartyViewModel involvedPartyViewModel);
        public Task<ServiceReturn<InvolvedPartyViewModel>> GetInvolvedPartyById(int involvedPartyId);
        public Task<ServiceReturn<List<InvolvedPartyViewModel>>> GetInvolvedPartiesByEmergencyId(int emergencyId);
        public Task<ServiceReturn<InvolvedPartyViewModel>> GetEmergencyIdsBySecurityStaffId(int secId);
    }
}
