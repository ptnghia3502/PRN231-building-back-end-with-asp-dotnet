using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBookStore.Services.ViewModels;

namespace eBookStore.Services.Interface
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorViewModel>> GetAllAsync();
        Task<AuthorViewModel> GetById(Guid id);
        Task<AuthorViewModel> CreateAuthor(AuthorCreateModel model);
        Task<AuthorViewModel> UpdateAuthor(AuthorUpdateModel model);
        Task<bool> DeleteAuthor(Guid id);
    }
}
