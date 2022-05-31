using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BLL;

public partial class Respuesta : System.Web.UI.Page
{
    Usuario_BE usuarioRespuesta = new Usuario_BE();
    Usuario_BLL usuarioRespuestaBLL = new Usuario_BLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        //recuperamos variables de sesion para instanciar un objeto usuario
        usuarioRespuesta = (Usuario_BE)Session["usuario"];
        string detalle = "Inicio de Sesion - Usuario: " + usuarioRespuesta.Usuario; 
        //se genera un registro en bitacora
        usuarioRespuestaBLL.LLenar_Bitacora(usuarioRespuesta.IdUsuario, detalle);
        Label1.Text = "Bienvenido " + usuarioRespuesta.Nombre + " Usted tiene permisos de: " + usuarioRespuesta.TipoUsuario.tipo_usuario;
        //listado de roles
        foreach (Accion_BE accion in usuarioRespuesta.TipoUsuario.listaAcciones)
        {
            ListBox1.Items.Add(accion.detalle);
        }

    }
}