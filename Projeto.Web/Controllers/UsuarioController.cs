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
using Projeto.DAL.DataSource;

namespace Projeto.Web.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", " ");
            }

            return View();
        }

        // GET: Alterar Senha
        public ActionResult Senha(int id)
        {
            // Verifica se usuário está autenticado e possui perfil de administrador
            if (!HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Admin"))
            {
                TempData["Falha"] = "Necessário estar autenticado com perfil de Administrador";
                return RedirectToAction("Login", "Usuario");
            }

            UsuarioDal uDal = new UsuarioDal();
            Usuario usuario = uDal.FindById(id);
            UsuarioAlterarSenha uView = new UsuarioAlterarSenha();
            uView.Id = usuario.IdUsuario;
            uView.Login = usuario.Login;

            return View(uView);
        }

        // GET: Visualizar dados da conta
        public ActionResult Perfil(int id)
        {
            UsuarioDal uDal = new UsuarioDal();
            Usuario usuario = uDal.FindById(id);
            PerfilDal pDal = new PerfilDal();
            Perfil perfil = pDal.FindById(usuario.IdPerfil);
            UsuarioViewModel uView = new Models.UsuarioViewModel();

            uView.Id = usuario.IdUsuario;
            uView.Nome = usuario.Nome;
            uView.Login = usuario.Login;
            uView.Perfil = perfil.Nome;

            return View(uView);
        }

        // GET: Listar usuários cadastrados no sistema
        public ActionResult Consulta()
        {
            // Verifica se usuário está autenticado e possui perfil de administrador
            if (!HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Admin"))
            {
                TempData["Falha"] = "Necessário estar autenticado com perfil de Administrador";
                return RedirectToAction("Login", "Usuario");
            }

            var listaUsuarioViewModel = new List<UsuarioViewModel>();
            Conexao con = new Conexao();
            UsuarioDal uDal = new UsuarioDal();

            try
            {
                List < Usuario > listaUsuarios = uDal.ListarUsuarios();
                foreach (var usuario in listaUsuarios)
                {
                    var uView = new UsuarioViewModel();

                    uView.Id = usuario.IdUsuario;
                    uView.Nome = usuario.Nome;
                    uView.Login = usuario.Login;
                    uView.Perfil = usuario.Perfil.Nome;

                    listaUsuarioViewModel.Add(uView);
                }
            }
            catch (Exception e)
            {
                TempData["Falha"] = e.Message;
            }
            return View(listaUsuarioViewModel);
        }

        // GET: Usuario/Cadastro
        public ActionResult Cadastro()
        {
            // Verifica se usuário está autenticado e possui perfil de administrador
            if (!HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Admin"))
            {
                TempData["Falha"] = "Necessário estar autenticado com perfil de Administrador";
                return RedirectToAction("Login", "Usuario");
            }

            UsuarioViewModel uViewCadastro = new UsuarioViewModel();
            PerfilDal pDal = new PerfilDal();
            uViewCadastro.Perfis = pDal.EnumerarPerfis();
            return View(uViewCadastro);
        }

        public ActionResult Excluir(int id)
        {
            // Verifica se usuário está autenticado e possui perfil de administrador
            if (!HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Admin"))
            {
                TempData["Falha"] = "Necessário estar autenticado com perfil de Administrador";
                return RedirectToAction("Login", "Usuario");
            }

            UsuarioDal uDal = new UsuarioDal();
            Usuario usuario = uDal.FindById(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            UsuarioViewModel uView = new UsuarioViewModel();
            PerfilDal pDal = new PerfilDal();
            

            uView.Id = usuario.IdUsuario;
            uView.Nome = usuario.Nome;
            uView.Login = usuario.Login;
            uView.Perfis = pDal.EnumerarPerfis();
            uView.Perfil = usuario.IdPerfil.ToString();

            return View(uView);
        }

        // POST: Usuario/Login
        [HttpPost]
        public ActionResult Login(UsuarioViewModelLogin model)
        {
            // Verifica se os dados do model estão de acordo com as regras do UsuarioViewModelLogin
            if (ModelState.IsValid)
            {
                try
                {
                    UsuarioDal uDal = new UsuarioDal();
                    Usuario usuario = uDal.FindByLoginSenha(model.Login, Criptografia.EncryptMD5(model.Senha));

                    if (usuario != null) //usuario foi encontrado?
                    {
                        // Transferir os dados do usuario para a classe de modelo..
                        UsuarioAutenticado auth = new UsuarioAutenticado();
                        auth.IdUsuario = usuario.IdUsuario;
                        auth.Nome = usuario.Nome;
                        auth.Login = usuario.Login;
                        auth.Perfil = usuario.Perfil.Nome;

                        // Criar o ticket de acesso
                        FormsAuthenticationTicket ticket =
                            new FormsAuthenticationTicket(JsonConvert.SerializeObject(auth), false, 5);

                        // Gravar o ticket em cookie
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                                           FormsAuthentication.Encrypt(ticket));
                        // Gravar o cookie
                        Response.Cookies.Add(cookie);

                        // Redirecionar para a área restrita
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                       TempData["Falha"] = "Acesso Negado. Tente novamente";
                    }
                    //ViewBag.Mensagem = "Acesso Negado.Tente novamente";
                }
                catch (Exception e)
                {
                    TempData["Falha"] = e.Message;
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Consulta(string buscaNome, string submit)
        {
            if (!HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Admin"))
            {
                TempData["Falha"] = "Necessário estar autenticado com perfil de Administrador";
                return RedirectToAction("Login", "Usuario");
            }

            var listaUsuarioViewModel = new List<UsuarioViewModel>();
            try
            {
                Conexao con = new Conexao();
                UsuarioDal uDal = new UsuarioDal();

                List<Usuario> listaUsuarios = uDal.ListarUsuarios();
                var uView = new UsuarioViewModel();

                if ((!String.IsNullOrEmpty(buscaNome)) && (submit == "Filtrar"))
                {
                    foreach (var usuario in listaUsuarios.Where(tbUsuario => tbUsuario.Nome.Contains(buscaNome)))
                    {
   

                        uView.Id = usuario.IdUsuario;
                        uView.Nome = usuario.Nome;
                        uView.Login = usuario.Login;
                        uView.Perfil = usuario.Perfil.Nome;

                        listaUsuarioViewModel.Add(uView);
                    }
                }
                else
                {
                    foreach (var dadosUsuario in listaUsuarios)
                    {
                        uView.Id = dadosUsuario.IdUsuario;
                        uView.Nome = dadosUsuario.Nome;
                        uView.Login = dadosUsuario.Login;
                        uView.Perfil = dadosUsuario.Perfil.Nome;

                        listaUsuarioViewModel.Add(uView);
                    }
                }
            }
            catch (Exception e)
            {
                TempData["Falha"] = e.Message;
            }
            return View(listaUsuarioViewModel);
        }

        // POST: Usuario/Cadastro
        [HttpPost] //submit do formulario
        public ActionResult Cadastro(UsuarioViewModel model)
        {
            // Verifica se usuário está autenticado e possui perfil de administrador
            if (!HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Admin"))
            {
                TempData["Falha"] = "Necessário estar autenticado com perfil de Administrador";
                return RedirectToAction("Login", "Usuario");
            }

            // Verifica se os dados do model estão de acordo com as regras do UsuarioViewModel
            if (ModelState.IsValid)
            {
                try
                {
                    UsuarioDal uDal = new UsuarioDal();
                    if (uDal.HasLogin(model.Login))
                    {
                        throw new Exception("Este Login já está cadastrado, tente outro");
                    }

                    Usuario usuario = new Usuario();
                    usuario.Nome = model.Nome;
                    usuario.Login = model.Login;
                    usuario.Senha = Criptografia.EncryptMD5(model.Senha);
                    usuario.IdPerfil = int.Parse(model.Perfil);
                    
                    // Gravar no Banco de Dados               
                    uDal.Insert(usuario);

                    TempData["Sucesso"] = "Usuário cadastrado com sucesso";
                    
                    // Limpar os campos do formulário
                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    TempData["Falha"] = e.Message;
                }
            }

            PerfilDal pDal = new PerfilDal();

            // Preencher DropDown List Perfil
            model.Perfis = pDal.EnumerarPerfis();

            return View(model);
        }

        // Ação para logout do usuário
        public ActionResult Logout()
        {
            // Remover o ticket do usuário
            FormsAuthentication.SignOut();

            // Redirecionar para a página inicial
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Senha(UsuarioAlterarSenha model)
        {
            if (ModelState.IsValid)
            {
                var auth = JsonConvert.DeserializeObject<UsuarioAutenticado>(HttpContext.User.Identity.Name);
                try
                {
                    UsuarioDal uDal = new UsuarioDal();
                    Usuario usuario = uDal.FindById(model.Id);
                    
                    if (Criptografia.EncryptMD5(model.SenhaAntiga).Equals(usuario.Senha))
                    {
                        usuario.Senha = Criptografia.EncryptMD5(model.SenhaNova);
                        uDal.Update(usuario);

                        ModelState.Clear();
                        TempData["Sucesso"] = "Senha alterada com sucesso";
                    }
                    else
                    {
                        TempData["Falha"] = "Senha antiga incorreta";
                        return View();
                    }
                }
                catch (Exception e)
                {
                    TempData["Falha"] = e.Message;
                }
            }
            else
            {
                TempData["Falha"] = "Dados incorretos. Verifique e tente novamente";
                return View();
            }
            return RedirectToAction("Perfil", "Usuario", new { model.Id });
        }


        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmaExcluir(int id)
        {
            // Verifica se usuário está autenticado e possui perfil de administrador
            if (!HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Admin"))
            {
                TempData["Falha"] = "Necessário estar autenticado com perfil de Administrador";
                return RedirectToAction("Login", "Usuario");
            }

            UsuarioDal uDal = new UsuarioDal();
            Usuario usuario = uDal.FindById(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            var auth = JsonConvert.DeserializeObject<UsuarioAutenticado>(HttpContext.User.Identity.Name);
            
            // Usuário Não pode excluir sua própria conta
            if (usuario.Login.Equals(auth.Login))
            {
                TempData["Falha"] = "Não é permitido excluir a própria conta";
                return RedirectToAction("Consulta", "Usuario");
            }

            try
            {
                uDal.Delete(usuario);
                TempData["Sucesso"] = "Usuário excluído com sucesso";
                return RedirectToAction("Consulta", "Usuario");
            }
            catch (Exception e)
            {
                TempData["Falha"] = e;
                return View();
            }
        }
    }
}