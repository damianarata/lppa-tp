using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using BE;
using System;

namespace DAL
{
    public class Integridad_DAL
    {
        Acceso_DAL ac = new Acceso_DAL();

        public string CalcularDVH(string pString)
        {
            // (Digito Verificador) 6 - Calculo de dvh basado en la codificacion ASCII
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
            // (Digito Verificador) 11 - Calculo de dvv como una suma acumulada de los dvh de cada registro de la tabla
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
            DataTable Tabla = ac.Leer(mQuery);
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

        public List<Registro_BE> ChequearIntegridad()
        {
            //(Digito Verificador) 3 - Se recalculan los valores de dvh y se los compara con los guardados. En caso de error, sumamos el registro erroneo a la tabla
            List<Registro_BE> Tablas = new List<Registro_BE>();
            string mHashCalculado;
            String[] mRegistroSplit;
            foreach (DigitoVerificador_BE mDigitoVerificador in ObtenerTablasDigitoVerificador()) // -->(Digito Verificador) 4 
            {
                foreach (Registro_BE mReg in ObtenerDatosRegistros(mDigitoVerificador.Tabla)) // -->(Digito Verificador) 5 
                {
                    mRegistroSplit = mReg.Datos.Split(char.Parse(";"));
                    mHashCalculado = CalcularDVH(mRegistroSplit[0]); // -->(Digito Verificador) 6
                    if (mHashCalculado != mRegistroSplit[1])
                    {
                        Tablas.Add(mReg);
                    }
                }
            }
            return Tablas;
        }

        public List<Registro_BE> ChequearDigitoVerificadorVertical()
        {
            //(Digito Verificador) 9 - Se Recalculan los DVV y se los compara con los guardados. En caso de error, sumamos el registro erroneo a la tabla
            List<Registro_BE> Tablas = new List<Registro_BE>();
            foreach (DigitoVerificador_BE mDigitoVerificador in ObtenerTablasDigitoVerificador()) //-->(Digito Verificador) 10 
            {
                string DigitoVerificador = CalcularDVV(mDigitoVerificador.Tabla); //-->(Digito Verificador) 11 
                if (DigitoVerificador != mDigitoVerificador.DVV)
                {
                    Registro_BE mRegisto = new Registro_BE();
                    mRegisto.Tabla = mDigitoVerificador.Tabla;
                    mRegisto.ID_Registro = "DVV";
                    Tablas.Add(mRegisto);
                }
            }
            return Tablas;
        }

        #region private functions
        private int ObtenerCantidadRegistrosDigitos(string pTabla)
        {
            string mQuery = "SELECT COUNT(*) as Cantidad FROM Digito_Verificador WHERE Tabla='" + pTabla + "'";

            DataTable Tabla = ac.Leer(mQuery);
            return int.Parse(Tabla.Rows[0]["Cantidad"].ToString());
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

            DataTable Tabla = ac.Leer("llenar_digito_verificador", parametros);
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

            DataTable Tabla = ac.Leer("modificar_digito_verificador", parametros);

        }

        private void BorrarDigitoVerificador(string pTabla)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter();
            parametros[0].ParameterName = "@tabla";
            parametros[0].DbType = DbType.String;
            parametros[0].Value = pTabla;

            DataTable Tabla = ac.Leer("borrar_digito_verificador", parametros);
        }

        private List<DigitoVerificador_BE> ObtenerTablasDigitoVerificador()
        {
            //(Digito Verificador) 4 - Se obtienen las tablas que tiene digitos verificadores
            //(Digito Verificador) 10 - Se obtienen las tablas que tiene digitos verificadores
            string mQuery = "SELECT * FROM Digito_Verificador";
            DataTable Tabla = ac.Leer(mQuery);
            List<DigitoVerificador_BE> mTablas = new List<DigitoVerificador_BE>();
            foreach (DataRow reg in Tabla.Rows)
            {
                DigitoVerificador_BE mDigitoVerificador = new DigitoVerificador_BE(int.Parse(reg["ID_Digito_Verificador"].ToString()));
                mDigitoVerificador.Tabla = reg["Tabla"].ToString();
                mDigitoVerificador.DVV = reg["DVV"].ToString();
                mTablas.Add(mDigitoVerificador);
            }
            return mTablas;
        }

        private List<Registro_BE> ObtenerDatosRegistros(string pTabla)
        {
            // (Digito Verificador) 5 - Se obtienen los datos de los registros para recalcular el dvh
            List<Registro_BE> Registros = new List<Registro_BE>();
            string Registro = "";
            string DVH = "";

            string mQuery = "SELECT * FROM " + pTabla;
            DataTable Tabla = ac.Leer(mQuery);

            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow mROW in Tabla.Rows)
                {
                    Registro_BE mRegistro = new Registro_BE();
                    for (int i = 0; i < Tabla.Columns.Count; i++)
                    {
                        string mCol = Tabla.Columns[i].ColumnName.ToString();
                        if (mCol != "dvh")
                            Registro += mROW[mCol].ToString();
                        if (mCol == "dvh")
                            DVH = mROW[mCol].ToString();
                        if (mCol == "id")
                            mRegistro.ID_Registro = mROW[mCol].ToString();
                    }
                    Registro += ";" + DVH;
                    mRegistro.Datos = Registro;
                    mRegistro.Tabla = pTabla;
                    Registros.Add(mRegistro);
                    Registro = "";
                }
            }
            return Registros;
        }
        #endregion
    }
}
