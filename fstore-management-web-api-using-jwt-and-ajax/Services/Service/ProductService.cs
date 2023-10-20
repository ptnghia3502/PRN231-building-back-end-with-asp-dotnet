using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Repositories;
using Repositories.Models;
using Services.Interfaces;
using Services.ViewModels;

namespace Services.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateProduct(ProductCreateView createDTO)
        {
            var newProduct = _mapper.Map<Product>(createDTO);

            await _unitOfWork.ProductRepo.AddAsync(newProduct);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var product = await _unitOfWork.ProductRepo.FindByField(x => x.ProductId == id);
            _unitOfWork.ProductRepo.Remove(product);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<List<ProductView>> GetAllProducts()
        {
            var products = await _unitOfWork.ProductRepo.GetAllAsync(x => x.Category!);
            var result = _mapper.Map<List<ProductView>>(products);
            return result;
        }

        public async Task<ProductView> GetProductById(int id)
        {
            var product = await _unitOfWork.ProductRepo.FindByField(x => x.ProductId == id, x => x.Category!);
            var result = _mapper.Map<ProductView>(product);
            return result;
        }

        public async Task<bool> UpdateProduct(int id, ProductUpdateView updateDTO)
        {
            var product = await _unitOfWork.ProductRepo.FindByField(x => x.ProductId == id);
            if (product == null)
            {
                return false;
            }
            product = _mapper.Map(updateDTO, product);
            _unitOfWork.ProductRepo.Update(product);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
