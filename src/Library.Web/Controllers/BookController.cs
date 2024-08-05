using Library.Application.Commands.Book;
using Library.Application.Handlers;
using Library.Web.Authorization;
using Library.Web.Mapping;
using Library.Web.Models.Book;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    [Authorization]
    public sealed class BookController : Controller
    {
        private readonly BookCommandHandler _handler;

        public BookController()
        {
            _handler = new BookCommandHandler();
        }

        [HttpGet]
        public async Task<ActionResult> Index(BookFilterViewModel searchModel, CancellationToken cancellationToken)
        {
            if (searchModel == null)
            {
                searchModel = new BookFilterViewModel();
            }

            var result = await _handler.Handle(new SearchBookCommand(searchModel.Title, searchModel.ISBN, searchModel.AuthorId, searchModel.PublisherId), cancellationToken);

            return View(BookMapping.Map(searchModel, result));
        }

        [HttpGet]
        public async Task<ActionResult> Details(long id, CancellationToken cancellationToken)
        {
            var result = await _handler.Handle(new GetBookCommand(id), cancellationToken);

            if (result == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            return View(BookMapping.Map(result));
        }

        [HttpPost]
        public async Task<ActionResult> Rent(long bookId, CancellationToken cancellationToken)
        {
            var result = await _handler.Handle(new RentBookCommand(bookId, SessionControl.SingedMember.Id), cancellationToken);

            if (!result.Success)
            {
                return RedirectToAction("Index", new BookFilterViewModel { ErrorMessage = result.Message });
            }

            return RedirectToAction("Member", "Member");
        }

        [HttpPost]
        public async Task<ActionResult> Return(long bookId, CancellationToken cancellationToken)
        {
            var result = await _handler.Handle(new ReturnBookCommand(bookId, SessionControl.SingedMember.Id), cancellationToken);

            return RedirectToAction("Member", "Member");
        }
    }
}