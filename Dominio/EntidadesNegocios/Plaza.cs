using System;
using System.Collections.Generic;
using System.Text;
using Negocio.EntidadesNegocios;

namespace Dominio.EntidadesNegocios
{
    public class Plaza : Compra
    {
        public int CostoFlete { get; set; }
        public double IVA { get; set; }

        
        public override double CalcularPrecio()
        {
            double costoT = 0;
            costoT += (CostoFlete + (double)IVA);
            foreach (ItemCompra item in Items)
            {
                costoT += item.CostoSubTotal();
            }
            return costoT;
        }

      

        public override bool Validar()
        {
             if ( CostoFlete > 0)
            {
                return true;
            }
            return false;
        }
        
    }
}
