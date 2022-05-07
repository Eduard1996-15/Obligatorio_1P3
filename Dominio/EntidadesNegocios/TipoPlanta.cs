using System;
using System.Collections.Generic;
using System.Text;
using Negocio.InterfacesRepositorios;

namespace Dominio.EntidadesNegocios
{
    public class TipoPlanta:IValidar
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<Planta> Plantas { get; set; }
        public int TopeDescripcion { get;  set; }

        public bool Validar()
        {
            return !string.IsNullOrEmpty(Nombre) &&
                !string.IsNullOrEmpty(Descripcion) && Descripcion.Length > 10;
        }

        public TipoPlanta() { }
        public TipoPlanta(string nom, string des)
        {
            //Validar();
            Nombre = nom;
            Descripcion = des;
        }
        public bool ValidarTope()
        {
            if (Validar()) 
            { 
                return (Descripcion.Length <= TopeDescripcion); 
            }
            return false;
        }
    }
}
