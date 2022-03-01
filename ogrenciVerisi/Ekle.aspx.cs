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
    public partial class Ekle : System.Web.UI.Page
    {
        TextBox restoranID = new TextBox();
        TextBox restoranIsmi = new TextBox();
        
        Button veriAl = new Button();
        Table tbl = new Table();
        DropDownList ddlRestoranTürü = new DropDownList();
        DropDownList ddlRestoranBilgileri = new DropDownList();
        General gnr;
        protected void Page_Load(object sender, EventArgs e)
        {
            gnr = new General(form1);
            ddlRestoranTürü = gnr.ddlCreate("SELECT * FROM OKTYPE", "FCODE", "FDESC");
            ddlRestoranBilgileri = gnr.ddlCreate("SELECT * FROM OKINFO", "BCODE", "BOLUM");
            ddlRestoranTürü.SelectedIndexChanged += bolumLoad;
            ddlRestoranTürü.Load += bolumLoad;
            
            form1.Controls.Add(new LiteralControl("<h1> * Yeni Bir Restoran Eklemek İçin Lütfen İlgili Bilgileri Doldurunuz *</h1>"));
            form1.Controls.Add(tbl);
            gnr.addLine("Restaurant ID:", restoranID, tbl);
            gnr.addLine("Restoran İsmi:", restoranIsmi, tbl);
            gnr.addLine("Restoran Türü:", ddlRestoranTürü, tbl);
            gnr.addLine("Restoran Bilgileri:", ddlRestoranBilgileri, tbl);
           

            veriAl.Text = "Kaydet";
            veriAl.Click += dbAdd;
            form1.Controls.Add(new LiteralControl("<br>"));
            form1.Controls.Add(veriAl);

            if (Session["User"] != null)
            {
                Response.Write(Session["User"].ToString());
            }

        }

  
        void bolumLoad(object obj, EventArgs e)
        {
         string value = ddlRestoranTürü.SelectedValue;

            ddlRestoranBilgileri = gnr.ddlCreate("SELECT * FROM OKINFO", "BCODE", "BOLUM");

        }

        void dbAdd(object obj, EventArgs e)
        {
            
            gnr.command = new SqlCommand("INSERT INTO OKREST (Restoran_ID,Restoran_İsmi,Restoran_Türü,Restoran_Bilgileri) VALUES (@Restoran_ID,@Restoran_İsmi,@Restoran_Türü,@Restoran_Bilgileri)", gnr.cnn);
            gnr.command.Parameters.AddWithValue("@Restoran_ID", restoranID.Text);
            gnr.command.Parameters.AddWithValue("@Restoran_İsmi", restoranIsmi.Text);
            gnr.command.Parameters.AddWithValue("@Restoran_Türü", ddlRestoranTürü.SelectedValue.ToString());
            gnr.command.Parameters.AddWithValue("@Restoran_Bilgileri", ddlRestoranBilgileri.SelectedValue.ToString());
            gnr.command.ExecuteNonQuery();
            

        }
    }
}