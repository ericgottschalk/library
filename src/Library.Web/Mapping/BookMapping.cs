using Library.Application.Results.Book;
using Library.Web.Models.Book;
using System.Linq;
using System.Web.Mvc;

namespace Library.Web.Mapping
{
    internal static class BookMapping
    {
        internal static BookSearchViewModel Map(BookFilterViewModel searchModel, SearchBookCommandResult result)
        {
            var bookViewModels = result.Books.Select(b => Map(b));

            var authors = result.Authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            var publishers = result.Publishers.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name });

            return new BookSearchViewModel(searchModel, bookViewModels, authors, publishers);
        }

        internal static BookViewModel Map(BookCommandResult result)
        {
            return new BookViewModel
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