using Datos.RepositoriosAdo;
using Dominio.EntidadesNegocios;
using Dominio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repositorios.RepositoriosAdo
{
    public class RepositorioTipoPlantaAdo : IRepositorioTipoPlanta
    {
		private Conexion manejadorConexion = new Conexion();
		private SqlConnection cn;
		public bool Add(TipoPlanta obj)
        {
			if (obj == null || !obj.Validar())
			{
				return false;
			}
			Conexion manejadorConexion = new Conexion();
			SqlConnection cn = (SqlConnection)manejadorConexion.CrearConexion();
            SqlCommand cmdAgrgarTIpoPlanta = new SqlCommand(@"INSERT INTO TIPO_PLANTA VALUES(@Nombre, @Descripcion);", cn);
			cmdAgrgarTIpoPlanta.Parameters.Add(new SqlParameter("@Nombre", obj.Nombre));
			cmdAgrgarTIpoPlanta.Parameters.Add(new SqlParameter("@Descripcion", obj.Descripcion));
             
			
			try
			{
				manejadorConexion.AbrirConexion(cn);
				cmdAgrgarTIpoPlanta.ExecuteNonQuery();
				return true;
			}
			catch (Exception ex)
			{
				//trn.Rollback();
				return false;
			}
			finally
			{
				manejadorConexion.CerrarConexion(cn);
			}
		}

        public TipoPlanta BuscarPorNombre(string nombre)
        {
			try
			{
				cn = manejadorConexion.CrearConexion();

				SqlCommand cmd = new SqlCommand("SELECT * FROM TIPO_PLANTA WHERE NOMBRE = @NOMBRE", cn);
				cmd.Parameters.Add(new SqlParameter("@NOMBRE", nombre));
				manejadorConexion.AbrirConexion(cn);
				SqlDataReader dr = cmd.ExecuteReader();//METODO DE LECTURA
				if (dr.HasRows)
				{
					TipoPlanta TP = null;
					while (dr.Read())
					{
						TP = new TipoPlanta
						{
							Id = (int)dr["ID_TIPO"],
							Nombre = dr["NOMBRE"].ToString(),
							Descripcion = dr["DESCRIPCION"].ToString()
						};
					}
					return TP;
				}
				return null;
			}
			catch (Exception ex)
			{
				return null;
			}
			finally
			{
				manejadorConexion.CerrarConexion(cn);
			}
		}

        public List<TipoPlanta> FindAll()
        {
			try
			{
				cn = manejadorConexion.CrearConexion();

				SqlCommand cmd = new SqlCommand("SELECT * FROM TIPO_PLANTA", cn);
				manejadorConexion.AbrirConexion(cn);
				SqlDataReader dr = cmd.ExecuteReader();
				if (dr.HasRows)
				{
					List<TipoPlanta> lista = new List<TipoPlanta>();
					while (dr.Read())
					{
						lista.Add(new TipoPlanta
						{
							Id = (int)dr["ID_TIPO"],
							Nombre = dr["NOMBRE"].ToString(),
							Descripcion = dr["DESCRIPCION"].ToString()
						});
					}
					return lista;
				}
				return null;
			}
			catch (Exception ex)
			{
				//throw new Exception(ex.Message);
				return null;
			}
			finally
			{
				manejadorConexion.CerrarConexion(cn);
			}
		}

        public TipoPlanta FindById(int clave)
        {
			try
			{
				cn = manejadorConexion.CrearConexion();

				SqlCommand cmd = new SqlCommand("SELECT * FROM TIPO_PLANTA WHERE ID_TIPO = @ID", cn);
				cmd.Parameters.Add(new SqlParameter("@ID", clave));
				manejadorConexion.AbrirConexion(cn);
				SqlDataReader dr = cmd.ExecuteReader();//METODO DE LECTURA
				if (dr.HasRows)
				{
					TipoPlanta TP = null;
					while (dr.Read())
					{
						 TP = new TipoPlanta{
							Id = (int)dr["ID_TIPO"],
							Nombre = dr["NOMBRE"].ToString(),
							Descripcion = dr["DESCRIPCION"].ToString()
						};
					}
					return TP;
				}
				return null;
			}
			catch (Exception ex)
			{
				return null;
			}
			finally
			{
				manejadorConexion.CerrarConexion(cn);
			}
		}


        public bool Remove(int clave)
        {
			Conexion manejadorConexion = new Conexion();
			SqlConnection cn = (SqlConnection)manejadorConexion.CrearConexion();
			SqlCommand cmdEliminarTIpoPlanta = new SqlCommand(@"DELETE TIPO_PLANTA WHERE ID_TIPO = @ID", cn);
			cmdEliminarTIpoPlanta.Parameters.Add(new SqlParameter("@ID", clave));
			try
			{
				manejadorConexion.AbrirConexion(cn);
				cmdEliminarTIpoPlanta.ExecuteNonQuery();
				
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

        public bool Update(TipoPlanta obj)
        {
			Conexion manejadorConexion = new Conexion();
			SqlConnection cn = (SqlConnection)manejadorConexion.CrearConexion();
			SqlCommand cmdModificarTIpoPlanta = new SqlCommand(@"UPDATE TIPO_PLANTA SET DESCRIPCION = @DESCRIPCION  WHERE ID_TIPO = @CLAVE", cn);
			cmdModificarTIpoPlanta.Parameters.Add(new SqlParameter("@CLAVE", obj.Id));
			cmdModificarTIpoPlanta.Parameters.Add(new SqlParameter("@DESCRIPCION", obj.Descripcion));
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
