using Projeto.DAL.Persistence;
using Projeto.Entities;
using System;
using System.Web.Mvc;

namespace Projeto.Web.Controllers
{

            public class TecnologiaController : Controller
    {
        public ActionResult Consulta()
        {
            // Verifica se usuário está autenticado e possui perfil de administrador
            if (!HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Admin"))
            {
                TempData["Falha"] = "Necessário estar autenticado com perfil de Administrador";
                return RedirectToAction("Login", "Usuario");
            }
            GenericDal<Tecnologia> tDal = new GenericDal<Tecnologia>();
            return View(tDal.FindAll());
        }

        public ActionResult Excluir(int id)
        {
            // Verifica se usuário está autenticado e possui perfil de administrador
            if (!HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Admin"))
            {
                TempData["Falha"] = "Necessário estar autenticado com perfil de Administrador";
                return RedirectToAction("Login", "Usuario");
            }
            GenericDal<Tecnologia> tDal = new GenericDal<Tecnologia>();
            Tecnologia tecnologia = tDal.FindById(id);

            // Verifica se a situação existe no B
            if (tecnologia == null)
            {
                return HttpNotFound();
            }

            return View(tecnologia);
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

            TecnologiaDal tDal = new TecnologiaDal();
            Tecnologia tecnologia = tDal.FindById(id);

            // Verifica se a situação existe no BD
            if (tecnologia == null)
            {
                return HttpNotFound();
            }

            // Tecnologias em uso não podem ser excluídas
            //if (tDal.IsTecnologiaEmUso (tecnologia.Id))
            //{
            //    TempData["Falha"] = "Valor: " + tecnologia.Nome + ", está em uso no cadastro de um ou mais candidatos";
            //    return RedirectToAction("Consulta", "tecnologia");
            //}

            // Após as validações, exclui a tecnologia
            try
            {
                tDal.Delete(tecnologia);
                TempData["Sucesso"] = "Registro excluído com sucesso";
                return RedirectToAction("Consulta", "tecnologia");
            }
            catch (Exception e)
            {
                TempData["Falha"] = e.Message;
                return RedirectToAction("Consulta", "tecnologia");
            }
        }

        [HttpPost]
        public ActionResult Consulta(string txtTecnologia)
        {
            // Verifica se usuário está autenticado e possui perfil de administrador
            if (!HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Admin"))
            {
                TempData["Falha"] = "Necessário estar autenticado com perfil de Administrador";
                return RedirectToAction("Login", "Usuario");
            }

            TecnologiaDal tDal = new TecnologiaDal();
            Tecnologia novaTecnologia = new Tecnologia();

            // Ao clicar no botão Adicionar, 
            // Adiciona nova situação no BD se o valor não for vazio
            if (txtTecnologia != "" && txtTecnologia != null)
            {

                // Verifica se o situação já existe no sistema
                if (tDal.TecnologiaExiste(txtTecnologia))
                {
                    TempData["Falha"] = "Já existe uma situação cadastrada no sistema com o nome: " + txtTecnologia;
                }
                else
                {
                    novaTecnologia.Nome = txtTecnologia;
                    tDal.Insert(novaTecnologia);
                    TempData["Sucesso"] = "Tecnologia cadastrada com sucesso";
                }
            }
            // Carrega lista de situações na tela novamente
            return View(tDal.ListarTecnologias());
        }

    }
}
