using Newtonsoft.Json;
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
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            return View();
        }

        // GET: Produto/Cadastro
        public ActionResult Cadastro()
        {
            return View();
        }

        // POST: Produto/Cadastro
        [HttpPost]
        public ActionResult Cadastro(ProdutoViewModelCadastro model)
        {
            if (ModelState.IsValid)
            {
                var auth = JsonConvert.DeserializeObject<UsuarioAutenticado>(HttpContext.User.Identity.Name);

                try
                {
                    ProdutoDal p = new ProdutoDal();
                    Produto produto = new Produto();

                    produto.Nome = model.Nome;
                    produto.Descricao = model.Descricao;

                    produto.CadastradoPor = auth.Nome;
                    produto.DataCadastro = DateTime.Now;
                    produto.EditadoPor = auth.Nome;
                    produto.DataEdicao = DateTime.Now;

                    p.Insert(produto);

                    ModelState.Clear();

                    ViewBag.Mensage = "Produto cadastrado com sucesso";
                }
                catch (Exception e)
                {
                    ViewBag.Mensage = e.Message;
                }

                

            }
            return View();
        }


        // GET: Produto/Consulta
        public ActionResult Consulta()
        {
            return View();
        }

        // GET: Produto
        public ActionResult Editar()
        {
            return View();
        }

        // GET: Produto
        public ActionResult Detalhes()
        {
            return View();
        }

        // GET: Produto
        public ActionResult Excluir()
        {
            return View();
        }



    }
}