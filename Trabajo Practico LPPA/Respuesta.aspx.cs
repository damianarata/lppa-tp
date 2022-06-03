using System;
using System.Collections.Generic;
using BE;
using BLL;

public partial class Respuesta : System.Web.UI.Page
{
    Usuario_BE usuarioRespuesta = new Usuario_BE();
    Usuario_BLL usuarioRespuestaBLL = new Usuario_BLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        usuarioRespuesta = (Usuario_BE)Session["usuario"];

        if (!IsPostBack) 
        {
            //recuperamos variables de sesion para instanciar un objeto usuario
            
            if (usuarioRespuesta.TipoUsuario.id == 1)
            {
                Button2.Visible = true;
            }
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

    protected void Button2_Click(object sender, EventArgs e)
    {
        string detalle = "Consulta de bitacora - Usuario: " + usuarioRespuesta.Usuario;
        usuarioRespuestaBLL.LLenar_Bitacora(usuarioRespuesta.IdUsuario, detalle);
        GridView1.Visible = true;

        List<DetalleBitacora_BE> bitacora = new List<DetalleBitacora_BE>();

        bitacora = usuarioRespuestaBLL.Cargar_Bitacora();

        GridView1.DataSource = bitacora;
        GridView1.DataBind();
        Button2.Visible = false;

    }

   
}