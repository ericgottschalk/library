using Library.Web.Models.Error;
using System.Web.Mvc;

namespace Library.Web.Filters
{
    public sealed class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            var model = new ErrorViewModel
            {
                Title = "An error occurred",
                Message = filterContext.Exception.Message,
                StackTrace = filterContext.Exception.StackTrace
            };

            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml",
                ViewData = new ViewDataDictionary<ErrorViewModel>(model)
            };
            filterContext.ExceptionHandled = true;
        }
    }
}