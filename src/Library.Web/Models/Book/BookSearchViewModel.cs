using System.Collections.Generic;
using System.Web.Mvc;

namespace Library.Web.Models.Book
{
    public sealed class BookSearchViewModel
    {
        public BookSearchViewModel(BookFilterViewModel searchModel, IEnumerable<BookViewModel> books, IEnumerable<SelectListItem> authors, IEnumerable<SelectListItem> publishers)
        {
            SearchModel = searchModel;
            Books = books;
            Authors = authors;
            Publishers = publishers;
        }

        public BookFilterViewModel SearchModel { get; private set; }
        public IEnumerable<BookViewModel> Books { get; private set; }
        public IEnumerable<SelectListItem> Authors { get; private set; }
        public IEnumerable<SelectListItem> Publishers { get; private set; }
    }
}