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
            List<Registro_BE> Tablas = pIntegridad.ChequearIntegridad();
            if (Tablas.Count > 0)
                return Tablas;
            return null;

            //if (Tabla.Count == 0) { }
            //else
            //{
            //    string mDetalle = "Fallo integridad";
            //    foreach (DigitoVerificador_BE mDVV in Tabla)
            //    {
            //        //Crear Registro en bitacora
            //        throw new Exception(mDetalle);
            //    }
            //}
        }

        public void ChequearDVV()
        {
            List<DigitoVerificador_BE> Tabla = pIntegridad.ChequearDigitoVerificadorVertical();
            if (Tabla.Count == 0) { }
            else
            {
                string mDetalle = "Fallo integridad digito verificador";
                foreach (DigitoVerificador_BE mDVV in Tabla)
                {
                    //Crear Registro en bitacora
                    throw new Exception(mDetalle);
                }
            }
        }
        #endregion
    }
}
