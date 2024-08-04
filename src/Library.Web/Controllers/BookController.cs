using Library.Web.Authorization;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    [Authorization]
    public sealed class BookController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}