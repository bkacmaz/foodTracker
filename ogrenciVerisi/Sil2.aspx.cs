using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


namespace ogrenciVerisi
{
    public partial class Sil2 : System.Web.UI.Page
    {
        General gnr;
        protected void Page_Load(object sender, EventArgs e)
        {
            gnr = new General(form1);
            string param = Request["NO"];
            Response.Write(param);
            gnr.command = new SqlCommand("DELETE FROM OKREST WHERE Restoran_ID=@Restoran_ID", gnr.cnn);
            gnr.command.Parameters.AddWithValue("@Restoran_ID", param);
            gnr.command.ExecuteNonQuery();
            Response.Redirect("main.aspx");

        }
    }
}