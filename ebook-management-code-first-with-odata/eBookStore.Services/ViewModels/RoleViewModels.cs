using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Services.ViewModels
{
    public class RoleViewModel
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; } = default!;
    }

    public class RoleCreateModel
    {
        public string RoleName { get; set; } = default!;
    }
}
