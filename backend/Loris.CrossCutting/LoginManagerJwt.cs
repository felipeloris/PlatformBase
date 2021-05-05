using Loris.Common;
using Loris.Common.Domain.Interfaces;
using Loris.Common.Tools;
using Loris.Common.Webapi.Helpers;
using Microsoft.AspNetCore.Http;
using System;

namespace Loris.CrossCutting
{
    public class LoginManagerJwt : ILoginManager
    {
        public ILogin Login { get; private set; }

        public bool Logged { get; private set; }

        public LoginManagerJwt(IHttpContextAccessor httpContextHandler)
        {
            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-context?view=aspnetcore-3.0

            try
            {
                var httpContext = httpContextHandler.HttpContext;
                var currentUser = httpContext?.User;

                Login = JwtHelper.GetLogin(currentUser);
                Logged = Login != null;

                if (Logged)
                    SetCulture(Login.Language);
            }
            catch (Exception)
            {
            }
        }

        public void SetCulture(Languages languageCode)
        {
            /* não foi necessário configurar, bastou configurar os threads
            var cultura = languageCode.GetCultureInfo();
            BASERES.Culture = cultura;
            */
            CultureHelper.SetCulture(languageCode);
        }
    }
}
