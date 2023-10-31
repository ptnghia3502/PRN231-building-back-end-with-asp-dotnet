using eBookStore.Services.ViewModels;

namespace eBookStore.Client.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseModel?> LoginAsync(LoginModel model);
    }
}
