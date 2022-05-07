using System;
using System.Collections.Generic;
using System.Text;
using Negocio.InterfacesRepositorios;

namespace Dominio.EntidadesNegocios
{
    public class ItemCompra:IValidar
    {
        public int Id { get; set; }
        public Planta Planta { get; set; }
        public int Cantidad { get; set; }   
        public double PrecioUnitario { get; set; }

        public double CostoSubTotal()
        {
            return Cantidad * PrecioUnitario;
        }

        public bool Validar()
        {
            return Cantidad > 0 && Planta != null && PrecioUnitario > 0;
        }
    }
}
