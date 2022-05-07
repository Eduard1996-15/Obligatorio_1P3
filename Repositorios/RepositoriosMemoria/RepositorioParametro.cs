using Dominio.InterfacesRepositorios;
using Negocio.EntidadesNegocios;
using Negocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.RepositoriosMemoria
{
    public class RepositorioParametro : IRepositorioParametro
    {
        public static List<Parametro> parametros { get; set; } = new List<Parametro>();
        public bool Add(Parametro obj)
        {
            bool ok = false;
            if (FindById(obj.Nombre) == null)
            {
                parametros.Add(obj);
                ok = true;
            }
            return ok;
        }

        public int BuscarRetornarValor(string nombre)
        {
            foreach (Parametro item in parametros)
            {
                if (item.Nombre == nombre)
                    return int.Parse(item.Valor); 
            }
            return 0;
        }

        public List<Parametro> FindAll()
        {
            return parametros;
        }

        public Parametro FindById(string nombre)
        {
            throw new NotImplementedException();
        }

        public bool Update(string nom, string  val)
        {
            Parametro modificar = new Parametro();
            if(!string.IsNullOrEmpty(val) && !string.IsNullOrEmpty(nom) && FindById(nom) != null)
            {
                modificar.Nombre = nom;
                modificar.Valor = val;
                return true;
            }
            return false; 
        }
    }
}
