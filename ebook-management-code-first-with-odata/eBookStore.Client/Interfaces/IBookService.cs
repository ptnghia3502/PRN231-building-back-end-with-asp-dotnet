using eBookStore.Services.ViewModels;

namespace eBookStore.Client.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>?> GetAllAsync(string query = "");
        Task<BookViewModel?> CreateAsync(BookCreateModel model);
        Task<bool> UpdateAsync(BookUpdateModel model);
        Task<bool> DeleteAsync(Guid id);
        Task<BookViewModel?> GetByIdAsync(Guid id);
        Task<bool> AddBookAuthor(BookAuthorCreateModel model);
    }
}
