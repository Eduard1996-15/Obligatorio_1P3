using Datos.RepositoriosAdo;
using Dominio.EntidadesNegocios;
using Dominio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Repositorios.RepositoriosAdo
{
    public class RepositorioClienteAdo : IRepositorioUsuario
    {

		private Conexion manejadorConexion = new Conexion();
		private SqlConnection cn;    
		public bool Add(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> FindAll()
        {
			try
			{
				cn = manejadorConexion.CrearConexion();

				SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario", cn);
				manejadorConexion.AbrirConexion(cn);
				SqlDataReader dr = cmd.ExecuteReader();
				if (dr.HasRows)
				{
					List<Usuario> lista = new List<Usuario>();
					while (dr.Read())
					{
						lista.Add(new Usuario
						{
							Id = (int)dr["Id"],
							Email = dr["EMAIL"].ToString(),
							Password = dr["CLAVE"].ToString()
						});
					}
					return lista;
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

		public Usuario FindById(int clave)
        {
			Usuario u = null;
			try
			{
				cn = manejadorConexion.CrearConexion();
				SqlCommand cmd = new SqlCommand($"SELECT * FROM Usuario where ID =@id ", cn);//comando
				SqlParameter param = new SqlParameter();//creo parametro
				param.ParameterName = "@id";//le doy nombre
				param.Value = clave;//valor
				cmd.Parameters.Add(param);//agrego al comando
				manejadorConexion.AbrirConexion(cn);//abro conexion
				SqlDataReader dr = cmd.ExecuteReader();//ejecuto y leo 
				if (dr.HasRows)//mietras lea
				{
					while (dr.Read())
					{
						Usuario u1 = new Usuario
						{
							Id = (int)dr["Id"],
							Email = dr["EMAIL"].ToString(),
							Password = dr["CLAVE"].ToString()
						};
						u = u1;
					}
					return u;
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

        public Usuario Loguin(string email, string password)
        {
			try
			{
				Usuario u = null;
				cn = manejadorConexion.CrearConexion();
				SqlCommand cmd = new SqlCommand($"SELECT * FROM Usuario where EMAIL = @email and CLAVE = @password ", cn);
				SqlParameter p1 = new SqlParameter();
				SqlParameter p2 = new SqlParameter();
				p1.ParameterName = "@email";
				p1.Value = email;
				p2.ParameterName = "@password";
				p2.Value = password;
				cmd.Parameters.Add(p1);
				cmd.Parameters.Add(p2);

				manejadorConexion.AbrirConexion(cn);
				SqlDataReader dr = cmd.ExecuteReader();
				if (dr.HasRows)
				{
					
					while (dr.Read())
					{
						Usuario u1 = new Usuario
						{
							Id = (int)dr["ID"],
							Email = dr["EMAIL"].ToString(),
							Password = dr["CLAVE"].ToString()
						};
						u = u1;
					}
					
				}
				return u;
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
            throw new NotImplementedException();
        }

        public bool Update(Usuario obj)
        {
            throw new NotImplementedException();
        }
    }
}
