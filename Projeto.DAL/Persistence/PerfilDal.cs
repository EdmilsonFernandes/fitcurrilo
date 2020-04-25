using Projeto.DAL.DataSource;
using Projeto.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Projeto.DAL.Persistence
{
    public class PerfilDal : GenericDal<Perfil>
    {
        public List<Perfil> ListarPerfis()
        {
            using (Conexao con = new Conexao())
            {
                var listaPerfil = new List<Perfil>();
                var query = from tbPerfil in con.Perfil
                            select tbPerfil;
                foreach (var perfil in query)
                {
                    listaPerfil.Add(perfil);
                }
                return listaPerfil;
            }
        }

        public IEnumerable<SelectListItem> EnumerarPerfis()
        {
            Conexao con = new Conexao();
            List<SelectListItem> listaPerfil = (from tbPerfil in con.Perfil.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = tbPerfil.Nome,
                                                    Value = tbPerfil.IdPerfil.ToString()
                                                }).ToList();
            return listaPerfil;
        }

    }
}
