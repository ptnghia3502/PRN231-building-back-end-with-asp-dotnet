using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class MemberView
    {
        public int MemberId { get; set; }
        public string Email { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class MemberCreateView
    {
        public int MemberId { get; set; }
        public string Email { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class MemberUpdateView
    {
        // ko update ID
        // public int MemberId { get; set; }
        public string Email { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class LoginView
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
