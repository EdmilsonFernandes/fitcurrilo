using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto.Web.Models;
using Projeto.Entities;
using Newtonsoft.Json;
using Projeto.DAL.Persistence;
using Projeto.DAL.DataSource;
using PagedList;
using System.IO;
using System.Net;
using System.Security.Principal;

namespace Projeto.Web.Controllers
{
    public class CurriculoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastro(CandidatoViewModel cvm)
        {
            CandidatoDal c = new CandidatoDal();

            Candidato candidato = c.FindById(cvm.Id);

            return View(candidato);
        }

        [HttpPost]
        public ActionResult Cadastro(CandidatoViewModel cvm, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (!
                    (upload.ContentType.Equals("application / pdf"))
                    //|| upload.ContentType.Equals())
                    //|| upload.ContentType.Equals())
                    )
                {
                    TempData["Falha"] = "Não é PDF";

                }
                var auth = JsonConvert.DeserializeObject<UsuarioAutenticado>(HttpContext.User.Identity.Name);
                try
                {
                    CandidatoDal c = new CandidatoDal();

                    if (upload != null && upload.ContentLength > 0)
                    {
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            Curriculo curriculo = new Curriculo();
                            curriculo.Nome = upload.FileName;
                            curriculo.Tamanho = upload.ContentLength;
                            curriculo.Tipo = upload.ContentType;
                            curriculo.Conteudo = reader.ReadBytes(upload.ContentLength);
                            curriculo.DataCadastro = DateTime.Now;
                            curriculo.CadastradoPor = auth.Nome;
                            cvm.Curriculos = new List<Curriculo> { curriculo };
                        }
                    }
                    //c.Update(cvm);
                    TempData["Sucesso"] = "Candidato cadastrado com sucesso!";
                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    TempData["Falha"] = e.Message;
                }

                return RedirectToAction("Gerenciamento", "Candidato");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(string ddlSituacao)
        {
            return View();
        }

        // GET: Candidato/Consulta
        public ActionResult Consulta(int id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var listaInfosCurriculo = new List<CurriculoViewModel>();
                try
                {
                    Conexao con = new Conexao();

                    var query = from tbCurriculo in con.Curriculo
                                where tbCurriculo.IdCandidato == id
                                select tbCurriculo;

                    foreach (var dadosCurriculo in query)
                    {
                        var model = new CurriculoViewModel();

                        model.Id = dadosCurriculo.Id;
                        model.Nome = dadosCurriculo.Nome;
                        model.DataCadastro = dadosCurriculo.DataCadastro;
                        model.Tamanho = dadosCurriculo.Tamanho;
                        model.CadastradoPor = dadosCurriculo.CadastradoPor;

                        listaInfosCurriculo.Add(model);
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Mensagem = e.Message;
                }
                return View(listaInfosCurriculo);
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
    
        }

        // GET: /Movies/Delete/5
        public ActionResult Excluir(int id)
        {
            CurriculoDal cuDal = new CurriculoDal();
            Curriculo curriculo = cuDal.FindById(id);
            if (curriculo == null)
            {
                return HttpNotFound();
            }
            else
            {
                CurriculoViewModel cuView = new CurriculoViewModel();

                cuView.Id = curriculo.Id;
                cuView.Nome = curriculo.Nome;
                cuView.Tamanho = curriculo.Tamanho;
                cuView.Tipo = curriculo.Tipo;
                cuView.DataCadastro = curriculo.DataCadastro;
                cuView.CadastradoPor = curriculo.CadastradoPor;
                cuView.IdCandidato = curriculo.IdCandidato;
                return View(cuView);
            }
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmaExcluir(int id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                UsuarioAutenticado usuarioAutenticado = new UsuarioAutenticado();
                UsuarioDal uDal = new UsuarioDal();
                CurriculoDal cuDal = new CurriculoDal();
                Curriculo curriculo = cuDal.FindById(id);
                
                if (curriculo == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    int idCandidato = curriculo.IdCandidato;
                    
                    // Usuário é Admin
                    if (HttpContext.User.IsInRole("Admin"))
                    {
                        try
                        {
                            cuDal.Delete(curriculo);
                            TempData["Sucesso"] = "Currículo excluído com sucesso";
                            return RedirectToAction("Detalhes", "Candidato", new { id = idCandidato });
                        }
                        catch (Exception e)
                        {
                            TempData["Falha"] = e.Message;
                            return View();
                        }
                    }
                    else
                    {
                        TempData["Falha"] = "Usuário não possui permissão de exclusão";
                        return RedirectToAction("Detalhes", "Candidato", new { id = idCandidato });
                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }
        public ActionResult Visualizar(int id)
        {
            CurriculoDal cuDal = new CurriculoDal();
            Curriculo curriculo = cuDal.FindById(id);
            byte[] conteudo = (byte[])curriculo.Conteudo;
            return File(conteudo, curriculo.Tipo);
        }
    }
}

