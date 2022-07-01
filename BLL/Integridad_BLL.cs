using System;
using System.Collections.Generic;
using System.Text;
using BE;
using DAL;

namespace BLL
{

    public class Integridad_BLL
    {
        Integridad_DAL pIntegridad = new Integridad_DAL();
        public void ChequearIntegridad()
        {
            try
            {
                this.ChequearDVH();
                this.ChequearDVV();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #region private functions
        public List<Registro_BE> ChequearDVH()
        {
            //(Digito Verificador) 2 - Se obtienen las tablas con errores de verificacion DVH
            List<Registro_BE> Tablas = pIntegridad.ChequearIntegridad();
            if (Tablas.Count > 0)
                return Tablas;
            return null;
        }

        public List<Registro_BE> ChequearDVV()
        {
            //(Digito Verificador) 8 - Se obtienen las tablas con errores de verificacion DVV
            List<Registro_BE> Tabla = pIntegridad.ChequearDigitoVerificadorVertical();
            if (Tabla.Count == 0) { return null; }
            else
            {
                return Tabla;
            }
        }
        #endregion
    }
}
