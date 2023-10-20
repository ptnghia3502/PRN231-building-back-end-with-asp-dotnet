using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.ViewModels;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductView>> GetAllProducts();
        Task<ProductView> GetProductById(int id);
        Task<bool> CreateProduct(ProductCreateView createDTO);
        Task<bool> UpdateProduct(int id, ProductUpdateView updateDTO);
        Task<bool> Delete(int id);
    }
}
