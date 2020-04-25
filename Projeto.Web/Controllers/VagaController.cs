using Newtonsoft.Json;
using Projeto.DAL.DataSource;
using Projeto.DAL.Persistence;
using Projeto.Entities;
using Projeto.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.Web.Controllers
{
    public class VagaController : Controller
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
                //CandidatoViewModel cView = new CandidatoViewModel();
                //cView.Situacoes = GetSituacao();
                return View();
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

        // GET: Listar usuários cadastrados no sistema
        public ActionResult Consulta()
        {
            // Verifica se usuário está autenticado e possui perfil de administrador
            if (!HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("Admin"))
            {
                TempData["Falha"] = "Necessário estar autenticado com perfil de Administrador";
                return RedirectToAction("Login", "Usuario");
            }

            var listaVagaModel = new List<VagaModel>();
            Conexao con = new Conexao();
            VagaDal vDal = new VagaDal();

            try
            {
                List<Vaga> listaVagas = vDal.ListarVagas();
                foreach (var vaga in listaVagas)
                {
                    var vModel = new VagaModel();

                    vModel.Id = vaga.Id;
                    vModel.Descricao = vaga.Descricao;
                    vModel.Situacao = vaga.Situacao;
                    vModel.Salario = vaga.Salario;
                    vModel.TipoContratacao = vaga.TipoContratacao;
                    vModel.DataCadastro = vaga.DataCadastro;

                    listaVagaModel.Add(vModel);
                }
            }
            catch (Exception e)
            {
                TempData["Falha"] = e.Message;
            }
            return View(listaVagaModel);
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
        public ActionResult Consulta(string buscaNome, string buscaSituacao, string submit)
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
                    //if (((!String.IsNullOrEmpty(buscaNome)) || (!String.IsNullOrEmpty(buscaSituacao))) && (submit == "Filtrar"))
                    if (submit == "Filtrar")
                    {
                        var filtro = query;
                        if (!String.IsNullOrEmpty(buscaNome))
                        {
                            filtro = filtro.Where(tbCandidato => tbCandidato.Nome.Contains(buscaNome));
                        }
                        if (!String.IsNullOrEmpty(buscaSituacao))
                        {
                            filtro = filtro.Where(tbCandidato => tbCandidato.Situacao.Contains(buscaSituacao));
                        }


                        foreach (var dadosCandidato in filtro)
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
                        CandidatoViewModel cView2 = new CandidatoViewModel();
                        ViewBag.Situacoes = GetSituacao(cView2); ;
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
                        CandidatoViewModel cView2 = new CandidatoViewModel();
                        ViewBag.Situacoes = GetSituacao(cView2);
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
        public ActionResult Cadastro(VagaModel vModel)
        {
            if (ModelState.IsValid)
            {
                VagaDal vDal = new VagaDal();
                var auth = JsonConvert.DeserializeObject<UsuarioAutenticado>(HttpContext.User.Identity.Name);
                try
                {
                    Vaga vaga = new Vaga();

                    vaga.Descricao = vModel.Descricao;
                    vaga.Situacao = vModel.Situacao;
                    vaga.Salario = vModel.Salario;
                    vaga.TipoContratacao = vModel.TipoContratacao;
                    vaga.DataCadastro = DateTime.Now;
                    vaga.DataAtualizacao = DateTime.Now;
                    vaga.CadastradoPor = auth.Nome;
                    vaga.AtualizadoPor = auth.Nome;

                    vDal.Insert(vaga);
                    TempData["Sucesso"] = "Vaga cadastrada com sucesso";
                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    TempData["Falha"] = e.Message;
                }

                return RedirectToAction("Consulta", "Candidato");
            }
            return View();
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
                    TempData["Falha"] = e.Message;
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
