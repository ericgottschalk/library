using Library.Application.Commands.Member;
using Library.Application.Handlers;
using Library.Web.Authorization;
using Library.Web.Mapping;
using Library.Web.Models.Member;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public sealed class MemberController : Controller
    {
        private readonly MemberCommandHandler _handler;

        public MemberController()
        {
            _handler = new MemberCommandHandler();
        }
         
        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var member = await _handler.Handle(new LoginCommand(model.Email, model.Password), cancellationToken);

                if (member != null)
                {
                    SessionControl.CreateMemberSession(member);

                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    model.ErrorMessage = "Invalid login attempt.";
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var command = new RegisterCommand(model.Name, model.Email, model.Password);

                await _handler.Handle(command, cancellationToken);

                return RedirectToAction("Login");
            }

            return View(model);
        }

        [HttpGet]
        [Authorization]
        public async Task<ActionResult> Member(CancellationToken cancellationToken)
        {
            var command = new GetMemberCommand(SessionControl.SingedMember.Id);

            var result = await _handler.Handle(command, cancellationToken);

            return View(MemberMapping.Map(result));
        }


        [HttpPost]
        public ActionResult Logout()
        {
            SessionControl.SignOut();
            return RedirectToAction("Index", "Book");
        }
    }
}