using Negocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;


namespace Dominio.EntidadesNegocios
{
    public class Planta:IValidar, IEquatable<Planta>
    {
        public int Id { get; set; }
        public string NombreVulgar { get; set; }//pueded ser uno o varios
        public string NombreCientifico { get; set; }
        public string Descripcion { get; set; }
        public int TopeDescripcion { get; set; }
        public int Altura { get; set; }
        public string Ambiente { get; set; } 
        public string ContinenteOrigen { get; set; }
        public string Foto { get; set; }//controlar que sea png o jpg
        public TipoPlanta Tipo { get; set; }
        public Ficha FichaPlanta { get; set; }//una ficha por planta


        private static int contador = 001;

        public enum TipoAmbiente
        {
            Interior ,
            Exterior,
            Mixta
        }

        public string Generarnombre(string n)
        {
            string nom = "";
            contador++;
            foreach (char item in n.Trim())
            {
                if (item == ' ')
                    nom += '_';
                nom += item;
            }
            nom += "00"+ contador;
            return nom;
        }

        public  bool Validar()
        {
            return !string.IsNullOrEmpty(NombreCientifico) && !string.IsNullOrEmpty(Foto) && !string.IsNullOrEmpty(Descripcion) && NombreVulgar.Length >5 && Altura>0 && FichaPlanta!=null;
        }

        public bool ValidarTope()
        {
            if (Validar())
            {
                return (Descripcion.Length <= TopeDescripcion);
            }return false;
        }

        public bool Equals([AllowNull] Planta other)
        {
            if (other == null) return false;
            return this.Id == other.Id;
        }

        public int CompareTo([AllowNull] Planta other)
        {
            if (this.Id.CompareTo(other.Id) > 0)//ordenar
            {
                return 1;
            }
            else 
            {
                return -1;
            }
        }


    }
}
