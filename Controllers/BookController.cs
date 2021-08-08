﻿using AutoMapper;
using bookStore.BookOperations.CreateBook;
using bookStore.BookOperations.DeleteBook;
using bookStore.BookOperations.GetBookDetail;
using bookStore.BookOperations.GetBooks;
using bookStore.BookOperations.UpdateBook;
using bookStore.DBOperations;
using FluentValidation;
using FluentValidation.Results;
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
           
                GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
                query.BookId = id;
                result = query.Handle();
           
            return Ok(result);
           
           

        }

        [HttpPost]

        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            
                command.Model = newBook;

                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

                //if(!result.IsValid)
                //    foreach(var item in result.Errors)
                //    {
                //        Console.WriteLine("Özellik " + item.PropertyName + "- Error Message: " + item.ErrorMessage );
                //    }
                //else
                //{ 
                //     command.Handle();

                //}
             
            return Ok();

        }

        [HttpPut("{id}")]

        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel UpdatedBook)
        {
           
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();                
                command.Model = UpdatedBook;
                validator.ValidateAndThrow(command);
                command.Handle();

           
            return Ok();

        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {
   
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
           
            return Ok();


        }
    }
}
