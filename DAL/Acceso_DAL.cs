using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Acceso_DAL
    {
        //CREO LA CONEXION A LA BASE DE DATOS 
        //SqlConnection conexion = new SqlConnection(@"Data Source = LAPTOP-RGP5HKC3\MSSQLSERVER01; Initial CAtalog = VeterinariaLPPA; Integrated Security = SSPI");
        //SqlConnection conexion = new SqlConnection(@"Data Source = LAPTOP - HKJF9404; Initial Catalog = Veterinaria_Cachorros; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //SqlConnection conexion = new SqlConnection(@"Data Source=Z690-F\SQLExpress;Initial Catalog=VeterinariaLPPA;Integrated Security=True");
        //SqlConnection conexion = new SqlConnection(@"Data Source=DESKTOP-4HK2VHN\SQLEXPRESS;Initial Catalog=VeterinariaLPPA;Integrated Security=True");
        SqlConnection conexion = new SqlConnection(@"Data Source=.;Initial Catalog=VeterinariaLPPA;Integrated Security=True");
        //SqlConnection conexion = new SqlConnection(@"Data Source=LAPTOP-HKJF9404;Initial Catalog=Veterinaria_Cachorros;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

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

        public DataTable Leer(string query)
        {
            //CREAMOS EL OBJETO TABLA 
            DataTable tabla = new DataTable();
            //CREAMOS EL ADAPTADOR (puente para recuperar datos de la tabla)
            SqlDataAdapter adaptador = new SqlDataAdapter();
            //PASAMOS AL SELECT COMMAND EL COMANDO (DESDE EL METODO CREAR COMANDO QUE LO HACE TODO) 
            adaptador.SelectCommand = new SqlCommand(query);
            adaptador.SelectCommand.Connection = conexion;
            Abrir();
            // AGREGAMOS LAS FILAS A LA TABLA
            adaptador.Fill(tabla);
            Cerrar();
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

        #region Integridad
        public string CalcularDVH(string pString)
        {
            int acum = 0;
            byte[] asciiBytes = Encoding.ASCII.GetBytes(pString);
            for (int i = 0; i < asciiBytes.Length; i++)
            {
                acum += asciiBytes[i] * i;
            }
            return acum.ToString();
        }
        public string CalcularDVV(List<string> pRows)
        {
            int acum = 0;
            foreach (string Registro in pRows)
            {
                acum += int.Parse(Registro);
            }
            return acum.ToString();
        }

        public void GuardarDigitoVerificador(List<string> pRegistros, string pTabla)
        {
            string DigitoVerificador = "";
            if (pRegistros.Count > 0)
                DigitoVerificador = CalcularDVV(pRegistros);
            else
            {
                BorrarDigitoVerificador(pTabla);
                return;
            }
            if (ObtenerCantidadRegistrosDigitos(pTabla) == 0)
                GuardarNuevoDigitoVerificador(pTabla, DigitoVerificador);
            else
            {
                ModificarDigitoVerificador(pTabla, DigitoVerificador);
            }
        }

        public List<string> ObtenerDVHs(string pTabla)
        {
            string mQuery = "SELECT DVH FROM " + pTabla;
            DataTable Tabla = Leer(mQuery);
            List<string> resultado = new List<string>();
            foreach (DataRow reg in Tabla.Rows)
            {
                resultado.Add(reg["dvh"].ToString());
            }
            return resultado;
        }

        public string CalcularDVV(string pTabla)
        {
            return CalcularDVV(ObtenerDVHs(pTabla));
        }

        #region private functions
        private int ObtenerCantidadRegistrosDigitos(string pTabla)
        {
            string mQuery = "SELECT COUNT(*) as Cantidad FROM Digito_Verificador WHERE Tabla='" + pTabla + "'";
            //return int.Parse(DAO.Instancia().ExecuteScalar(mQuery).ToString());

            DataTable Tabla = Leer(mQuery);
            return int.Parse(Tabla.Rows[0]["Cantidad"].ToString());
            //List<string> resultado = new List<string>();

            //foreach (DataRow reg in Tabla.Rows)
            //{
            //    resultado.Add(reg["dvh"].ToString());
            //}
            //return resultado;
        }

        private void GuardarNuevoDigitoVerificador(string pTabla, string DigitoVerificador)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter();
            parametros[0].ParameterName = "@tabla";
            parametros[0].DbType = DbType.String;
            parametros[0].Value = pTabla;

            parametros[1] = new SqlParameter();
            parametros[1].ParameterName = "@dvv";
            parametros[1].DbType = DbType.String;
            parametros[1].Value = DigitoVerificador;

            DataTable Tabla = Leer("llenar_digito_verificador", parametros);


            //int Id = DAO.Instancia().ObtenerUltimoId("Digito_Verificador") + 1;
            //string mQuery = "INSERT INTO Digito_Verificador (ID_Digito_Verificador, Tabla, DVV) VALUES (" + Id + ", '" + pTabla + "', '" + DigitoVerificador + "')";
            //DAO.Instancia().ExecuteNonQuery(mQuery);
        }

        private void ModificarDigitoVerificador(string pTabla, string DigitoVerificador)
        {

            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter();
            parametros[0].ParameterName = "@tabla";
            parametros[0].DbType = DbType.String;
            parametros[0].Value = pTabla;

            parametros[1] = new SqlParameter();
            parametros[1].ParameterName = "@dvv";
            parametros[1].DbType = DbType.String;
            parametros[1].Value = DigitoVerificador;

            DataTable Tabla = Leer("modificar_digito_verificador", parametros);
            //string mQuery = "UPDATE Digito_Verificador SET DVV = " + DigitoVerificador + "WHERE Tabla='" + pTabla + "'";
            //DAO.Instancia().ExecuteNonQuery(mQuery);

        }

        private void BorrarDigitoVerificador(string pTabla)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter();
            parametros[0].ParameterName = "@tabla";
            parametros[0].DbType = DbType.String;
            parametros[0].Value = pTabla;

            DataTable Tabla = Leer("borrar_digito_verificador", parametros);
            //string mQuery = "DELETE Digito_Verificador WHERE Tabla='" + pTabla + "'";
            //DAO.Instancia().ExecuteNonQuery(mQuery);
        }
        #endregion
        #endregion
    }
}
