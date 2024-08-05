using Library.Application.Results;
using System.Web;
using System.Web.Security;

namespace Library.Web.Authorization
{
    public static class SessionControl
    {
        private const string SIGNED_MEMBER = "SIGNED_MEMBER";

        public static SignedMember SingedMember
        {
            get
            {
                var signedMember = HttpContext.Current.Session[SIGNED_MEMBER];
                
                if (signedMember == null)
                    return null;

                return signedMember as SignedMember;
            }
        }

        public static void CreateMemberSession(LoginCommandResult member)
        {
            var signedMember = new SignedMember(member.Id, member.Email);
            FormsAuthentication.SetAuthCookie(member.Email, true);
            HttpContext.Current.Session[SIGNED_MEMBER] = signedMember;
        }

        public static void SignOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Abandon();
        }
    }
}