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

namespace Projeto.Web.Controllers
{
    public class CandidatoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string ddlSituacao)
        {
            return View();
        }

        // GetSituacao para Cadastro: Apenas opção "Novo Candidato"
        private IEnumerable<SelectListItem> GetSituacao()
        {
            Conexao con = new Conexao();

            List<SelectListItem> situacaoList = (from tbSituacao in con.Situacao.AsEnumerable()
                                                 where tbSituacao.Descricao == "Novo Candidato"
                                                 select new SelectListItem
                                                 {
                                                     Text = tbSituacao.Descricao,
                                                     Value = tbSituacao.Id.ToString()
                                                 }).ToList();
            return situacaoList;
        }

        // GetSituacao com todos os valores cadastrados
        private IEnumerable<SelectListItem> GetSituacao(CandidatoViewModel cvm)
        {
            Conexao con = new Conexao();
            List<SelectListItem> situacaoList = (from tbSituacao in con.Situacao.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = tbSituacao.Descricao,
                                                     Value = tbSituacao.Id.ToString()
                                                 }).ToList();
            return situacaoList;
        }

        public ActionResult Gerenciamento()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (TempData["Mensagem"] != null)
                {
                    ViewBag.Message = TempData["Mensagem"].ToString();
                }
                return View();
            }
            return RedirectToAction("Login", "Usuario");
        }

        public ActionResult Cadastro()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                CandidatoViewModel cView = new CandidatoViewModel();
                cView.Situacoes = GetSituacao();
                return View(cView);
            }
            return RedirectToAction("Login", "Usuario");
        }

        public ActionResult Editar(int id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                CandidatoDal cDal = new CandidatoDal();
                Candidato candidato = cDal.FindById(id);
                CandidatoViewModel cView = new CandidatoViewModel();

                cView.Id = candidato.Id;
                cView.Nome = candidato.Nome;
                cView.GrauInstrucao = candidato.GrauInstrucao;
                cView.QtdeCertificados = candidato.QtdeCertificados;
                cView.Situacao = candidato.Situacao;
                cView.DataCadastro = candidato.DataCadastro;
                cView.DataAtualizacao = candidato.DataAtualizacao;
                cView.CadastradoPor = candidato.CadastradoPor;
                cView.AtualizadoPor = candidato.AtualizadoPor;
                cView.Observacao = candidato.Observacao;
                cView.Situacoes = GetSituacao(cView);

                return View(cView);
            }
            return RedirectToAction("Login", "Usuario");
        }

        // GET: Candidato/Consulta
        public ActionResult Consulta()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var listaInfosCandidato = new List<CandidatoViewModel>();
                try
                {
                    Conexao con = new Conexao();
                    var query = from tbCandidato in con.Candidato
                                join tbSituacao in con.Situacao on tbCandidato.Situacao equals tbSituacao.Id.ToString()
                                select new
                                {
                                    tbCandidato.Id,
                                    tbCandidato.Nome,
                                    tbCandidato.GrauInstrucao,
                                    tbCandidato.QtdeCertificados,
                                    tbCandidato.Situacao,
                                    tbCandidato.DataCadastro,
                                    tbCandidato.Observacao,
                                    tbSituacao.Descricao,
                                };

                    foreach (var dadosCandidato in query)
                    {
                        var cView = new CandidatoViewModel();

                        cView.Id = dadosCandidato.Id;
                        cView.Nome = dadosCandidato.Nome;
                        cView.GrauInstrucao = dadosCandidato.GrauInstrucao;
                        cView.QtdeCertificados = dadosCandidato.QtdeCertificados;
                        cView.Situacao = dadosCandidato.Descricao;
                        cView.DataCadastro = dadosCandidato.DataCadastro;
                        cView.Observacao = dadosCandidato.Observacao;

                        listaInfosCandidato.Add(cView);
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Mensagem = e.Message;
                }
                return View(listaInfosCandidato);
            }
            return RedirectToAction("Login", "Usuario");
        }

        public ActionResult Detalhes(int id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                CandidatoDal cDal = new CandidatoDal();
                Candidato candidato = cDal.FindById(id);
                CandidatoViewModel cView = new CandidatoViewModel();

                cView.Id = candidato.Id;
                cView.Nome = candidato.Nome;
                cView.GrauInstrucao = candidato.GrauInstrucao;
                cView.QtdeCertificados = candidato.QtdeCertificados;
                cView.Situacao = candidato.Situacao;
                cView.DataCadastro = candidato.DataCadastro;
                cView.DataAtualizacao = candidato.DataAtualizacao;
                cView.CadastradoPor = candidato.CadastradoPor;
                cView.AtualizadoPor = candidato.AtualizadoPor;
                cView.Observacao = candidato.Observacao;
                cView.Situacoes = GetSituacao(cView);

                return View(cView);
            }
            return RedirectToAction("Login", "Usuario");
        }

        public ActionResult Excluir(int id)
        {
            CandidatoDal cDal = new CandidatoDal();
            Candidato candidato = cDal.FindById(id);

            if (candidato == null)
            {
                return HttpNotFound();
            }
            else
            {
                CandidatoViewModel cView = new CandidatoViewModel();
               
                cView.Id = candidato.Id;
                cView.Nome = candidato.Nome;
                cView.GrauInstrucao = candidato.GrauInstrucao;
                cView.QtdeCertificados = candidato.QtdeCertificados;
                cView.Situacao = candidato.Situacao;
                cView.DataCadastro = candidato.DataCadastro;
                cView.DataAtualizacao = candidato.DataAtualizacao;
                cView.CadastradoPor = candidato.CadastradoPor;
                cView.AtualizadoPor = candidato.AtualizadoPor;
                cView.Observacao = candidato.Observacao;
                cView.Situacoes = GetSituacao(cView);

                return View(cView);
            }
        }

        [HttpPost]
        public ActionResult Consulta(string buscaNome, string submit)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var lista = new List<CandidatoViewModel>();
                try
                {
                    Conexao con = new Conexao();
                    var query = from tbCandidato in con.Candidato
                                join tbSituacao in con.Situacao on tbCandidato.Situacao equals tbSituacao.Id.ToString()
                                select new
                                {
                                    tbCandidato.Id,
                                    tbCandidato.Nome,
                                    tbCandidato.GrauInstrucao,
                                    tbCandidato.QtdeCertificados,
                                    tbCandidato.Situacao,
                                    tbCandidato.DataCadastro,
                                    tbCandidato.Observacao,
                                    tbSituacao.Descricao,
                                };
                    if ((!String.IsNullOrEmpty(buscaNome)) && (submit == "Filtrar"))
                    {
                        foreach (var dadosCandidato in query.Where(tbCandidato => tbCandidato.Nome.Contains(buscaNome)))
                        {
                            var cView = new CandidatoViewModel();

                            cView.Id = dadosCandidato.Id;
                            cView.Nome = dadosCandidato.Nome;
                            cView.GrauInstrucao = dadosCandidato.GrauInstrucao;
                            cView.QtdeCertificados = dadosCandidato.QtdeCertificados;
                            cView.Situacao = dadosCandidato.Descricao;
                            cView.Observacao = dadosCandidato.Observacao;
                            cView.DataCadastro = dadosCandidato.DataCadastro;

                            lista.Add(cView);
                        }
                        return View(lista);
                    }
                    else
                    {
                        foreach (var dadosCandidato in query)
                        {
                            var cView = new CandidatoViewModel();

                            cView.Id = dadosCandidato.Id;
                            cView.Nome = dadosCandidato.Nome;
                            cView.GrauInstrucao = dadosCandidato.GrauInstrucao;
                            cView.QtdeCertificados = dadosCandidato.QtdeCertificados;
                            cView.Situacao = dadosCandidato.Descricao;
                            cView.Observacao = dadosCandidato.Observacao;
                            cView.DataCadastro = dadosCandidato.DataCadastro;

                            ModelState.Clear();
                            lista.Add(cView);
                        }
                        return View(lista);
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Mensagem = e.Message;
                }
            }
            return RedirectToAction("Login", "Usuario");
        }

        // Verifica se o tipo do arquivo é permitido 
        // Apenas PDF DOC e DOCX são permitidos
        public bool ValidaTipoCurriculo(string mimeType)
        {
            bool retorno = false;

            if (mimeType.Equals("application/pdf") //.pdf
                || mimeType.Equals("application/msword") //.doc
                || mimeType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document") //.docx
               )
            {
                retorno = true;
            }
            return retorno;
        }

        [HttpPost]
        public ActionResult Cadastro(CandidatoViewModel cView, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                CandidatoDal cDal = new CandidatoDal();
               
                // Verifica se o nome do candidato já existe no sistema
                if (cDal.CandidatoExiste(cView.Nome))
                {
                    TempData["Falha"] = "Já existe um candidato cadastrado no sistema com o nome: " + cView.Nome;
                    return RedirectToAction("Cadastro", "Candidato");
                }

                var auth = JsonConvert.DeserializeObject<UsuarioAutenticado>(HttpContext.User.Identity.Name);
                try
                {
                    Candidato candidato = new Candidato();

                    candidato.Nome = cView.Nome;
                    candidato.GrauInstrucao = cView.GrauInstrucao;
                    candidato.QtdeCertificados = cView.QtdeCertificados;
                    candidato.Situacao = cView.Situacao;
                    candidato.DataCadastro = DateTime.Now;
                    candidato.DataAtualizacao = DateTime.Now;
                    candidato.CadastradoPor = auth.Nome;
                    candidato.AtualizadoPor = auth.Nome;
                    candidato.Observacao = cView.Observacao;

                    if (upload != null && upload.ContentLength > 0)
                    {
                        // Verifica se o tipo do arquivo é permitido 
                        // Apenas PDF DOC e DOCX são permitidos
                        if (!ValidaTipoCurriculo(upload.ContentType))
                        {
                            TempData["Falha"] = "Tipo de arquivo não permitido";
                            return RedirectToAction("Cadastro", "Candidato");
                        }

                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            Curriculo curriculo = new Curriculo(); 
                            curriculo.Nome = upload.FileName;
                            curriculo.Tamanho = upload.ContentLength;
                            curriculo.Tipo = upload.ContentType;
                            curriculo.Conteudo = reader.ReadBytes(upload.ContentLength);
                            curriculo.DataCadastro = DateTime.Now;
                            curriculo.CadastradoPor = auth.Nome;
                            candidato.Curriculos = new List<Curriculo> { curriculo };
                        }
                    }
                    cDal.Insert(candidato);
                    TempData["Sucesso"] = "Candidato cadastrado com sucesso";
                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    TempData["Falha"] = e.Message;
                }

                return RedirectToAction("Consulta", "Candidato");
            }
            CandidatoViewModel cView2 = new CandidatoViewModel();
            cView2.Situacoes = GetSituacao();
            return View(cView2);
        }

        [HttpPost]
        public ActionResult Editar(CandidatoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var auth = JsonConvert.DeserializeObject<UsuarioAutenticado>(HttpContext.User.Identity.Name);
                try
                {
                    CandidatoDal c = new CandidatoDal();
                    Candidato candidato = c.FindById(model.Id);

                    candidato.Nome = model.Nome;
                    candidato.GrauInstrucao = model.GrauInstrucao;
                    candidato.QtdeCertificados = model.QtdeCertificados;
                    candidato.Situacao = model.Situacao;
                    candidato.DataCadastro = model.DataCadastro;
                    candidato.DataAtualizacao = DateTime.Now;
                    candidato.CadastradoPor = model.CadastradoPor;
                    candidato.AtualizadoPor = auth.Nome;
                    candidato.Observacao = model.Observacao;

                    c.Update(candidato);

                    ModelState.Clear();
                    TempData["Sucesso"] = "Registro alterado com sucesso";
                }
                catch (Exception e)
                {
                    TempData["Falha"] = e.Message;
                    //return RedirectToAction("Detalhes", "Candidato", new { model.Id });
                }
            }
            return RedirectToAction("Detalhes", "Candidato", new { model.Id });
        }
        public ActionResult AddCurriculo(int id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                CandidatoDal c = new CandidatoDal();
                Candidato candidato = c.FindById(id);
                CandidatoViewModel cvm = new CandidatoViewModel();

                cvm.Id = candidato.Id;
                cvm.Nome = candidato.Nome;
                cvm.GrauInstrucao = candidato.GrauInstrucao;
                cvm.QtdeCertificados = candidato.QtdeCertificados;
                cvm.Situacao = candidato.Situacao;
                cvm.DataCadastro = candidato.DataCadastro;
                cvm.DataAtualizacao = candidato.DataAtualizacao;
                cvm.CadastradoPor = candidato.CadastradoPor;
                cvm.AtualizadoPor = candidato.AtualizadoPor;
                cvm.Observacao = candidato.Observacao;
                cvm.Situacoes = GetSituacao(cvm);

                return View(cvm);
            }
            return RedirectToAction("Login", "Usuario");
        }

        [HttpPost]
        public ActionResult AddCurriculo(CandidatoViewModel model, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid)
            {
                var auth = JsonConvert.DeserializeObject<UsuarioAutenticado>(HttpContext.User.Identity.Name);
                try
                {
                    // Verifica se o tipo do arquivo é permitido 
                    // Apenas PDF DOC e DOCX são permitidos
                    if (!ValidaTipoCurriculo(upload.ContentType))
                    {
                        TempData["Falha"] = "Tipo de arquivo não permitido";
                        return RedirectToAction("Detalhes", "Candidato", new { model.Id });
                    }

                    CandidatoDal c = new CandidatoDal();
                    Candidato candidato = c.FindById(model.Id);

                    if (upload != null && upload.ContentLength > 0)
                    {
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            CurriculoDal cu = new CurriculoDal();
                            Curriculo curriculo = new Curriculo();
                            curriculo.Nome = upload.FileName;
                            curriculo.Tamanho = upload.ContentLength;
                            curriculo.Tipo = upload.ContentType;
                            curriculo.Conteudo = reader.ReadBytes(upload.ContentLength);
                            curriculo.DataCadastro = DateTime.Now;
                            curriculo.CadastradoPor = auth.Nome;
                            curriculo.IdCandidato = candidato.Id;
                            cu.Insert(curriculo);
                            candidato.Curriculos = new List<Curriculo> { curriculo };
                        }
                    }
                    c.Update(candidato);
                    TempData["Sucesso"] = "Currículo adicionado com sucesso";
                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    TempData["Falha"] =  e.Message;
                }
                return RedirectToAction("Detalhes", "Candidato", new { model.Id });
            }
            return View();
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmaExcluir(int id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                CandidatoDal cDal = new CandidatoDal();
                Candidato candidato = cDal.FindById(id);
                if (candidato == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    // Usuário é Admin
                    if (HttpContext.User.IsInRole("Admin"))
                    {
                        try
                        {
                            cDal.Delete(candidato);
                            TempData["Sucesso"] = "Candidato excluído com sucesso";
                            return RedirectToAction("Consulta", "Candidato");
                        }
                        catch (Exception e)
                        {
                            TempData["Falha"] = e;
                            return View();
                        }
                    }
                    else
                    {
                        TempData["Falha"] = "Usuário não possui permissão de exclusão";
                        return RedirectToAction("Detalhes", "Candidato", new { candidato.Id });
                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }
    }
}
