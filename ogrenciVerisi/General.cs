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
   
   public class General
    {
        public SqlCommand command;
        public SqlConnection cnn;
        
       public General(System.Web.UI.HtmlControls.HtmlForm frm)
        {

            string connectionString = "Data Source=PC-34;Initial Catalog=OGRENCI;User ID=SQLuser;Password=1";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            frm.Controls.Add(new LiteralControl("<a href = Main.aspx> Anasayfa </a> "));
            frm.Controls.Add(new LiteralControl(" <a href = Ekle.aspx> Ekle </a> "));
            frm.Controls.Add(new LiteralControl(" <a href = Guncelle.aspx> Güncelle </a> "));
            frm.Controls.Add(new LiteralControl(" <a href = Sil.aspx> Sil </a> "));
            frm.Controls.Add(new LiteralControl(" <a href = List.aspx> Liste </a>"));

        }

       public void closeDB()
        {
            command.Dispose();
            cnn.Close();
            cnn.Dispose();
        }

        public DropDownList ddlCreate(string sqlStr, string codeField, string descField)
        {
            DataTable dTable = new DataTable(); 

            if(HttpContext.Current.Session[codeField] != null)
            {
                dTable = (DataTable) HttpContext.Current.Session[codeField];
            }
            
            else
            {
                command = new SqlCommand(sqlStr, cnn);
                SqlDataAdapter dA = new SqlDataAdapter(command);        
                dA.Fill(dTable);
                HttpContext.Current.Session[codeField] = dTable;
            }
            
            DropDownList ddl = new DropDownList();
            ddl.DataTextField = descField; // text field name of table dispalyed in dropdown       
            ddl.DataValueField = codeField;
            ddl.DataSource = dTable;
            ddl.DataBind();
            return ddl;
        }

        public void addLine(string label, object obj, Table tbl)
        {

            Label lbl = new Label();
            lbl.Text = label;
            TableRow trow = new TableRow();
            TableCell tCellLabel = new TableCell();
            tCellLabel.Controls.Add(lbl);
            trow.Controls.Add(tCellLabel);

            if (obj.ToString().IndexOf("TextBox") > 0)
            {
                TextBox txtbox = (TextBox)obj;
                TableCell tCellTextBox = new TableCell();
                tCellTextBox.Controls.Add(txtbox);
                trow.Controls.Add(tCellTextBox);

            }
            else
            {
                DropDownList ddlList = (DropDownList)obj;
                TableCell tCellDDL = new TableCell();
                tCellDDL.Controls.Add(ddlList);
                trow.Controls.Add(tCellDDL);
            }
            tbl.Controls.Add(trow);           
        }
    }
}