using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBookStore.Services.ViewModels;

namespace eBookStore.Services.InterfaceSerivce
{
    public interface IBookAuthorService
    {
        Task<IEnumerable<BookAuthorViewModel>> GetAllAsync();
        Task<BookAuthorViewModel> GetBookAuthorByIdAsync(Guid id);
        Task<BookAuthorViewModel> CreateBookAuthorAsync(BookAuthorCreateModel model);
        Task<BookAuthorViewModel> UpdateBookAuthorAsync(BookAuthorUpdateModel model);
        Task<bool> DeleteBookAuthorAsync(Guid id);
    }
}
