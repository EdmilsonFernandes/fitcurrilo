using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }


        #region Relacionamentos

        public int IdPerfil { get; set; } //chave estrangeira..
        //Usuario TEM 1 Perfil
        public virtual Perfil Perfil { get; set; }

        #endregion
    }
}
