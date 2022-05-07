using System;
using System.Collections.Generic;
using System.Text;
using Dominio.EntidadesNegocios;
using Dominio.InterfacesRepositorios;
using System.Linq;

namespace Repositorios.RepositoriosMemoria
{
    public class RepositorioCliente : IRepositorioUsuario
    {
        public static List<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public bool Add(Usuario obj)
        {
            bool ok = false;
            if (obj.Validar() && FindById(obj.Id) == null)
            {
                Usuarios.Add(obj);
                ok = true;
            }
            return ok;
        }

        public List<Usuario> FindAll()
        {
            return Usuarios;
        }

        public Usuario FindById(int clave)
        {
            Usuario buscado = null;
            //buscado = RepositorioClientesMemoria.Clientes.Where(c => c.Id == id).SingleOrDefault();
            foreach (Usuario usu in Usuarios)
            {
                if (usu.Id == clave) buscado = usu;
            }
            return buscado;
        }

        public Usuario Loguin(string e, string p)
        {
            Usuario buscado = null;
            //buscado = RepositorioClientesMemoria.Clientes.Where(c => c.Id == id).SingleOrDefault();
            foreach (Usuario usu in Usuarios)
            {
                if (usu.Email == e && usu.Password == p) buscado = usu;
            }
            return buscado;
        }

        public bool Remove(int clave)
        {
            bool ok = false;
            Usuario aBorrar = this.FindById(clave);
            if (aBorrar != null)
            {
               Usuarios.Remove(aBorrar);
                ok = true;
            }
            return ok;
        }

        public bool Update(Usuario obj)
        {
            bool ok = false;
            Usuario aModificar = this.FindById(obj.Id);
            if (obj.Validar() && aModificar != null)
            {
                aModificar.Email = obj.Email;
                aModificar.Password = obj.Password;
                ok = true;
            }
            return ok;
        }
    }
}
