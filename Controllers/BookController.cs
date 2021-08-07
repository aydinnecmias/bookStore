using AutoMapper;
using bookStore.BookOperations.CreateBook;
using bookStore.BookOperations.DeleteBook;
using bookStore.BookOperations.GetBookDetail;
using bookStore.BookOperations.GetBooks;
using bookStore.BookOperations.UpdateBook;
using bookStore.DBOperations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static bookStore.BookOperations.CreateBook.CreateBookCommand;
using static bookStore.BookOperations.UpdateBook.UpdateBookCommand;

namespace bookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]

    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;

        public BookController (BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper; 
        }        

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);

        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex )
            {

                
                return BadRequest(ex.Message);
            }
            return Ok(result);
           
           

        }

        [HttpPost]

        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
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

        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel UpdatedBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = UpdatedBook;
                command.Handle();


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
            return Ok();

        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {
            

            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();


        }
    }
}
