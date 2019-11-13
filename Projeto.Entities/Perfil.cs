using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
    public class Perfil
    {
        public int IdPerfil { get; set; }
        public string Nome { get; set; }

        #region Relacionamentos

        //Perfil TEM Muitos Usuarios..
        public virtual List<Usuario> Usuarios { get; set; }

        #endregion
    }
}
