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
        List<DigitoVerificador_BE> tablas = (List<DigitoVerificador_BE>)Session["Tablas"];

        if (!IsPostBack)
        {
            foreach (DigitoVerificador_BE digito in tablas)
            {
                if (null == ListBox1.Items.FindByValue(digito.Tabla))
                {
                    ListBox1.Items.Add(digito.Tabla);
                }
            }
        }
    }
}