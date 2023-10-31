using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Services.ViewModels
{
    public class BookAuthorViewModel : BookAuthorUpdateModel
    {

    }
    public class BookAuthorUpdateModel : BookAuthorCreateModel
    {
        public Guid Id { get; set; }
    }
    public class BookAuthorCreateModel
    {
        public double Royality_Percentage { get; set; }
        public Guid AuthorId { get; set; }
        public Guid BookId { get; set; }
    }
}
