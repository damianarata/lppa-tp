using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BE;
namespace DAL
{
    public class Usuario_DAL
    {
        Acceso_DAL ac = new Acceso_DAL();
        public Usuario_BE loguear(string usuario, string contraseña)
        {

            string contraseña_encriptada = Calcular_HashMD5(contraseña);

            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter();
            parametros[0].ParameterName = "@usu";
            parametros[0].DbType = DbType.String;
            parametros[0].Value = usuario;

            parametros[1] = new SqlParameter();
            parametros[1].ParameterName = "@pass";
            parametros[1].DbType = DbType.String;
            parametros[1].Value = contraseña_encriptada;

            DataTable Tabla = ac.Leer("verificar_usuario", parametros);

            Usuario_BE usuarioBE = new Usuario_BE(); 

            foreach (DataRow reg in Tabla.Rows)
            {

                usuarioBE.Usuario = reg["usuario"].ToString();
                usuarioBE.Contraseña = reg["contraseña"].ToString();
                usuarioBE.IdUsuario = Convert.ToInt32(reg["id"].ToString());
                usuarioBE.Nombre = reg["nombre"].ToString();
                TipoUsuario_BE tipoUsuario = new TipoUsuario_BE(); 
                tipoUsuario.id = Convert.ToInt32(reg["id_tipo_usuario"].ToString());
                tipoUsuario.tipo_usuario = reg["tipo_usuario"].ToString();
                usuarioBE.TipoUsuario = tipoUsuario; 

                //user.Sesion.sesionIniciada = bool.Parse(reg["sesionIniciada"].ToString());

            }
            return usuarioBE;
        }

        public void LLenar_Bitacora(int id_usuario, string detalle)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter();
            parametros[0].ParameterName = "@id_usu";
            parametros[0].DbType = DbType.String;
            parametros[0].Value = id_usuario;

            parametros[1] = new SqlParameter();
            parametros[1].ParameterName = "@detalle";
            parametros[1].DbType = DbType.String;
            parametros[1].Value = detalle;

            DataTable Tabla = ac.Leer("llenar_bitacora", parametros);

        }

        public List<Accion_BE> Buscar_Acciones(int id_tipo_usuario)
        {
            List<Accion_BE> acciones = new List<Accion_BE>();

            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter();
            parametros[0].ParameterName = "@id_tipo_usu";
            parametros[0].DbType = DbType.String;
            parametros[0].Value = id_tipo_usuario;
          

            DataTable Tabla = ac.Leer("listar_acciones", parametros);
            foreach (DataRow reg in Tabla.Rows)
            {
                Accion_BE accion = new Accion_BE();
                accion.id = Convert.ToInt32(reg["id"].ToString());
                accion.detalle = reg["detalle"].ToString();

                acciones.Add(accion);
            }
            return acciones;
        }

        public List<DetalleBitacora_BE>Listar_Bitacora()
        {
            List<DetalleBitacora_BE> bitacora = new List<DetalleBitacora_BE>();
                       
            DataTable Tabla = ac.Leer("listar_bitacora", null);
            foreach (DataRow reg in Tabla.Rows)
            {
                DetalleBitacora_BE detalle = new DetalleBitacora_BE();
                detalle.Id = Convert.ToInt32(reg["id"].ToString());
                detalle.Id_Usuario = Convert.ToInt32(reg["id_usuario"].ToString());
                detalle.Detalle = reg["detalle"].ToString();
                detalle.Usuario = reg["usuario"].ToString();
                detalle.Fecha =Convert.ToDateTime(reg["Fecha"].ToString());
                bitacora.Add(detalle);
            }
            return bitacora;
        }


        private string Calcular_HashMD5(string cadena)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] cadenaBytes = System.Text.Encoding.ASCII.GetBytes(cadena);
                byte[] hashBytes = md5.ComputeHash(cadenaBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public bool RestoreDB(string dire)
        {
            string query = string.Empty;
            string Query2 = string.Format("ALTER DATABASE " + "[VeterinariaLPPA] " + "SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            query = "USE MASTER RESTORE DATABASE" + " VeterinariaLPPA " + "FROM " + dire + " WITH REPLACE";
            string Query3 = string.Format("ALTER DATABASE " + "[VeterinariaLPPA]" + " SET MULTI_USER");
            bool status1 = ac.ExecuteQuery(Query2);
            bool status2 = ac.ExecuteQuery(query);
            bool status3 = ac.ExecuteQuery(Query3);
            if (status1 == true && status2 == true && status3 == true)
            {
                return true;
            }
            return false;
        }
        public bool TakeDB(string filename, string dire, int partes)
        {
            string query = string.Empty;
            int cant = Convert.ToInt16(partes);
            string nomb = filename + "_" + Convert.ToString(DateTime.Today.Day) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Year) + "_Parte";
            string texto1 = "";
            string info = "";
            for (int i = 1; i <= cant; i++)
            {
                texto1 += info;
                texto1 += string.Format("DISK = N'" + dire + "\\" + filename + Convert.ToString(i) + ".bak'");
                info = ",";
            }
            query = @"BACKUP DATABASE[" + "VeterinariaLPPA" + "] TO " + texto1 + " WITH NOFORMAT, NOINIT, NAME = N'" + "VeterinariaLPPA" + "-Completa Base de datos Copia de seguridad', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            ac.ExecuteQuery(query);
            return true;
        }
    }
}
