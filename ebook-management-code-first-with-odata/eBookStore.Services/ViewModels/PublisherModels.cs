using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Services.ViewModels
{
    public class PublisherCreateModel
    {
        public string Name { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Country { get; set; } = default!;
    }

    public class PublisherUpdateModel : PublisherCreateModel
    {
        public Guid Id { get; set; } = default!;
    }

    public class PublisherViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Country { get; set; } = default!;
    }
}
