using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using BE;



namespace BLL
{
    public class Usuario_BLL
    {
        Usuario_DAL mapper = new Usuario_DAL();

        public Usuario_BE Verificar_Usuario(string usuario, string contraseña)
        {
            return mapper.loguear(usuario, contraseña);
        }

        public void LLenar_Bitacora(int id_usuario, string detalle)
        {
            mapper.LLenar_Bitacora(id_usuario, detalle);
        }

        public List<Accion_BE> Buscar_Acciones(int id_tipousuario)
        {
           return mapper.Buscar_Acciones(id_tipousuario);
        }

        public List<DetalleBitacora_BE> Cargar_Bitacora()
        {
            return mapper.Listar_Bitacora();
        }

        public bool TakeDB(string filename, string dire, int partes)
        {
            return mapper.TakeDB(filename, dire, partes);
        }
        public bool RestoreDB(string dire)
        {
            return mapper.RestoreDB(dire);
        }
    }
}

