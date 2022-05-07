using System;
using System.Collections.Generic;
using System.Text;
using Dominio.EntidadesNegocios;
using Dominio.InterfacesRepositorios;
using System.Linq;

namespace Repositorios.RepositoriosMemoria
{
    public class RepositorioCompra : IRepositorioCompra
    {
        public static List<Compra> Compras { get; set; } = new List<Compra>();

        public bool Add(Compra obj)
        {
            bool ok = false;
            if (obj.Validar() && FindById(obj.Id) == null)
            {
                Compras.Add(obj);
                ok = true;
            }
            return ok;
        }

        public List<Compra> FindAll()
        {
            return Compras;
        }

        public Compra FindById(int clave)
        {
            Compra buscada = null;
            foreach (Compra item in Compras)
            {
                if (item.Id.Equals(clave)) buscada = item;
            }
            return buscada;
        }

        public bool Remove(int clave)
        {
            bool ok = false;

            Compra aBorrar = this.FindById(clave);
            if (aBorrar != null)
            {
                Compras.Remove(aBorrar);
            }return ok;
        }

        public bool Update(Compra obj)
        {
            bool ok = false;
            Compra aModificar = this.FindById(obj.Id);
            if (obj.Validar() && aModificar != null)
            {
                //aModificar.Cantidad = obj.Cantidad;
                aModificar.Fecha = obj.Fecha;
                //aModificar.Precio = obj.Precio;
                //aModificar.Plantas = obj.Plantas;
                ok = true;
            }
            return ok;
        }
    }
}
