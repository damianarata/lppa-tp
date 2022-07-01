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
            List<Registro_BE> Tablas = new List<Registro_BE>();
            string mHashCalculado;
            String[] mRegistroSplit;
            foreach (DigitoVerificador_BE mDigitoVerificador in ObtenerTablasDigitoVerificador())
            {
                foreach (Registro_BE mReg in ObtenerDatosRegistros(mDigitoVerificador.Tabla))
                {
                    mRegistroSplit = mReg.Datos.Split(char.Parse(";"));
                    mHashCalculado = CalcularDVH(mRegistroSplit[0]);
                    if (mHashCalculado != mRegistroSplit[1])
                    {
                        Tablas.Add(mReg);
                    }
                }
            }
            return Tablas;
        }

        public List<DigitoVerificador_BE> ChequearDigitoVerificadorVertical()
        {
            List<DigitoVerificador_BE> Tablas = new List<DigitoVerificador_BE>();
            foreach (DigitoVerificador_BE mDigitoVerificador in ObtenerTablasDigitoVerificador())
            {
                string DigitoVerificador = CalcularDVV(mDigitoVerificador.Tabla);
                if (DigitoVerificador != mDigitoVerificador.DVV)
                    Tablas.Add(mDigitoVerificador);
            }
            return Tablas;
        }

        #region private functions
        private int ObtenerCantidadRegistrosDigitos(string pTabla)
        {
            string mQuery = "SELECT COUNT(*) as Cantidad FROM Digito_Verificador WHERE Tabla='" + pTabla + "'";
            //return int.Parse(DAO.Instancia().ExecuteScalar(mQuery).ToString());

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

            DataTable Tabla = ac.Leer("borrar_digito_verificador", parametros);
            //string mQuery = "DELETE Digito_Verificador WHERE Tabla='" + pTabla + "'";
            //DAO.Instancia().ExecuteNonQuery(mQuery);
        }

        private List<DigitoVerificador_BE> ObtenerTablasDigitoVerificador()
        {
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
