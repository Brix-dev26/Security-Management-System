using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Domain.IServices
{
    public interface IStaffManagerService
    {
       public Task<ServiceReturn<StaffManagerViewModel>> AddStaffManager(StaffManagerViewModel StaffManagerViewModel);
        public Task<ServiceReturn<StaffManagerViewModel>> Login(long UserId, long Password);
        public Task<ServiceReturn<StaffManagerViewModel>> GetStaffManagerById(int staffManagerId);
    }

}
