using System.Collections.Generic;

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
