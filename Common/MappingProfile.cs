using AutoMapper;
using bookStore.BookOperations.GetBookDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static bookStore.BookOperations.CreateBook.CreateBookCommand;

namespace bookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest=> dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));

        } 
    }
}
