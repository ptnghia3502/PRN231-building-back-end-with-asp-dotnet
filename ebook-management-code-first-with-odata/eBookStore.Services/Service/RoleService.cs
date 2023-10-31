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
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<RoleViewModel> CreateRole(RoleCreateModel roleModel)
        {
            var role = _mapper.Map<Role>(roleModel);
            await _unitOfWork.RoleRepository.AddAsync(role);
            if (await _unitOfWork.SaveChangesAsync())
            {
                return _mapper.Map<RoleViewModel>(await _unitOfWork.RoleRepository.GetByIdAsync(role.Id));
            }
            else throw new Exception("Save Changes Failed");
        }

        public async Task<IEnumerable<RoleViewModel>> GetRoles()
            => _mapper.Map<IEnumerable<RoleViewModel>>(await _unitOfWork.RoleRepository.GetAllAsync());


        public async Task<RoleViewModel> GetRoleById(Guid id)
            => _mapper.Map<RoleViewModel>(await _unitOfWork.RoleRepository.GetByIdAsync(id));
    }
}
