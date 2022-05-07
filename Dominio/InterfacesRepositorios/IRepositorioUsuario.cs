using Dominio.EntidadesNegocios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.InterfacesRepositorios
{
    public interface IRepositorioUsuario:IRepositorio<Usuario>
    {

        public Usuario Loguin(string e, string p);
    }
}
