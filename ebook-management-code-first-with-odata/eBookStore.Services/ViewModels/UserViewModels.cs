using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Services.ViewModels
{
    public class LoginModel
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

    public class LoginResponseModel
    {
        public UserViewModel? User { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
    public class UserCreateModel
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Source { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string MiddleName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public Guid PublisherId { get; set; }
        public Guid RoleId { get; set; }
    }

    public class UserUpdateModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Source { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string MiddleName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public Guid RoleId { get; set; } = default!;
        public Guid PublisherId { get; set; } = default!;
    }

    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Source { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string MiddleName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public Guid RoleId { get; set; } = default!;
        public RoleViewModel Role { get; set; } = default!;
        public Guid PublisherId { get; set; } = default!;
        public DateTime HireDate { get; set; }
    }
}
