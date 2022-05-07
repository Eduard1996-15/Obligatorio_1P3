using Datos.RepositoriosAdo;
using Dominio.EntidadesNegocios;
using Dominio.InterfacesRepositorios;
using Negocio.EntidadesNegocios;
using Negocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Datos.RepositoriosAdo
{
    public class RepositorioParametroADO : IRepositorioParametro
    {
        private Conexion manejadorConexion = new Conexion();
        private SqlConnection cn;
        public bool Add(Parametro obj)
        {
            throw new NotImplementedException();
        }

        public int BuscarRetornarValor(string nombre)
        {
            cn = manejadorConexion.CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT  PARAMETRO.VALOR  FROM PARAMETRO WHERE  PARAMETRO.NOMBRE = @N", cn);
            cmd.Parameters.Add(new SqlParameter("@N", nombre));
            manejadorConexion.AbrirConexion(cn);

            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    int valor = 0;
                    while (dr.Read())
                    {
                       
                         valor= int.Parse(dr["valor"].ToString());
                    }
                    return valor;
                }
                return 0;
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                return 0;
            }
            finally
            {
                manejadorConexion.CerrarConexion(cn);
            }
        }

        public List<Parametro> FindAll()
        {
            throw new NotImplementedException();
        }

        public Parametro FindById(string clave)
        {
            throw new NotImplementedException();
        }

        public bool Update(string nom, string val )
        {

            Conexion manejadorConexion = new Conexion();
            SqlConnection cn = (SqlConnection)manejadorConexion.CrearConexion();
            SqlCommand cmdModificarTIpoPlanta = new SqlCommand(@"update  PARAMETRO set VALOR = @val where nombre = @nom", cn);
            cmdModificarTIpoPlanta.Parameters.Add(new SqlParameter("@val", val));
            cmdModificarTIpoPlanta.Parameters.Add(new SqlParameter("@nom", nom));
            try
            {
                manejadorConexion.AbrirConexion(cn);
                cmdModificarTIpoPlanta.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                manejadorConexion.CerrarConexion(cn);
            }
        }
    }
}
