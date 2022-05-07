using Dominio.EntidadesNegocios;
using Dominio.InterfacesRepositorios;
using Negocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;


namespace Repositorios.RepositoriosMemoria
{
    public class RepositorioPlanta : IRepositorioPlanta
    {
        public static List<Planta> Plantas { get; set; } = new List<Planta>();
        public static List<TipoPlanta> Tipos { get; set; } = new List<TipoPlanta>();

        public bool Add(Planta obj)
        {
            bool ok = false;
            if (obj.Validar() && FindById(obj.Id) == null)
            {
                Plantas.Add(obj);
                ok = true;
            }
            return ok;
        }

        public List<Planta> FindAll()
        {
            return Plantas;
        }

        public Planta FindById(int clave)
        {
            Planta buscado = null;
            //buscado = RepositorioClientesMemoria.Clientes.Where(c => c.Id == id).SingleOrDefault();
            foreach (Planta pl in Plantas)
            {
                if (pl.Id == clave) buscado = pl;
            }
            return buscado;
        }

        public IEnumerable<Planta> ListarPorAlturaMayor(int altura)
        {
            List<Planta> listadoAlturaMayor = new List<Planta>();
            int alturaMayor = -9999;
            foreach (Planta pl in Plantas)
            {
                if (pl.Altura > alturaMayor)
                    listadoAlturaMayor.Add(pl);
            }
            return listadoAlturaMayor;
          
        }

        public IEnumerable<Planta> ListarPorAlturaMenor(int altura)
        {
            List<Planta> listadoAlturaMenor = new List<Planta>();
            int alturaMenor = 9999;
            foreach (Planta pl in RepositorioPlanta.Plantas)
            {
                if (pl.Altura < alturaMenor)
                    listadoAlturaMenor.Add(pl);
            }
            return listadoAlturaMenor;

        }
    
        public IEnumerable<Planta> ListarPorAmbiente(string tipoAmbiente)
        {
            List<Planta> listadoPorAmbiente = new List<Planta>();
            foreach (Planta pl in RepositorioPlanta.Plantas)
            {
                //TODO tipos de datos diferentes enum - string
                if (pl.Ambiente == tipoAmbiente)
                listadoPorAmbiente.Add(pl);
            }
            return listadoPorAmbiente;

        }

        public IEnumerable<Planta> ListarPorNombre(string nombre)
        {
            Convert.ToChar(nombre);
            List<Planta> listadoPorNombre    = new List<Planta>();
            foreach (Planta pl in RepositorioPlanta.Plantas)
            {
                if ((pl.NombreCientifico.IndexOf(nombre) > -1) || (pl.NombreVulgar.IndexOf(nombre) > -1))
                    listadoPorNombre.Add(pl);
            }
            return listadoPorNombre;
        }

        public IEnumerable<Planta> ListarPorTipo(string nombreTipo)
        {
            List<Planta> listadoPorTipo = new List<Planta>();
            foreach (Planta pl in RepositorioPlanta.Plantas)
            {
                if (pl.Tipo.Nombre == nombreTipo)
                    listadoPorTipo.Add(pl);
            }
            return listadoPorTipo;
        }

        public bool Remove(int clave)
        {
            bool ok = false;
            Planta aBorrar = this.FindById(clave);
            if (aBorrar != null)
            {
                Plantas.Remove(aBorrar);
                ok = true;
            }
            return ok;
        }

        public bool Update(Planta obj)
        {
            bool ok = false;
            Planta aModificar = this.FindById(obj.Id);
            if (obj.Validar() && aModificar != null)
            {
                aModificar.Altura = obj.Altura;
                aModificar.Ambiente = obj.Ambiente;
                aModificar.ContinenteOrigen = obj.ContinenteOrigen;
                aModificar.FichaPlanta.FrecuenciaRiego = obj.FichaPlanta.FrecuenciaRiego;//la ficha van tds sus datos modificables
                aModificar.FichaPlanta.Temperatura = obj.FichaPlanta.Temperatura;
                aModificar.FichaPlanta.TipoIluminacion = obj.FichaPlanta.TipoIluminacion;
                aModificar.Foto = obj.Foto;
                aModificar.NombreCientifico = obj.NombreCientifico;
                aModificar.NombreVulgar = obj.NombreVulgar;
                
                ok = true;
            }
            return ok;
        }
    }
}
