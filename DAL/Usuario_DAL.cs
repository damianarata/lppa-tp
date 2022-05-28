﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BE;
namespace DAL
{
    public class Usuario_DAL
    {
        Acceso_DAL ac = new Acceso_DAL();
        public Usuario_BE loguear(string usuario, string contraseña)
        {
         

            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter();
            parametros[0].ParameterName = "@usu";
            parametros[0].DbType = DbType.String;
            parametros[0].Value = usuario;

            parametros[1] = new SqlParameter();
            parametros[1].ParameterName = "@pass";
            parametros[1].DbType = DbType.String;
            parametros[1].Value = contraseña;

            DataTable Tabla = ac.Leer("verificar_usuario", parametros);

            Usuario_BE usuarioBE = new Usuario_BE(); 

            foreach (DataRow reg in Tabla.Rows)
            {

                usuarioBE.Usuario = reg["usuario"].ToString();
                usuarioBE.Contraseña = reg["contraseña"].ToString();
                usuarioBE.IdUsuario = Convert.ToInt32(reg["id"].ToString());
                usuarioBE.Nombre = reg["nombre"].ToString();
                TipoUsuario_BE tipoUsuario = new TipoUsuario_BE(); 
                tipoUsuario.id = Convert.ToInt32(reg["id_tipo_usuario"].ToString());
                tipoUsuario.tipo_usuario = reg["tipo_usuario"].ToString();
                usuarioBE.TipoUsuario = tipoUsuario; 

                //user.Sesion.sesionIniciada = bool.Parse(reg["sesionIniciada"].ToString());

            }
            return usuarioBE;
        }

        public void LLenar_Bitacora(int id_usuario, string detalle)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter();
            parametros[0].ParameterName = "@id_usu";
            parametros[0].DbType = DbType.String;
            parametros[0].Value = id_usuario;

            parametros[1] = new SqlParameter();
            parametros[1].ParameterName = "@detalle";
            parametros[1].DbType = DbType.String;
            parametros[1].Value = detalle;

            DataTable Tabla = ac.Leer("llenar_bitacora", parametros);

        }

        public List<Accion_BE> Buscar_Acciones(int id_tipo_usuario)
        {
            List<Accion_BE> acciones = new List<Accion_BE>();

            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter();
            parametros[0].ParameterName = "@id_tipo_usu";
            parametros[0].DbType = DbType.String;
            parametros[0].Value = id_tipo_usuario;
          

            DataTable Tabla = ac.Leer("listar_acciones", parametros);
            foreach (DataRow reg in Tabla.Rows)
            {
                Accion_BE accion = new Accion_BE();
                accion.id = Convert.ToInt32(reg["id"].ToString());
                accion.detalle = reg["detalle"].ToString();

                acciones.Add(accion);
            }
            return acciones;
        }

        

    }
}
