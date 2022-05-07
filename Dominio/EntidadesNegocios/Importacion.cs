using System;
using System.Collections.Generic;
using System.Text;
using Negocio.EntidadesNegocios;

namespace Dominio.EntidadesNegocios
{
    public class Importacion: Compra
    {
        public double ImpuestoImportacion { get; set; }//falta arreglar
        public double TasaArancel { get; set; }
        public string DescripcionMedidasSanitaria { get; set; }

        public override double CalcularPrecio()
        {
            double costoT = 0;
            foreach (ItemCompra item in Items)
            {
                costoT += item.CostoSubTotal();
                if (item.Planta.ContinenteOrigen == "si")
                    costoT -= TasaArancel;//si es del continente origen se hace un descuento de la tasa arancel
                else
                    costoT += (TasaArancel + (double)ImpuestoImportacion);//si no se aumenta impuesto de importacion + tasa arancel
            }
            
            return costoT;
        }

        public override bool Validar()
        {
            if(!string.IsNullOrEmpty(DescripcionMedidasSanitaria) && (double)ImpuestoImportacion > 0 && TasaArancel > 0){
                return true;
            }
            return false;
            
        }
    }
}
