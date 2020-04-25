using Projeto.DAL.Persistence;
using Projeto.Entities;
using System;
using System.Web.Mvc;

namespace Projeto.Web.Controllers
{
    public class SituacaoController : Controller
    {
        public ActionResult Consulta()
        {
            // Verifica se usuário está autenticado e possui perfil de administrador
            if (!HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Admin"))
            {
                TempData["Falha"] = "Necessário estar autenticado com perfil de Administrador";
                return RedirectToAction("Login", "Usuario");
            }
            SituacaoDal sDal = new SituacaoDal();
            return View(sDal.ListarSituacoes());
        }

        public ActionResult Excluir(int id)
        {
            // Verifica se usuário está autenticado e possui perfil de administrador
            if (!HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Admin"))
            {
                TempData["Falha"] = "Necessário estar autenticado com perfil de Administrador";
                return RedirectToAction("Login", "Usuario");
            }

            SituacaoDal sDal = new SituacaoDal();
            Situacao situacao = sDal.FindById(id);

            // Verifica se a situação existe no B
            if (situacao == null)
            {
                return HttpNotFound();
            }

            return View(situacao);
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

            SituacaoDal sDal = new SituacaoDal();
            Situacao situacao = sDal.FindById(id);

            // Verifica se a situação existe no BD
            if (situacao == null)
            {
                return HttpNotFound();
            }

            // Situações com o valor "Novo Candidato" e "Currículo Visualizado" não podem ser excluídas
            if (situacao.Descricao.Equals("Novo Candidato") || situacao.Descricao.Equals("Currículo Visualizado"))
            {
                TempData["Falha"] = "Não é possível excluir o valor: " + situacao.Descricao + " da tabela situação";
                return RedirectToAction("Consulta", "Situacao");
            }

            // Situações em uso não podem ser excluídas
            if (sDal.SituacaoEmUso(situacao.Id))
            {
                TempData["Falha"] = "Valor: " + situacao.Descricao + ", está em uso no cadastro de um ou mais candidatos";
                return RedirectToAction("Consulta", "Situacao");
            }

            // Após as validações, exclui a situação
            try
            {
                sDal.Delete(situacao);
                TempData["Sucesso"] = "Registro excluído com sucesso";
                return RedirectToAction("Consulta", "Situacao");
            }
            catch (Exception e)
            {
                TempData["Falha"] = e.Message;
                return RedirectToAction("Consulta", "Situacao");
            }
        }

        [HttpPost]
        public ActionResult Consulta(string txtSituacao)
        {
            // Verifica se usuário está autenticado e possui perfil de administrador
            if (!HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Admin"))
            {
                TempData["Falha"] = "Necessário estar autenticado com perfil de Administrador";
                return RedirectToAction("Login", "Usuario");
            }

            SituacaoDal sDal = new SituacaoDal();
            Situacao novaSituacao = new Situacao();

            // Ao clicar no botão Adicionar, 
            // Adiciona nova situação no BD se o valor não for vazio
            if (txtSituacao != "" && txtSituacao != null)
            {

                // Verifica se o situação já existe no sistema
                if (sDal.SituacaoExiste(txtSituacao))
                {
                    TempData["Falha"] = "Já existe uma situação cadastrada no sistema com o nome: " + txtSituacao;
                }
                else
                {
                    novaSituacao.Descricao = txtSituacao;
                    sDal.Insert(novaSituacao);
                    TempData["Sucesso"] = "Situação cadastrada com sucesso";
                }
            }
            // Carrega lista de situações na tela novamente
            return View(sDal.ListarSituacoes());
        }

    }
}
