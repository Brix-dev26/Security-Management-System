using Shared.Domain;
using System.Threading.Tasks;
using ViewModel;

namespace Domain.IServices
{
    public interface ISecurityStaffService
    {
        public Task<ServiceReturn<SecurityStaffViewModel>> AddSecurityStaff(SecurityStaffViewModel securityStaffViewModel);
        public Task<ServiceReturn<SecurityStaffViewModel>> GetSecurityStaffById(int Sec_ID);
        public Task<ServiceReturn<SecurityStaffViewModel>> Login(long UserId, long Password);
        public Task<ServiceReturn<SecurityStaffViewModel>> UpdateGateBySecurityId(int secId, int gateId);

    }
}
