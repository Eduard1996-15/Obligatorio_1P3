using Dominio.EntidadesNegocios;
using Dominio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorios.RepositoriosMemoria
{
    public class RepositorioTipoPlanta : IRepositorioTipoPlanta
    {

        public static List<TipoPlanta> TipoPlantas { get; set; } = new List<TipoPlanta>();

        public bool Add(TipoPlanta obj)
        {
            bool ok = false;
            if (obj.Validar() && FindById(obj.Id) == null)
            {
                TipoPlantas.Add(obj);
                ok = true;
            }
            return ok;
        }

        public TipoPlanta BuscarPorNombre(string nombre)
        {
            TipoPlanta buscado = null;
            foreach (TipoPlanta pl in TipoPlantas)
            {
                if (pl.Nombre == nombre) buscado = pl;
            }
            return buscado;
        }

        public List<TipoPlanta> FindAll()
        {
            return TipoPlantas;
        }

        public TipoPlanta FindById(int clave)
        {
            TipoPlanta buscado = null;
            //buscado = RepositorioClientesMemoria.Clientes.Where(c => c.Id == id).SingleOrDefault();
            foreach (TipoPlanta pl in TipoPlantas)
            {
                if (pl.Id == clave) buscado = pl;
            }
            return buscado;
        }

        public IEnumerable<TipoPlanta> ListarPorTipo(string nombre)
        {
            List<TipoPlanta> listadoPorTipo = new List<TipoPlanta>();

            foreach (TipoPlanta tp in RepositorioTipoPlanta.TipoPlantas)
            {
                if (tp.Nombre == nombre)
                    listadoPorTipo.Add(tp);
            }
            return listadoPorTipo;

        }

        public bool Remove(int clave)
        {
            bool ok = false;
            TipoPlanta aBorrar = this.FindById(clave);
            if (aBorrar != null)
            {
                TipoPlantas.Remove(aBorrar);
                ok = true;
            }
            return ok;
        }

        public bool Update(TipoPlanta obj)
        {
            bool ok = false;
            TipoPlanta aModificar = this.FindById(obj.Id);
            if (obj.Validar() && aModificar != null)
            {
                aModificar.Nombre = obj.Nombre;
                aModificar.Descripcion = obj.Descripcion;
                aModificar.Plantas = obj.Plantas;
                ok = true;
            }
            return ok;
        }
    }
}
