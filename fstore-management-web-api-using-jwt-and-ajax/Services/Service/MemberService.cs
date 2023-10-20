using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Repositories;
using Repositories.Models;
using Services.Interfaces;
using Services.ViewModels;

namespace Services.Service
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        public MemberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateMember(MemberCreateView createDTO)
        {
            var newMember = _mapper.Map<Member>(createDTO);

            await _unitOfWork.MemberRepo.AddAsync(newMember);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var member = await _unitOfWork.MemberRepo.FindByField(x => x.MemberId == id);
            _unitOfWork.MemberRepo.Remove(member);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<List<MemberView>> GetAllMembers()
        {
            var members = await _unitOfWork.MemberRepo.GetAllAsync();
            var result = _mapper.Map<List<MemberView>>(members);
            return result;
        }

        public async Task<MemberView> GetMemberByEmail(string email, string password)
        {
            var member = await _unitOfWork.MemberRepo.FindByField(x => x.Email == email && x.Password == password);
            var result = _mapper.Map<MemberView>(member);
            return result;
        }

        public async Task<MemberView> GetMemberById(int id)
        {
            var member = await _unitOfWork.MemberRepo.FindByField(x => x.MemberId == id);
            var result = _mapper.Map<MemberView>(member);
            return result;
        }

        public async Task<bool> UpdateMember(int id, MemberUpdateView updateDTO)
        {
            var member = await _unitOfWork.MemberRepo.FindByField(x => x.MemberId == id);
            if (member == null)
            {
                return false;
            }
            member = _mapper.Map(updateDTO, member);
            _unitOfWork.MemberRepo.Update(member);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
