using Dominio.EntidadesNegocios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.InterfacesRepositorios
{
    public interface IRepositorioTipoPlanta : IRepositorio<TipoPlanta>
    {
        public TipoPlanta BuscarPorNombre(string nombre);
    }
}
