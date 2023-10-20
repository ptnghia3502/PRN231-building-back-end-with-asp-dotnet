using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models;

namespace Services.ViewModels
{
    public class OrderView
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }

        public virtual MemberView? Member { get; set; }
        public virtual ICollection<OrderDetailView> OrderDetails { get; set; }
    }

    public class OrderCreateView
    {
        public int OrderId { get; set; }
        public int? MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }
        public virtual ICollection<OrderDetailView> OrderDetails { get; set; }
    }

    public class OrderUpdateView
    {
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }
    }
}
