using eBookStore.Client.Models;

namespace eBookStore.Client.Interfaces
{
    public interface IBaseService
    {
        Task<string?> SendAsync(RequestModel request, bool withToken = true);
    }
}
