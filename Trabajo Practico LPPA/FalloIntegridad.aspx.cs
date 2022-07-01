using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BE;

public partial class FalloIntegridad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<Registro_BE> tablas = (List<Registro_BE>)Session["Registros"];

        if (!IsPostBack)
        {
            //(Digito Verificador) 13 - Se muestran las tablas por pantalla
            this.llenarGrid();
        }

    }

    private void llenarGrid()
    {
        GridView1.Visible = true;

        List<Registro_BE> registros = new List<Registro_BE>();

        registros = (List<Registro_BE>)Session["Registros"];
        GridView1.DataSource = registros;
        GridView1.DataBind();
    }

    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.llenarGrid();
    }
}