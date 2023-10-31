using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Services.ViewModels
{
    public class AuthorViewModel
    {
        public Guid Id { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Zip { get; set; } = default!;
        public string Email { get; set; } = default!;

    }

    public class AuthorCreateModel
    {
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Zip { get; set; } = default!;
        public string Email { get; set; } = default!;

    }

    public class AuthorUpdateModel : AuthorCreateModel
    {
        public Guid Id { get; set; } = default;
    }
}
