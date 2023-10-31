using eBookStore.Services.ViewModels;

namespace eBookStore.Client.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleViewModel>?> GetAllRoleAsync();

    }
}
