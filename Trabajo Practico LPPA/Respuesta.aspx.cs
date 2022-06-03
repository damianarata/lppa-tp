using System;
using System.Collections.Generic;
using BE;
using BLL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
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
                TextBox1.Visible = false;
                Label3.Visible = false;
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
        Label2.Text = "Bitacora de actividades: ";
        Label3.Visible = true;
        TextBox1.Visible = true;
        Label3.Text = "Usuario: ";
        Button2.Visible = false;
        this.llenarGrid();
        
    }

    private void llenarGrid()
    {
        string detalle = "Consulta de bitacora - Usuario: " + usuarioRespuesta.Usuario;
        usuarioRespuestaBLL.LLenar_Bitacora(usuarioRespuesta.IdUsuario, detalle);
        GridView1.Visible = true;

        List<DetalleBitacora_BE> bitacora = new List<DetalleBitacora_BE>();

        bitacora = usuarioRespuestaBLL.Cargar_Bitacora();
        if (TextBox1.Text != "")
        {
            bitacora = bitacora.FindAll(FilterFunc);
        }
        GridView1.DataSource = bitacora;
        GridView1.DataBind();
    }

    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.llenarGrid();
    }

    protected void textBox1_TextChanged(object sender, EventArgs e)
    {
        llenarGrid();
    }

    private bool FilterFunc(DetalleBitacora_BE detalle)
    {
        if (detalle.Usuario.Contains(TextBox1.Text) || TextBox1.Text.Contains(detalle.Usuario))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}