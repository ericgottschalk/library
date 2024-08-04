using Library.Application.Results;
using System.Web;
using System.Web.Security;

namespace Library.Web.Authorization
{
    public static class SessionControl
    {
        private const string SINGED_MEMBER = "SINGED_MEMBER";

        public static SingedMember SingedMember
        {
            get
            {
                return HttpContext.Current.Session[SINGED_MEMBER] as SingedMember;
            }
        }

        public static void CreateMemberSession(LoginCommandResult member)
        {
            var singedMember = new SingedMember(member.Id, member.Email);
            FormsAuthentication.SetAuthCookie(member.Email, true);
            HttpContext.Current.Session[SINGED_MEMBER] = singedMember;
        }

        public static void SignOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Abandon();
        }
    }
}