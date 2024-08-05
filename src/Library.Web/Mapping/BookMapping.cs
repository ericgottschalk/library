using Library.Application.Results.Book;
using Library.Web.Models.Book;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Library.Web.Mapping
{
    internal static class BookMapping
    {
        internal static BookSearchViewModel Map(BookFilterViewModel searchModel, SearchBookCommandResult result)
        {
            var bookViewModels = result.Books.Select(b => new BookViewModel
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                Language = b.Language,
                AuthorName = b.AuthorName,
                PublisherName = b.PublisherName,
                IsRented = b.IsRented
            });

            var authors = result.Authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            var publishers = result.Publishers.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name });

            return new BookSearchViewModel(searchModel, bookViewModels, authors, publishers);
        }

        internal static BookDetailsViewModel Map(BookCommandResult result)
        {
            return new BookDetailsViewModel
            {
                AuthorName = result.AuthorName,
                PublisherName = result.PublisherName,
                IsRented = result.IsRented,
                Id = result.Id,
                Title = result.Title,
                ISBN = result.ISBN,
                Language = result.Language,
                PublicationDate = result.PublicationDate,
                Summary = result.Summary
            };
        }
    }
}