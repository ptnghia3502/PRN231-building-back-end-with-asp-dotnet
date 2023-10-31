using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBookStore.Services.ViewModels;

namespace eBookStore.Services.InterfaceSerivce
{
    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>> GetAllAsync();
        Task<BookViewModel> GetByIdAsync(Guid id);
        Task<BookViewModel> CreateAsync(BookCreateModel model);
        Task<BookViewModel> UpdateAsync(BookUpdateModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}
