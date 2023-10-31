using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBookStore.Services.InterfaceRepo;

namespace eBookStore.Repositories
{
    public interface IUnitOfWork
    {
        public IAuthorRepository AuthorRepository { get; }
        public IBookAuthorRepository BookAuthorRepository { get; }
        public IBookRepository BookRepository { get; }
        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IPublisherRepository PublisherRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
