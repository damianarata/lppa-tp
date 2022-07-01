using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BE;

public partial class Inicio : System.Web.UI.Page
{
    Integridad_BLL pIntegridad = new Integridad_BLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        List<DigitoVerificador_BE> Tablas = pIntegridad.ChequearDVH();
        if (null != Tablas)
        {
            Session["Tablas"] = Tablas;
            Response.Redirect("FalloIntegridad.aspx");
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Login.aspx");

    }
}