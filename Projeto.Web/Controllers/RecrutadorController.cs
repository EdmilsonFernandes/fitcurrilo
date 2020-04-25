using Projeto.DAL.Persistence; //acesso a banco..
using Projeto.Entities; //entidades..
using Projeto.Web.Models; //classes de modelo..
using System;
using System.Web.Mvc;
using System.Web.Security;


namespace Projeto.Web.Controllers
{
    public class RecrutadorController : Controller
    {
        public ActionResult Consulta()
        {
            // Verifica se usuário está autenticado e possui perfil de administrador
            if (!HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Admin"))
            {
                TempData["Falha"] = "Necessário estar autenticado com perfil de Administrador";
                return RedirectToAction("Login", "Usuario");
            }
            RecrutadorDal rDal = new RecrutadorDal();
            return View(rDal.ListarRecrutadores());
        }

        public ActionResult Cadastro()
        {
            // Verifica se usuário está autenticado e possui perfil de administrador
            if (!HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Admin"))
            {
                TempData["Falha"] = "Necessário estar autenticado com perfil de Administrador";
                return RedirectToAction("Login", "Usuario");
            }

            RecrutadorViewModel rViewCadastro = new RecrutadorViewModel();
            return View(rViewCadastro);
        }
        // POST: Usuario/Cadastro
        [HttpPost]
        public ActionResult Cadastro(RecrutadorViewModel rView)
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
                    RecrutadorDal rDal = new RecrutadorDal();
                    if (rDal.RecrutadorExiste(rView.Nome))
                    {
                        TempData["Falha"] = "Este Recrutador já está cadastrado, tente outro nome";
                        return RedirectToAction("Cadastro", "Recrutador");
                    }
                    if (!String.IsNullOrEmpty(rView.Email) && rDal.EmailEmUso(rView.Email))
                    {
                        TempData["Falha"] = "Este e-mail já está em uso por outro recrutador";
                        return RedirectToAction("Cadastro", "Recrutador");
                    }
                    
                    Recrutador recrutador = new Recrutador();
                    recrutador.Nome = rView.Nome;
                    recrutador.Email = rView.Email;

                    // Gravar no Banco de Dados               
                    rDal.Insert(recrutador);
                    TempData["Sucesso"] = "Recrutador cadastrado com sucesso";
                }
                catch (Exception e)
                {
                    TempData["Falha"] = e.Message;
                }
                return RedirectToAction("Consulta", "Recrutador");
            }
            return View(rView);
        }





        public ActionResult Excluir(int id)
        {
            // Verifica se usuário está autenticado e possui perfil de administrador
            if (!HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Admin"))
            {
                TempData["Falha"] = "Necessário estar autenticado com perfil de Administrador";
                return RedirectToAction("Login", "Usuario");
            }

            RecrutadorDal rDal = new RecrutadorDal();
            Recrutador recrutador = rDal.FindById(id);

            // Verifica se a situação existe no B
            if (recrutador == null)
            {
                return HttpNotFound();
            }

            return View(recrutador);
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

            RecrutadorDal rDal = new RecrutadorDal();
            Recrutador recrutador = rDal.FindById(id);

            // Verifica se a situação existe no BD
            if (recrutador == null)
            {
                return HttpNotFound();
            }

            // Situações com o valor "Novo Candidato" e "Currículo Visualizado" não podem ser excluídas
           // if (recrutador.Descricao.Equals("Novo Candidato") || situacao.Descricao.Equals("Currículo Visualizado"))
           // {
           //     TempData["Falha"] = "Não é possível excluir o valor: " + situacao.Descricao + " da tabela situação";
           //     return RedirectToAction("Consulta", "Situacao");
           // }

            // Situações em uso não podem ser excluídas
         //  if (rdal.SituacaoEmUso(situacao.Id))
         //  {
         //      TempData["Falha"] = "Valor: " + situacao.Descricao + ", está em uso no cadastro de um ou mais candidatos";
         //      return RedirectToAction("Consulta", "Situacao");
         //  }

            // Após as validações, exclui a situação
            try
            {
                rDal.Delete(recrutador);
                TempData["Sucesso"] = "Registro excluído com sucesso";
                return RedirectToAction("Consulta", "Recrutador");
            }
            catch (Exception e)
            {
                TempData["Falha"] = e.Message;
                return RedirectToAction("Consulta", "Recrutador");
            }
        }

        [HttpPost]
        public ActionResult Consulta(string txtRecrutador)
        {
            // Verifica se usuário está autenticado e possui perfil de administrador
            if (!HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Admin"))
            {
                TempData["Falha"] = "Necessário estar autenticado com perfil de Administrador";
                return RedirectToAction("Login", "Usuario");
            }

            RecrutadorDal rDal = new RecrutadorDal();
            Recrutador novoRecrutador = new Recrutador();

            // Ao clicar no botão Adicionar, 
            // Adiciona nova situação no BD se o valor não for vazio
            if (txtRecrutador != "" && txtRecrutador != null)
            {

                // Verifica se o situação já existe no sistema
               // if (rDal.SExiste(txtSituacao))
               // {
               //     TempData["Falha"] = "Já existe uma situação cadastrada no sistema com o nome: " + txtSituacao;
               // }
               // else
               // {
                    novoRecrutador.Nome = txtRecrutador;
                    rDal.Insert(novoRecrutador);
                    TempData["Sucesso"] = "Recrutador cadastrado com sucesso";
                //}
            }
            // Carrega lista de situações na tela novamente
            return View(rDal.ListarRecrutadores());
        }

    }
}
