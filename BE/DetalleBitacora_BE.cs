using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class DetalleBitacora_BE
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string detalle;

        public string Detalle
        {
            get { return detalle; }
            set { detalle = value; }
        }


        private int id_usuario; 
                
        public int Id_Usuario   
        {
            get { return id_usuario; }
            set { id_usuario = value; }
        }

        private DateTime fecha;

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        private string usuario;

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }



    }
}
