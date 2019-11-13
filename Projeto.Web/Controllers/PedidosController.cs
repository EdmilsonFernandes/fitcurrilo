using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto.DAL.Persistence;
using Projeto.Entities;
using Projeto.Web.Models;
using Newtonsoft.Json;

namespace Projeto.Web.Controllers
{
    public class PedidosController : Controller
    {
        // GET: CPLAB/Pedidos/Cadastro
        public ActionResult Cadastro()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                //redirecionamento..
                return RedirectToAction("Login", "Usuario");
            }

            return View();
        }

        // GET: CPLAB/Pedidos/Consulta
        public ActionResult Consulta()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                //redirecionamento..
                return RedirectToAction("Login", "Usuario");
            }

            var lista = new List<AprovacaoCurriculosViewModelConsulta>();


            try
            {
                AprovacaoPedidosDal context = new AprovacaoPedidosDal();

                foreach (AprovacaoCurriculos a in context.FindAll())
                {
                    var model = new AprovacaoCurriculosViewModelConsulta();

                    model.NumeroOFProducao = a.NomeCandidato;
                    model.DataFabricacao = a.DataDeCadastro;
                    model.DataAnalize = a.DataDeVisto;
                    model.NumeroCertificado = a.NumeroCertificado;
                    model.NumeroOFLaboratorio = a.NumeroOFLaboratorio;
                    model.NomeProduto = a.NomeProduto;
                    model.QuantidadeKG = a.QuantidadeKG;
                    model.Situacao = a.Situacao;
                    model.AtualizadoPor = a.AtualizadoPor;

                    lista.Add(model);
                }



            }
            catch (Exception e)
            {

                ViewBag.Mensagem = e.Message;
            }

            return View(lista);
        }

        // POST: CPLAB/Pedidos/Cadastro
        [HttpPost]
        public ActionResult Cadastro(AprovacaoCurriculos model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var auth = JsonConvert.DeserializeObject<UsuarioAutenticado>(HttpContext.User.Identity.Name);

                    AprovacaoCurriculos pedidos = new AprovacaoCurriculos();
                    pedidos.NomeCandidato = model.NomeCandidato;
                    pedidos.DataDeCadastro = model.DataDeCadastro;
                    pedidos.DataDeVisto = model.DataDeVisto;
                    pedidos.NumeroCertificado = model.NumeroCertificado;
                    pedidos.NumeroOFLaboratorio = model.NumeroOFLaboratorio;
                    pedidos.NomeProduto = model.NomeProduto;
                    pedidos.QuantidadeKG = model.QuantidadeKG;
                    pedidos.Situacao = model.Situacao;
                    pedidos.Observacao = model.Observacao;
                    pedidos.AtualizadoPor = auth.Nome;

                    AprovacaoPedidosDal context = new AprovacaoPedidosDal();

                    context.Insert(pedidos);

                    ModelState.Clear();

                }
                catch (Exception e)
                {

                    ViewBag.Message = e.Message;
                }
            }
            return View();
        }

    }
}
