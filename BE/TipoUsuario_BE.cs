using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class TipoUsuario_BE
    {
        public int id { get; set; }
        public string tipo_usuario { get; set; }

        public List<Accion_BE> listaAcciones { get; set; }

    }
}
