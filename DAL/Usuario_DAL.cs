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
                //usuarioBE.Bloqueado = Convert.ToInt32(reg["bloqueado"].ToString());

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
            string fecha = "";
            int id = 0;
            foreach (DataRow reg in Tabla.Rows)
            {
                fecha = reg["fecha"].ToString();
                id = Convert.ToInt32(reg["id"].ToString());
            }
            ac.CalcularDVH(id.ToString() + detalle + fecha + id_usuario);

            parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter();
            parametros[0].ParameterName = "@id";
            parametros[0].DbType = DbType.String;
            parametros[0].Value = id;

            parametros[1] = new SqlParameter();
            parametros[1].ParameterName = "@dvh";
            parametros[1].DbType = DbType.String;
            parametros[1].Value = ac.CalcularDVH(id.ToString() + detalle + fecha + id_usuario);

            Tabla = ac.Leer("update_bitacora", parametros);
            ac.GuardarDigitoVerificador(ac.ObtenerDVHs("Bitacora"),"Bitacora");
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

        //Agregado para el bloqueo de usuario por reintentos de contraseña
        public void Bloquear_Usuario(string usuario)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter();
            parametros[0].ParameterName = "@usu";
            parametros[0].DbType = DbType.String;
            parametros[0].Value = usuario;

            DataTable Tabla = ac.Leer("bloquear_usuario", parametros);

        }

        public Usuario_BE validar_usuario_sinpassword(string usuario)
        {


            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter();
            parametros[0].ParameterName = "@usu";
            parametros[0].DbType = DbType.String;
            parametros[0].Value = usuario;


            DataTable Tabla = ac.Leer("verificar_usuario_sinpassword", parametros);

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
                usuarioBE.Bloqueado = Convert.ToInt32(reg["bloqueado"].ToString());

            }
            return usuarioBE;
        }

        public void blanquear_password(string usuario)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter();
            parametros[0].ParameterName = "@usu";
            parametros[0].DbType = DbType.String;
            parametros[0].Value = usuario;

            DataTable Tabla = ac.Leer("blanquear_password", parametros);
        }
    }
}
