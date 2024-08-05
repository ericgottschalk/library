using Library.Web.Models.Error;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public sealed class ErrorController : Controller
    {
        public ActionResult Index()
        {
            var model = new ErrorViewModel
            {
                Title = "An error occurred",
                Message = "Something went wrong."
            };

            return View("Error", model);
        }

        public ActionResult NotFound()
        {
            var model = new ErrorViewModel
            {
                Title = "Page Not Found",
                Message = "The page you are looking for does not exist."
            };

            return View("Error", model);
        }

        public ActionResult ServerError()
        {
            var model = new ErrorViewModel
            {
                Title = "Server Error",
                Message = "An unexpected error occurred on the server."
            };

            return View("Error", model);
        }
    }
}