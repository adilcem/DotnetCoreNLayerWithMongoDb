using AutoMapper;
using DotnetCoreNLayerWithMongoDb.API.DTO.Book;
using DotnetCoreNLayerWithMongoDb.Core.MongoDb.Models;
using DotnetCoreNLayerWithMongoDb.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreNLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMongoService<Book> _bookService;
        private readonly IMapper _mapper;


        public BooksController(IMongoService<Book> bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        //GET: /<controller>/
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<BookDto>>(books));
        }
        //Get: /person/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var book = await _bookService.GetByIdAsync(id);

            return Ok(_mapper.Map<BookDto>(book));
        }

        [HttpPost]
        public async Task<IActionResult> Save(BookDto book)
        {
            var addedBook = await _bookService.AddAsync(_mapper.Map<Book>(book));

            return Created(String.Empty, _mapper.Map<BookDto>(addedBook));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, BookDto book)
        {
            var updatedBook= await _bookService.GetByIdAsync(id);

            if (!(updatedBook is null))
            {
                updatedBook.BookName = book.BookName;
                updatedBook.Author = book.Author;
                updatedBook.Category = book.Category;
                updatedBook.Price= book.Price;
                _bookService.Update(updatedBook);

                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(string id)
        {
            _bookService.Remove(id);

            return NoContent();
        }
    }
}
