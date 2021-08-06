using bookStore.BookOperations.CreateBook;
using bookStore.BookOperations.GetBooks;
using bookStore.DBOperations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static bookStore.BookOperations.CreateBook.CreateBookCommand;

namespace bookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]

    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController (BookStoreDbContext context)
        {
            _context = context;
        }        

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);

        }

        [HttpGet("{id}")]

        public Book GetById(int id)
        {
            var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
            return book;

        }

        [HttpPost]

        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
             
            return Ok();

        }

        [HttpPut("{id}")]

        public IActionResult UpdateBook(int id, [FromBody] Book UpdatedBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);

            if (book is null)
                return BadRequest();
            book.GenreId = UpdatedBook.GenreId != default ? UpdatedBook.GenreId : book.GenreId;
            book.PageCount = UpdatedBook.PageCount != default ? UpdatedBook.PageCount : book.PageCount;
            book.PublishDate = UpdatedBook.PublishDate != default ? UpdatedBook.PublishDate : book.PublishDate;
            book.Title = UpdatedBook.Title != default ? UpdatedBook.Title : book.Title;
            _context.SaveChanges();
            return Ok();

        }

        [HttpDelete("{id}")]

        public IActionResult DeletedBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x=> x.Id == id);

            if (book is null)
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();


        }
    }
}
