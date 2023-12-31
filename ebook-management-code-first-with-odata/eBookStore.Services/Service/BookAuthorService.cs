﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eBookStore.Domains.Models;
using eBookStore.Repositories;
using eBookStore.Services.InterfaceSerivce;
using eBookStore.Services.ViewModels;

namespace eBookStore.Services.Service
{
    public class BookAuthorService : IBookAuthorService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public BookAuthorService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<BookAuthorViewModel> CreateBookAuthorAsync(BookAuthorCreateModel model)
        {
            var bookAuthor = _mapper.Map<BookAuthor>(model);

            var bookAuthorList = await _unitOfWork.BookAuthorRepository.FindListByField(x => x.BookId == model.BookId);
            if (bookAuthorList.Count > 0)
            {
                var flag = bookAuthorList.FirstOrDefault(x => x.AuthorId == model.AuthorId);
                if (flag is not null) throw new Exception("Author Exsited!");

            }
            bookAuthor.AuthorOrder = bookAuthorList.Count + 1;
            await _unitOfWork.BookAuthorRepository.AddAsync(bookAuthor);
            if (await _unitOfWork.SaveChangesAsync())
            {
                return _mapper.Map<BookAuthorViewModel>(await _unitOfWork.BookAuthorRepository.GetByIdAsync(bookAuthor.Id));
            }
            else throw new Exception("Save changes failed!");
        }

        public async Task<bool> DeleteBookAuthorAsync(Guid id)
        {
            var bookAuthor = await _unitOfWork.BookAuthorRepository.GetByIdAsync(id);
            if (bookAuthor != null)
            {
                _unitOfWork.BookAuthorRepository.SoftRemove(bookAuthor);
                return await _unitOfWork.SaveChangesAsync();
            }
            else throw new Exception($"Not found Book author with Id: {id}");
        }

        public async Task<IEnumerable<BookAuthorViewModel>> GetAllAsync()
        => _mapper.Map<IEnumerable<BookAuthorViewModel>>(await _unitOfWork.BookAuthorRepository.GetAllAsync());

        public async Task<BookAuthorViewModel> GetBookAuthorByIdAsync(Guid id)
        => _mapper.Map<BookAuthorViewModel>(await _unitOfWork.BookAuthorRepository.GetByIdAsync(id));

        public async Task<BookAuthorViewModel> UpdateBookAuthorAsync(BookAuthorUpdateModel model)
        {
            var item = await _unitOfWork.BookAuthorRepository.GetByIdAsync(model.Id);
            if (item != null)
            {
                _mapper.Map(model, item);
                _unitOfWork.BookAuthorRepository.Update(item);
                if (await _unitOfWork.SaveChangesAsync())
                {
                    return _mapper.Map<BookAuthorViewModel>(item);
                }
                else throw new Exception("Save Changes failed!");
            }
            else throw new Exception($"Not found Book Author with Id: {model.Id}");
        }
    }
}
