using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBookStore.Services.ViewModels;

namespace eBookStore.Services.InterfaceSerivce
{
    public interface IPublisherService
    {
        Task<IEnumerable<PublisherViewModel>> GetAllAsync();
        Task<PublisherViewModel> GetByIdAsync(Guid id);
        Task<PublisherViewModel> CreateAsync(PublisherCreateModel model);
        Task<PublisherViewModel> UpdateAsync(PublisherUpdateModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}
