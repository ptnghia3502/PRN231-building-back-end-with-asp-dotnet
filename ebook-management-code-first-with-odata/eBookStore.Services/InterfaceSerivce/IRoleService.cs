using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBookStore.Services.ViewModels;

namespace eBookStore.Services.InterfaceSerivce
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleViewModel>> GetRoles();
        Task<RoleViewModel> GetRoleById(Guid id);
        Task<RoleViewModel> CreateRole(RoleCreateModel role);

    }
}
