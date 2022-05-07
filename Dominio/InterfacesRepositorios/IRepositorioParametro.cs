using System;
using System.Collections.Generic;
using System.Text;
using Dominio.InterfacesRepositorios;
using Negocio.EntidadesNegocios;

namespace Dominio.InterfacesRepositorios
{   
    public interface IRepositorioParametro
    {
        public int BuscarRetornarValor( string nombre);
        bool Add(Parametro obj);
        bool Update(string nom, string value );
        Parametro FindById(string nom);
    }
}   
