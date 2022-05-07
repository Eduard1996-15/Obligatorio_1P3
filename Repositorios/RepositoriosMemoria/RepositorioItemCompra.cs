using System;
using System.Collections.Generic;
using System.Text;
using Dominio.EntidadesNegocios;
using Dominio.InterfacesRepositorios;
using Negocio.InterfacesRepositorios;

namespace Datos.RepositoriosMemoria
{
    class RepositorioItemCompra : IRepositorioItemCompra
    {
        public static List<ItemCompra> ItemsCompras { get; set; } = new List<ItemCompra>();
        public bool Add(ItemCompra obj)
        {
            bool ok = false;
            if ( FindById(obj.Id) == null)
            {
                ItemsCompras.Add(obj);
                ok = true;
            }
            return ok;
        }

        public List<ItemCompra> FindAll()
        {
            return ItemsCompras;
        }

        public ItemCompra FindById(int clave)
        {
            ItemCompra buscada = null;
            foreach (ItemCompra item in ItemsCompras)
            {
                if (item.Id.Equals(clave)) buscada = item;
            }
            return buscada; ;
        }

        public bool Remove(int clave)
        {
            bool ok = false;

            ItemCompra aBorrar = this.FindById(clave);
            if (aBorrar != null)
            {
                ItemsCompras.Remove(aBorrar);
            }
            return ok;
        }

        public bool Update(ItemCompra obj)
        {
            bool ok = false;
            ItemCompra aModificar = this.FindById(obj.Id);
            if (aModificar != null)
            {
                aModificar.Cantidad = obj.Cantidad;
                aModificar.PrecioUnitario = obj.PrecioUnitario;
                aModificar.Planta = obj.Planta;
                
                ok = true;
            }
            return ok;
        }
    }
}
