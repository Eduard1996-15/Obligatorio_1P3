using Datos.RepositoriosAdo;
using Dominio.EntidadesNegocios;
using Dominio.InterfacesRepositorios;
using Negocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace Repositorios.RepositoriosAdo
{
    public class RepositorioPlantaAdo : IRepositorioPlanta
    {
        private Conexion manejadorConexion = new Conexion();
        private SqlConnection cn;
        public bool Add(Planta obj)
        {
            
            if (obj == null || !obj.ValidarTope())
            {                                                  
                return false;                                                                         
            }
            //NOMBRE_VULGAR,NOMBRE_CIENTIFICO,DESCRIPCION, ALTURA_MAX, AMBIENTE, CONTINENTEORIGEN ,FOTO ,ID_TIPO
            
            SqlConnection cn = (SqlConnection)manejadorConexion.CrearConexion();
            SqlTransaction trn = null;
        try
        { 
            SqlCommand cmdAgregarPlanta = new SqlCommand(@"insert PLANTA values(@nombrev,@nombrec,@descripcion,@altura,@ambiente,@continente,@foto,@idTipo); SELECT CAST(Scope_Identity() as int);", cn);
                
            //planta
              cmdAgregarPlanta.Parameters.Add(new SqlParameter("@nombrev", obj.NombreVulgar));
            cmdAgregarPlanta.Parameters.Add(new SqlParameter("@nombrec", obj.NombreCientifico));
            cmdAgregarPlanta.Parameters.Add(new SqlParameter("@descripcion", obj.Descripcion));
            cmdAgregarPlanta.Parameters.Add(new SqlParameter("@altura", obj.Altura)); 
            cmdAgregarPlanta.Parameters.Add(new SqlParameter("@ambiente", obj.Ambiente));
            cmdAgregarPlanta.Parameters.Add(new SqlParameter("@continente", obj.ContinenteOrigen));
            cmdAgregarPlanta.Parameters.Add(new SqlParameter("@foto", obj.Foto));
            cmdAgregarPlanta.Parameters.Add(new SqlParameter("@idTipo", obj.Tipo.Id));
                SqlCommand cmdAgregarFicha = new SqlCommand();
                cmdAgregarFicha.Connection = cn;
                cmdAgregarFicha.CommandText= (@"insert FICHA values(@idp,@frecuenciaRiego,@tipoIluminacion,@temperatura);");

                manejadorConexion.AbrirConexion(cn);//abrir conexion
                trn = cn.BeginTransaction();//comienzo de transacciones
                cmdAgregarFicha.Transaction = trn;//transaction ficha
                cmdAgregarPlanta.Transaction = trn;//transaccion planta
                //cmdAgregarPlanta.ExecuteNonQuery();
                int idGenerado = (int)cmdAgregarPlanta.ExecuteScalar();//id generado de planta
                //ficha
                cmdAgregarFicha.Parameters.Add(new SqlParameter("@idp", idGenerado));
                cmdAgregarFicha.Parameters.Add(new SqlParameter("@frecuenciaRiego", obj.FichaPlanta.FrecuenciaRiego));
                cmdAgregarFicha.Parameters.Add(new SqlParameter("@tipoIluminacion", obj.FichaPlanta.TipoIluminacion));
                cmdAgregarFicha.Parameters.Add(new SqlParameter("@temperatura", obj.FichaPlanta.Temperatura));
                
                cmdAgregarFicha.ExecuteNonQuery();//ejecutarficha
                cmdAgregarFicha.Parameters.Clear();//cerrar parametros
                trn.Commit();
                return true;
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                trn.Rollback();
                return false;
            }
            finally
            {
                manejadorConexion.CerrarConexion(cn);
            }
        }

        public List<Planta> FindAll()
        {
             cn = manejadorConexion.CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT PLANTA.*, FICHA.* FROM PLANTA, FICHA 
			WHERE PLANTA.ID_PLANTA = FICHA.ID_PLANTA", cn);
            manejadorConexion.AbrirConexion(cn);
            SqlDataReader dr = cmd.ExecuteReader();
            try
            {
                if (dr.HasRows)
                {
                    List<Planta> lista = new List<Planta>();
                    while (dr.Read())
                    {
                        Planta p = new Planta();
                        TipoPlanta tp = new TipoPlanta();
                        Ficha f = new Ficha();
                        p.Id = (int)dr["ID_PLANTA"];
                        p.NombreVulgar = dr["NOMBRE_VULGAR"].ToString();
                        p.NombreCientifico = dr["NOMBRE_CIENTIFICO"].ToString();
                        p.Descripcion = dr["DESCRIPCION"].ToString();
                        p.Altura = (int)dr["ALTURA_MAX"];
                        p.Ambiente = dr["AMBIENTE"].ToString();
                        p.ContinenteOrigen = dr["CONTINENTEORIGEN"].ToString();
                        p.Foto = dr["FOTO"].ToString();
                        tp.Id = (int)dr["ID_TIPO"];
                        f.FrecuenciaRiego = dr["FRECUENCIA_RIEGO"].ToString();
                        f.TipoIluminacion = dr["TIPO_ILUMINACION"].ToString();
                        f.Temperatura = (int)dr["TEMPERATURA"];
                        p.FichaPlanta = f;
                        p.Tipo = tp;
                        lista.Add(p);
                       
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

        public Planta FindById(int clave)
        {
            cn = manejadorConexion.CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT PLANTA.*, FICHA.* FROM PLANTA, FICHA 
			WHERE PLANTA.ID_PLANTA = FICHA.ID_PLANTA AND PLANTA.ID_PLANTA = @P", cn);
            cmd.Parameters.Add(new SqlParameter("@P", clave));
            manejadorConexion.AbrirConexion(cn);
          
            try
            { 
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    Planta p = new Planta();
                    while (dr.Read())
                    {
                        TipoPlanta tp = new TipoPlanta();
                        Ficha f = new Ficha();
                        p.Id = (int)dr["ID_PLANTA"];
                        p.NombreVulgar = dr["NOMBRE_VULGAR"].ToString();
                        p.NombreCientifico = dr["NOMBRE_CIENTIFICO"].ToString();
                        p.Descripcion = dr["DESCRIPCION"].ToString();
                        p.Altura = (int)dr["ALTURA_MAX"];
                        p.Ambiente = dr["AMBIENTE"].ToString();
                        p.ContinenteOrigen = dr["CONTINENTEORIGEN"].ToString();
                        p.Foto = dr["FOTO"].ToString();
                        tp.Id = (int)dr["ID_TIPO"];
                        f.FrecuenciaRiego = dr["FRECUENCIA_RIEGO"].ToString();
                        f.TipoIluminacion = dr["TIPO_ILUMINACION"].ToString();
                        f.Temperatura = (int)dr["TEMPERATURA"];
                        p.FichaPlanta = f;
                        p.Tipo = tp;
                    }
                    return p;
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

        public IEnumerable<Planta> ListarPorAlturaMayor(int altura)
        {
            cn = manejadorConexion.CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT PLANTA.*, FICHA.* FROM PLANTA, FICHA WHERE
            PLANTA.ID_PLANTA = FICHA.ID_PLANTA AND ALTURA_MAX >= @altu ", cn);
            cmd.Parameters.Add(new SqlParameter("@altu", altura));
            manejadorConexion.AbrirConexion(cn);

            try
            {

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    List<Planta> lista = new List<Planta>();
                    while (dr.Read())
                    {
                        Planta p = new Planta();
                        TipoPlanta tp = new TipoPlanta();
                        Ficha f = new Ficha();
                        p.Id = (int)dr["ID_PLANTA"];
                        p.NombreVulgar = dr["NOMBRE_VULGAR"].ToString();
                        p.NombreCientifico = dr["NOMBRE_CIENTIFICO"].ToString();
                        p.Descripcion = dr["DESCRIPCION"].ToString();
                        p.Altura = (int)dr["ALTURA_MAX"];
                        p.Ambiente = dr["AMBIENTE"].ToString();
                        p.ContinenteOrigen = dr["CONTINENTEORIGEN"].ToString();
                        p.Foto = dr["FOTO"].ToString();
                        tp.Id = (int)dr["ID_TIPO"];
                        f.FrecuenciaRiego = dr["FRECUENCIA_RIEGO"].ToString();
                        f.TipoIluminacion = dr["TIPO_ILUMINACION"].ToString();
                        f.Temperatura = (int)dr["TEMPERATURA"];
                        p.FichaPlanta = f;
                        p.Tipo = tp;
                        lista.Add(p);

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

        public IEnumerable<Planta> ListarPorAlturaMenor(int altura)
        {
            cn = manejadorConexion.CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT PLANTA.*, FICHA.* FROM PLANTA, FICHA WHERE
            PLANTA.ID_PLANTA = FICHA.ID_PLANTA AND  ALTURA_MAX =< @alt ", cn);
            cmd.Parameters.Add(new SqlParameter("@alt", altura));
            manejadorConexion.AbrirConexion(cn);
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    List<Planta> lista = new List<Planta>();
                    while (dr.Read())
                    {
                        Planta p = new Planta();
                        TipoPlanta tp = new TipoPlanta();
                        Ficha f = new Ficha();
                        p.Id = (int)dr["ID_PLANTA"];
                        p.NombreVulgar = dr["NOMBRE_VULGAR"].ToString();
                        p.NombreCientifico = dr["NOMBRE_CIENTIFICO"].ToString();
                        p.Descripcion = dr["DESCRIPCION"].ToString();
                        p.Altura = (int)dr["ALTURA_MAX"];
                        p.Ambiente = dr["AMBIENTE"].ToString();
                        p.ContinenteOrigen = dr["CONTINENTEORIGEN"].ToString();
                        p.Foto = dr["FOTO"].ToString();
                        tp.Id = (int)dr["ID_TIPO"];
                        f.FrecuenciaRiego = dr["FRECUENCIA_RIEGO"].ToString();
                        f.TipoIluminacion = dr["TIPO_ILUMINACION"].ToString();
                        f.Temperatura = (int)dr["TEMPERATURA"];
                        p.FichaPlanta = f;
                        p.Tipo = tp;
                        lista.Add(p);
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

        public IEnumerable<Planta> ListarPorAmbiente(string tipoAmbiente)
        {
            cn = manejadorConexion.CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT PLANTA.*, FICHA.* FROM PLANTA, FICHA WHERE
            PLANTA.ID_PLANTA = FICHA.ID_PLANTA AND  AMBIENTE = @amb ", cn);
            cmd.Parameters.Add(new SqlParameter("@amb", tipoAmbiente));

            manejadorConexion.AbrirConexion(cn);

            try
            {

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    List<Planta> lista = new List<Planta>();
                    while (dr.Read())
                    {
                        Planta p = new Planta();
                        TipoPlanta tp = new TipoPlanta();
                        Ficha f = new Ficha();
                        p.Id = (int)dr["ID_PLANTA"];
                        p.NombreVulgar = dr["NOMBRE_VULGAR"].ToString();
                        p.NombreCientifico = dr["NOMBRE_CIENTIFICO"].ToString();
                        p.Descripcion = dr["DESCRIPCION"].ToString();
                        p.Altura = (int)dr["ALTURA_MAX"];
                        p.Ambiente = dr["AMBIENTE"].ToString();
                        p.ContinenteOrigen = dr["CONTINENTEORIGEN"].ToString();
                        p.Foto = dr["FOTO"].ToString();
                        tp.Id = (int)dr["ID_TIPO"];
                        f.FrecuenciaRiego = dr["FRECUENCIA_RIEGO"].ToString();
                        f.TipoIluminacion = dr["TIPO_ILUMINACION"].ToString();
                        f.Temperatura = (int)dr["TEMPERATURA"];
                        p.FichaPlanta = f;
                        p.Tipo = tp;
                        lista.Add(p);

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

        public IEnumerable<Planta> ListarPorNombre(string nombre)
        {
            cn = manejadorConexion.CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT Distinct PLANTA.*, FICHA.* FROM PLANTA, FICHA WHERE
            PLANTA.ID_PLANTA = FICHA.ID_PLANTA AND NOMBRE_VULGAR LIKE @nom or NOMBRE_CIENTIFICO LIKE @nom ", cn);
            cmd.Parameters.Add(new SqlParameter("@nom", "%" + nombre + "%"));
            manejadorConexion.AbrirConexion(cn);
           
            try
            {
                
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    List<Planta> lista = new List<Planta>();
                    while (dr.Read())
                    {
                        Planta p = new Planta();
                        TipoPlanta tp = new TipoPlanta();
                        Ficha f = new Ficha();
                        p.Id = (int)dr["ID_PLANTA"];
                        p.NombreVulgar = dr["NOMBRE_VULGAR"].ToString();
                        p.NombreCientifico = dr["NOMBRE_CIENTIFICO"].ToString();
                        p.Descripcion = dr["DESCRIPCION"].ToString();
                        p.Altura = (int)dr["ALTURA_MAX"];
                        p.Ambiente = dr["AMBIENTE"].ToString();
                        p.ContinenteOrigen = dr["CONTINENTEORIGEN"].ToString();
                        p.Foto = dr["FOTO"].ToString();
                        tp.Id = (int)dr["ID_TIPO"];
                        f.FrecuenciaRiego = dr["FRECUENCIA_RIEGO"].ToString();
                        f.TipoIluminacion = dr["TIPO_ILUMINACION"].ToString();
                        f.Temperatura = (int)dr["TEMPERATURA"];
                        p.FichaPlanta = f;
                        p.Tipo = tp;
                        if (!lista.Contains(p))
                           lista.Add(p);

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

        public IEnumerable<Planta> ListarPorTipo(string nombreTipo)
        {
            cn = manejadorConexion.CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT PLANTA.*, FICHA.* FROM PLANTA, FICHA, TIPO_PLANTA WHERE
            PLANTA.ID_TIPO = TIPO_PLANTA.ID_TIPO and FICHA.ID_PLANTA = PLANTA.ID_PLANTA AND TIPO_PLANTA.NOMBRE = @nom ", cn);           
            cmd.Parameters.Add(new SqlParameter("@nom", nombreTipo));
            manejadorConexion.AbrirConexion(cn);
            SqlDataReader dr = cmd.ExecuteReader();

            try
            {
                if (dr.HasRows)
                {
                    List<Planta> lista = new List<Planta>();
                    while (dr.Read())
                    {
                        Planta p = new Planta();
                        TipoPlanta tp = new TipoPlanta();
                        Ficha f = new Ficha();
                        p.Id = (int)dr["ID_PLANTA"];
                        p.NombreVulgar = dr["NOMBRE_VULGAR"].ToString();
                        p.NombreCientifico = dr["NOMBRE_CIENTIFICO"].ToString();
                        p.Descripcion = dr["DESCRIPCION"].ToString();
                        p.Altura = (int)dr["ALTURA_MAX"];
                        p.Ambiente = dr["AMBIENTE"].ToString();
                        p.ContinenteOrigen = dr["CONTINENTEORIGEN"].ToString();
                        p.Foto = dr["FOTO"].ToString();
                        tp.Id = (int)dr["ID_TIPO"];
                        f.FrecuenciaRiego = dr["FRECUENCIA_RIEGO"].ToString();
                        f.TipoIluminacion = dr["TIPO_ILUMINACION"].ToString();
                        f.Temperatura = (int)dr["TEMPERATURA"];
                        p.FichaPlanta = f;
                        p.Tipo = tp;
                        lista.Add(p);

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

        public bool Remove(int clave)
        {
            throw new NotImplementedException();
        }

        public bool Update(Planta obj)
        {
            throw new NotImplementedException();
        }
    }
}
