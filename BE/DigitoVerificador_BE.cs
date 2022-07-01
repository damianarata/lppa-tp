using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class DigitoVerificador_BE
    {
        public int ID_Digito_Verificador { get; set; }
        public string Tabla { get; set; }
        public string DVV { get; set; }
        public string ID_Registro { get; set; }
        public DigitoVerificador_BE() { }
        public DigitoVerificador_BE(int pId)
        {
            this.ID_Digito_Verificador = pId;
        }
    }
}
