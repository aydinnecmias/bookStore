using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]

    public class BookController : ControllerBase
    {

        private static List<Book> BookList = new List<Book>()
        {
            new Book
            {
                Id = 1,
                Title = "Lean Startup",
                GenreId = 1,
                PageCount = 200,
                PublishDate = new DateTime(2001,06,12) 

            }

        };

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;                   

        }

        [HttpGet("{id}")]

        public Book GetById(int id)
        {
            var book = BookList.Where(book => book.Id == id).SingleOrDefault();
            return book;

        }
    }
}
