using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Services.ViewModels
{
    public class BookViewModel : BookUpdateModel
    {

    }

    public class BookUpdateModel : BookCreateModel
    {
        public Guid Id { get; set; } = default!;
        public ICollection<BookAuthorCreateModel> BookAuthors { get; set; } = default!;
    }

    public class BookCreateModel
    {
        public string Title { get; set; } = default!;
        public string Type { get; set; } = default!;
        public double Price { get; set; }
        public string Advance { get; set; } = default!;
        public string Royalty { get; set; } = default!;
        public DateTime PublishedDate { get; set; } = default!;
        public string Notes { get; set; } = default!;
        public Guid PublisherId { get; set; } = default!;

    }
}
