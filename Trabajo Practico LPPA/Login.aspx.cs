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
        usuarioBE = usuarioBLL.Verificar_Usuario_sinpassword(TextBox1.Text);
        //Verificar si existe y si esta bloqueado
        if (!string.IsNullOrEmpty(usuarioBE.Usuario) && (usuarioBE.Bloqueado < 3))
        {
                   //Verificar si la password esta correcta
                    usuarioBE = usuarioBLL.Verificar_Usuario(TextBox1.Text, TextBox2.Text);
                    if(!string.IsNullOrEmpty(usuarioBE.Usuario))
                    {
                          //Trae el perfil del usuario de la base
                          usuarioBE.TipoUsuario.listaAcciones = usuarioBLL.Buscar_Acciones(usuarioBE.TipoUsuario.id);
                          //Bloqueado se pone 0
                          usuarioBLL.blanquear_password(TextBox1.Text);
                          //Guardamos el objeto usuario en la variables de sesion
                          Session["usuario"] = usuarioBE;
                          Response.Redirect("Respuesta.aspx");
                    }
                    //contraseña incorrecta aumentar contador bloqueado en 1
                    else
                    {
                        usuarioBLL.Bloquear_usuario(TextBox1.Text);
                        Label1.Text = "Su contraseña es incorrecta, a las 3 veces se le bloqueara el usuario";
                        Label1.Visible = true;
                    }
        }
        else if (usuarioBE.Bloqueado == 3)
        {
                    Label1.Text = "Su usuario esta bloqueada. Avisar al webmaster para el desbloqueo";
                    Label1.Visible = true;
        }
    
        else 
        {
                    //Usuario o contraseña invalidos
                    Label1.Visible = true;
        }
     } 

    // Ir a buscar a la base de datos el usr por nombre de usuario y solo eso y si encontras el usuario, traer todo el registro y recien ahi validar la password (en usr BLL)
    // O sea 2 metodos uno verificar_usuario como esta ahora pero SIN la contraseña y otro metodo a parte para validar la password (valida_password) en usr BLL
    // si el usr no existe no pasa nada, pasamos de largo, en caso de que exista el usr y la password este mal se incrementa el campo bloqueo (bloqueo_usuario) 
    // en caso de que el usr exista y bloqueado > 3 si le avisa que su usr esta bloqueado (sin importar que password meta)
    // Si el usr esta ok y la password esta ok y bloqueado es < 3 se le pone en 0 el bloqueado


}