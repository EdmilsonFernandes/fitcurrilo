using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto.Web.Models; //classes de modelo..
using System.Web.Security;
using Projeto.Entities; //entidades..
using Projeto.DAL.Persistence; //acesso a banco..
using Projeto.Util; //criptografia..
using Newtonsoft.Json; //JSON..

namespace Projeto.Web.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario/Login
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //redirecionamento..
                return RedirectToAction("Index", " ");
            }

            return View();
        }

        // GET: Usuario/Cadastro
        public ActionResult Cadastro()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //redirecionamento..
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Usuario/Login
        [HttpPost] //submit do formulario
        public ActionResult Login(UsuarioViewModelLogin model)
        {
            if(ModelState.IsValid) //se os dados da model estão corretos?
            {
                try
                {
                    UsuarioDal d = new UsuarioDal(); //persistencia..
                    Usuario u = d.FindByLoginSenha(model.Login, Criptografia.EncryptMD5(model.Senha));
                    
                    if(u != null) //usuario foi encontrado?
                    {
                        //transferir os dados do usuario para a classe de modelo..
                        UsuarioAutenticado auth = new UsuarioAutenticado();
                        auth.IdUsuario = u.IdUsuario;
                        auth.Nome = u.Nome;
                        auth.Login = u.Login;
                        auth.Perfil = u.Perfil.Nome;

                        //1º) Criar o ticket de acesso..
                        FormsAuthenticationTicket ticket = 
                            new FormsAuthenticationTicket(JsonConvert.SerializeObject(auth), false, 5);

                        //2º) Gravar o ticket em cookie..
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, 
                                                           FormsAuthentication.Encrypt(ticket));
                        //gravar o cookie..
                        Response.Cookies.Add(cookie);

                        //redirecionar para a área restrita..
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                       ViewBag.Mensagem = "Acesso Negado.Tente novamente";

                    }
                }
                catch(Exception e)
                {
                    ViewBag.Mensagem = e.Message;
                }
            }

            return View();
        }

        // POST: Usuario/Cadastro
        [HttpPost] //submit do formulario
        public ActionResult Cadastro(UsuarioViewModelCadastro model)
        {
            if(ModelState.IsValid) //se os dados da model estão corretos?
            {
                try
                {
                    UsuarioDal d = new UsuarioDal(); //persistencia..
                    if(d.HasLogin(model.Login)) //se ja existe o login na base de dados..
                    {
                        throw new Exception("Erro Este login ja esta cadastrado, tente outro.");
                    }

                    Usuario u = new Usuario(); //entidade..
                    u.Nome  = model.Nome;
                    u.Login = model.Login;
                    u.Senha = Criptografia.EncryptMD5(model.Senha);
                    u.IdPerfil = 1; //Perfil Usuario padrão..

                    //cadastrando no banco de dados..                    
                    d.Insert(u); //gravando o usuario no banco de dados..

                    ViewBag.Mensagem = "Usuario " + u.Nome + ", cadastrado com sucesso.";
                    ModelState.Clear(); //limpar os campos do formulário..
                }
                catch(Exception e)
                {
                    //exibir mensagem de erro..
                    ViewBag.Mensagem = e.Message;
                }
            }

            return View();
        }

        //ação para logout do usuario..
        public ActionResult Logout()
        {
            //remover o ticket do usuario..
            FormsAuthentication.SignOut();

            //redirecionar para a página inicial..
            return RedirectToAction("Index", "Home");
        }
    }
}