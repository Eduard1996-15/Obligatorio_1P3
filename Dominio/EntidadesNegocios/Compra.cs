using System;
using System.Collections.Generic;
using System.Text;
using Negocio.InterfacesRepositorios;

namespace Dominio.EntidadesNegocios
{
    public abstract class Compra:IValidar
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public List<ItemCompra> Items { get; set; }

        public abstract double CalcularPrecio();

        public abstract bool Validar();
    }

}
