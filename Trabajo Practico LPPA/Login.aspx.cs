using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using BLL;
using BE;

public partial class _Default : System.Web.UI.Page 
{
    Usuario_BLL usuarioBLL = new Usuario_BLL();
    Usuario_BE usuarioBE = new Usuario_BE(); 
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        
            usuarioBE = usuarioBLL.Verificar_Usuario(TextBox1.Text, TextBox2.Text);

        if (!string.IsNullOrEmpty(usuarioBE.Usuario))
        {
            usuarioBE.TipoUsuario.listaAcciones = usuarioBLL.Buscar_Acciones(usuarioBE.TipoUsuario.id);
            Session["usuario"] = usuarioBE;
            Response.Redirect("Respuesta.aspx");

        }
        else 
        {
            Label1.Visible = true;
        } 

        
    }
}