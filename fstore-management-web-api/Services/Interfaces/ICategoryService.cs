using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategory();
    }
}
