using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Domains.Models
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; } = default!;
        public ICollection<User> Users { get; set; } = default!;
    }
}
