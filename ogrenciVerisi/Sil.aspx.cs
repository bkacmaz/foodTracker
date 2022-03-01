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
    public partial class Sil : System.Web.UI.Page
    {
        General gnr;
        TextBox ogrNo = new TextBox();
        Table tbl = new Table();
        Button veriAl = new Button();
        protected void Page_Load(object sender, EventArgs e)
        {
            gnr = new General(form1);
            form1.Controls.Add(tbl);
            gnr.addLine("Restoran ID:", ogrNo, tbl);
            veriAl.Text = "Kayıt Sil";
            veriAl.Click += dbSil;
            form1.Controls.Add(new LiteralControl("<br>"));
            form1.Controls.Add(veriAl);

            if (Session["User"] != null)
            {
                Response.Write(Session["User"].ToString());
            }
        }

        void dbSil(object obj, EventArgs e)
        {

            gnr.command = new SqlCommand("DELETE FROM OKREST WHERE Restoran_ID=@Restoran_ID", gnr.cnn);
            gnr.command.Parameters.AddWithValue("@Restoran_ID", ogrNo.Text);
            gnr.command.ExecuteNonQuery();
            Response.Redirect("main.aspx");
            


        }
    }
}