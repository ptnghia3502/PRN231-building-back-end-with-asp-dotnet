using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eBookStore.Domains.Models;
using eBookStore.Repositories;
using eBookStore.Services.InterfaceSerivce;
using eBookStore.Services.ViewModels;

namespace eBookStore.Services.Service
{
    public class PublisherService : IPublisherService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public PublisherService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PublisherViewModel> CreateAsync(PublisherCreateModel model)
        {
            var publisher = _mapper.Map<Publisher>(model);
            await _unitOfWork.PublisherRepository.AddAsync(publisher);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PublisherViewModel>(await _unitOfWork.PublisherRepository.GetByIdAsync(publisher.Id));
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var publisher = await _unitOfWork.PublisherRepository.GetByIdAsync(id);
            if (publisher is not null)
            {
                _unitOfWork.PublisherRepository.SoftRemove(publisher);
                return await _unitOfWork.SaveChangesAsync();
            }
            else throw new Exception($"Not found publisher with Id: {id}");

        }

        public async Task<IEnumerable<PublisherViewModel>> GetAllAsync()
        => _mapper.Map<IEnumerable<PublisherViewModel>>(await _unitOfWork.PublisherRepository.GetAllAsync());

        public async Task<PublisherViewModel> GetByIdAsync(Guid id)
            => _mapper.Map<PublisherViewModel>(await _unitOfWork.PublisherRepository.GetByIdAsync(id));


        public async Task<PublisherViewModel> UpdateAsync(PublisherUpdateModel model)
        {
            var publisher = await _unitOfWork.PublisherRepository.GetByIdAsync(model.Id);
            if (publisher is not null)
            {
                _mapper.Map(model, publisher);
                _unitOfWork.PublisherRepository.Update(publisher);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<PublisherViewModel>(await _unitOfWork.PublisherRepository.GetByIdAsync(model.Id));
            }
            else throw new Exception($"Not found Publisher with Id: {model.Id}");
        }
    }

}
