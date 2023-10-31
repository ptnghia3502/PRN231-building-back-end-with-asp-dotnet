using eBookStore.Services.ViewModels;

namespace eBookStore.Client.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel?> CreateUser(UserCreateModel model);
        Task<IEnumerable<UserViewModel>?> GetAllUserAsync(string search = "");
        Task<UserViewModel?> GetUserById(Guid id);
        Task<bool> DeleteUserAsync(Guid id);
        Task<bool> UpdateUserAsync(UserUpdateModel model);
    }
}
