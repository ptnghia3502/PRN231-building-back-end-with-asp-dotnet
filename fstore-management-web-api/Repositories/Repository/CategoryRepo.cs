using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Interfaces;
using Repositories.Models;

namespace Repositories.Repository
{
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(FStoreDBContext context) : base(context)
        {
        }
    }
}
