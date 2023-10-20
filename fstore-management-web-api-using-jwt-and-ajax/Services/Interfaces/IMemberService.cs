using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models;
using Services.ViewModels;

namespace Services.Interfaces
{
    public interface IMemberService
    {
        Task<List<MemberView>> GetAllMembers();
        Task<MemberView> GetMemberById(int id);
        Task<MemberView> GetMemberByEmail(string email, string password);
        Task<bool> CreateMember(MemberCreateView createDTO);
        Task<bool> UpdateMember(int id, MemberUpdateView updateDTO);
        Task<bool> Delete(int id);
    }
}
