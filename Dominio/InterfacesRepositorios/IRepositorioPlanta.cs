using System;
using System.Collections.Generic;
using System.Text;
using Dominio.EntidadesNegocios;
using Negocio.InterfacesRepositorios;

namespace Dominio.InterfacesRepositorios
{
    public interface IRepositorioPlanta:IRepositorio<Planta>
    {
        IEnumerable<Planta> ListarPorNombre(string nombre);
        IEnumerable<Planta> ListarPorAmbiente(string tipoAmbiente); //TODO Cambiar tipo de dato
        IEnumerable<Planta> ListarPorAlturaMenor(int altura);
        IEnumerable<Planta> ListarPorAlturaMayor(int altura);
        IEnumerable<Planta> ListarPorTipo(string nombreTipo);


    }
}
