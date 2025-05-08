using Shared.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel;

namespace Domain.IServices
{
    public interface IGateService
    {
       public Task<ServiceReturn<GateViewModel>> AddGate(GateViewModel gateViewModel);
       public Task<ServiceReturn<GateViewModel>> GetGateById(int gateId);
       public Task<ServiceReturn<List<GateViewModel>>> GetGatesByCampusId(int campusId);
    }
}
