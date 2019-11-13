using Newtonsoft.Json;
using Projeto.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Projeto.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity) HttpContext.Current.User.Identity;

                        FormsAuthenticationTicket ticket = id.Ticket;

                        UsuarioAutenticado model = JsonConvert.DeserializeObject
                                                   <UsuarioAutenticado>(ticket.Name);

                        string[] roles = { model.Perfil };

                        HttpContext.Current.User = new GenericPrincipal(id, roles);
                    }
                }
            }
        }
    }


}
