using System;
using System.Collections.Generic;
using System.Text;
using Microsoft;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Datos.RepositoriosAdo
{
    public class Conexion
    {
		private string cadenaConexion =null;
		public Conexion()
        {
			cadenaConexion = ObtenerStringConexion();
        }
		private string ObtenerStringConexion()
		{
			try
			{
				IConfigurationBuilder builder = new ConfigurationBuilder();
				builder.AddJsonFile("appsettings.json");

				IConfiguration config = builder.Build();
				string cadena = config.GetConnectionString("MiConexion");
				return cadena;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		public SqlConnection CrearConexion()
		{
			return new SqlConnection(cadenaConexion);
		}
		public bool AbrirConexion(SqlConnection cn)
		{

			try
			{
				if (cn.State != ConnectionState.Open)
				{
					cn.Open();
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				return false;
			}
			finally
			{
				System.Diagnostics.Debug.WriteLine("Entré al finally de abrir conexión");
			}
		}
		public bool CerrarConexion(SqlConnection cn)
		{
			try
			{
				if (cn.State != ConnectionState.Closed)
				{
					cn.Close();

					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				return false;
			}
			finally
			{
				cn.Dispose();
				System.Diagnostics.Debug.WriteLine("Entré al finally de abrir conexión");
			}
		}
	}
}
