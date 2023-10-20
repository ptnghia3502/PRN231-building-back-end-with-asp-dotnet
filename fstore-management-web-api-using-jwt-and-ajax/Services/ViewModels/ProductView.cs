using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models;

namespace Services.ViewModels
{
    public class ProductView
    {
        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Weight { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        public virtual CategoryView? Category { get; set; }
    }

    public class ProductCreateView
    {
        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Weight { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }

    public class ProductUpdateView
    {
        public int? CategoryId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Weight { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }
}
