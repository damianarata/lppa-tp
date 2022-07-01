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
        // 1- Se chequea que la integri
        List<Registro_BE> RegistrosDVH = pIntegridad.ChequearDVH();
        List<Registro_BE> RegistrosDVV = pIntegridad.ChequearDVV();

        if (null != RegistrosDVH || null != RegistrosDVV)
        {
            List<Registro_BE> Registros = new List<Registro_BE>();
            if ( null != RegistrosDVH ) { Registros.AddRange(RegistrosDVH); }
            if ( null != RegistrosDVV) { Registros.AddRange(RegistrosDVV); }
            Session["Registros"] = Registros;
            Response.Redirect("FalloIntegridad.aspx");
        }
        //Registros = pIntegridad.ChequearDVV();
        //if (null != Registros)
        //{
        //    Session["Registros"] = Registros;
        //    Response.Redirect("FalloIntegridad.aspx");
        //}
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Login.aspx");

    }
}