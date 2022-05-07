using System;
using System.Collections.Generic;
using System.Text;
using Negocio.InterfacesRepositorios;

namespace Dominio.EntidadesNegocios
{
    public class Ficha:IValidar
    {
        public int ID { get; set; }
        public string FrecuenciaRiego { get; set; }
        public string TipoIluminacion { get; set; }
        public int  Temperatura { get; set; }

       /* public enum Iluminacion
        {
            SolarDirecta,
            SolarIndirecta,
            Sombra
        }*/

        public bool Validar()
        {
            return !string.IsNullOrEmpty(FrecuenciaRiego) && Temperatura > 0 && !string.IsNullOrEmpty(TipoIluminacion);
        }
    }   
}
