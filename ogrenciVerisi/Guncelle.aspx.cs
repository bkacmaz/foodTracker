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
    public partial class Guncelle : System.Web.UI.Page
    {
        TextBox restoranID = new TextBox();
        TextBox restoranIsmi = new TextBox();
        DropDownList ddlRestoranTürü = new DropDownList();
        DropDownList ddlRestoranBilgileri = new DropDownList();
        Button veriAl = new Button();
        Button veriAl2 = new Button();
        Table tbl = new Table();
        General gnr;
        protected void Page_Load(object sender, EventArgs e)
        {
            gnr = new General(form1);
            ddlRestoranTürü = gnr.ddlCreate("SELECT * FROM OKTYPE", "FCODE", "FDESC");
            ddlRestoranBilgileri = gnr.ddlCreate("SELECT * FROM OKINFO", "BCODE", "BOLUM");

            form1.Controls.Add(tbl);
            
            gnr.addLine("Restaurant ID:", restoranID, tbl);
            gnr.addLine("Restoran İsmi:", restoranIsmi, tbl);
            gnr.addLine("Restoran Türü:", ddlRestoranTürü, tbl);
            gnr.addLine("Restoran Bilgileri:", ddlRestoranBilgileri, tbl);

            veriAl.Text = "Getir";
            veriAl.Click += dbGetir;
            form1.Controls.Add(new LiteralControl("<br>"));
            form1.Controls.Add(veriAl);

            veriAl2.Text = "Güncelle";
            veriAl2.Click += dbUpdt;
            form1.Controls.Add(new LiteralControl("<br>"));
            form1.Controls.Add(veriAl2);

            if (Session["User"] != null)
            {
                Response.Write(Session["User"].ToString());
            }
        }

        void dbGetir(object obj, EventArgs e)
        {
            DataTable dTable = new DataTable();
            string x = "SELECT* FROM OKREST WHERE Restoran_ID = '" + restoranID.Text + "'";
            gnr.command = new SqlCommand(x, gnr.cnn);
            SqlDataAdapter dA = new SqlDataAdapter(gnr.command);
            dA.Fill(dTable);
            if(dTable.Rows.Count == 0)
            {
                form1.Controls.Add(new LiteralControl("Kayıt Bulunamadı"));
            }
            else
            {
                restoranIsmi.Text = dTable.Rows[0]["Restoran_İsmi"].ToString();
                ddlRestoranTürü.SelectedValue = dTable.Rows[0]["Restoran_Türü"].ToString();
                ddlRestoranBilgileri.SelectedValue = dTable.Rows[0]["Restoran_Bilgileri"].ToString();
            }
            

        }

        void dbUpdt(object obj, EventArgs e)
        {
            gnr.command = new SqlCommand("UPDATE OKREST SET Restoran_İsmi = @Restoran_İsmi, Restoran_Türü= @Restoran_Türü, Restoran_Bilgileri = @Restoran_Bilgileri WHERE Restoran_ID = @Restoran_ID", gnr.cnn);          

          //  gnr.command = new SqlCommand("INSERT INTO OKREST (Restoran_ID,Restoran_İsmi,Restoran_Türü,Restoran_Bilgileri) VALUES (@Restoran_ID,@Restoran_İsmi,@Restoran_Türü,@Restoran_Bilgileri)", gnr.cnn);
            gnr.command.Parameters.AddWithValue("@Restoran_ID", restoranID.Text);
            gnr.command.Parameters.AddWithValue("@Restoran_İsmi", restoranIsmi.Text);
            gnr.command.Parameters.AddWithValue("@Restoran_Türü", ddlRestoranTürü.SelectedValue.ToString());
            gnr.command.Parameters.AddWithValue("@Restoran_Bilgileri", ddlRestoranBilgileri.SelectedValue.ToString());


            gnr.command.ExecuteNonQuery();

            Response.Redirect("Main.aspx");
        }


    }
}