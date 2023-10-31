using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBookStore.Domains.Models;
using eBookStore.Repositories.Data;
using eBookStore.Services.Interface;
using eBookStore.Services.InterfaceRepo;

namespace eBookStore.Repositories.Repository
{
    public class BookAuthorRepository : GenericRepository<BookAuthor>, IBookAuthorRepository
    {
        public BookAuthorRepository(AppDbContext dbContext, ICurrentTime currentTime, IClaimsService claimsService)
        : base(dbContext, currentTime, claimsService) { }
    }
}
