using Shared.Domain;
using System.Threading.Tasks;
using ViewModel;

namespace Domain.IServices
{
    public interface IStaffManagerCampusSevices
    {
        public Task<ServiceReturn<StaffManagerCampusViewModel>> AddStaffManagerCampus(StaffManagerCampusViewModel viewModel);
 

    }
}
