using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System;

namespace DAL
{
    public class Acceso_DAL
    {
        //CREO LA CONEXION A LA BASE DE DATOS 
        //SqlConnection conexion = new SqlConnection(@"Data Source = LAPTOP-RGP5HKC3\MSSQLSERVER01; Initial CAtalog = VeterinariaLPPA; Integrated Security = SSPI");
        //SqlConnection conexion = new SqlConnection(@"Data Source = LAPTOP - HKJF9404; Initial Catalog = Veterinaria_Cachorros; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //SqlConnection conexion = new SqlConnection(@"Data Source=Z690-F\SQLExpress;Initial Catalog=VeterinariaLPPA;Integrated Security=True");
        //SqlConnection conexion = new SqlConnection(@"Data Source=DESKTOP-4HK2VHN\SQLEXPRESS;Initial Catalog=VeterinariaLPPA;Integrated Security=True");
        //SqlConnection conexion = new SqlConnection(@"Data Source =.; Initial Catalog = VeterinariaLPPA; Integrated Security = True");
        //SqlConnection conexion = new SqlConnection(@"Data Source=LAPTOP-HKJF9404;Initial Catalog=Veterinaria_Cachorros;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlConnection conexion = new SqlConnection(@"Data Source=DESKTOP-ERQ371J\SQLEXPRESS;Initial Catalog=VeterinariaLPPA;Integrated Security=True");

        //ABRO Y CIERRO LA CONEXION
        public void Abrir()
        {
            conexion.Open();
        }

        public void Cerrar()
        {
            conexion.Close();
        }

        SqlTransaction transaccion;

        //HACEMOS UN METODO QUE CREARA EL COMANDO PARA NO TENER QUE REPETIR EL PROCEDIMIENTO
        //EL MISMO TENDRA COMO ENTRADA "STOREPROCEDURE" Y PARAMETROS (privado solo lo uso aca)
        private SqlCommand CrearComando(string storeprocedure, SqlParameter[] parametros)
        {
            // INSTANCIO UN COMANDO DE TIPO SQL COMMAND
            SqlCommand comando = new SqlCommand();
            // AL COMANDO INSTANCIADO LE PASO LA CONEXION
            comando.Connection = conexion;
            // LE INDICO EL TIPO DE COMANDO QUE SERA STORE PROCEDURE
            comando.CommandType = CommandType.StoredProcedure;
            // LE PASO EL COMMANDTEXT --> STRING storeprocedure
            comando.CommandText = storeprocedure;
            //SI LA TRANSACCION TIENE DATOS (ES TRANSACCION) 
            if (transaccion != null)
            {
                //LE PASO LA TRANSACCION A MI COMANDO
                comando.Transaction = transaccion;
            }
            if (parametros != null)
            {
                // SI HAY PARAMETROS, PASO PARAMETROS
                comando.Parameters.AddRange(parametros);
            }
            return comando;
        }

        public DataTable Leer(string storeprocedure, SqlParameter[] parametros)
        {
            //CREAMOS EL OBJETO TABLA 
            DataTable tabla = new DataTable();
            //CREAMOS EL ADAPTADOR (puente para recuperar datos de la tabla)
            SqlDataAdapter adaptador = new SqlDataAdapter();
            //PASAMOS AL SELECT COMMAND EL COMANDO (DESDE EL METODO CREAR COMANDO QUE LO HACE TODO) 
            adaptador.SelectCommand = CrearComando(storeprocedure, parametros);
            // AGREGAMOS LAS FILAS A LA TABLA
            adaptador.Fill(tabla);
            adaptador = null;
            return tabla;
        }

        public bool Verificar_Usuario(string sql, SqlParameter[] parametros)
        {

            Abrir();
            SqlCommand cmd = CrearComando(sql, parametros);
            SqlDataReader lector = cmd.ExecuteReader();

            bool res = lector.HasRows;
            lector = null;
            Cerrar();
            return res;
        }

        public bool ExecuteQuery(string Query)
        {
            bool result = false;

            try
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conexion;
                comm.CommandType = CommandType.Text;
                comm.CommandText = string.Format(Query);
                conexion.Open();
                int i;
                i = comm.ExecuteNonQuery();

                if (i > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = false;
                Console.WriteLine(Query);
            }
            finally
            {
                conexion.Close();
            }
            return result;
        }
    }
}
