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
    public partial class List : System.Web.UI.Page
    {
        General gnr;
        DataTable dTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            gnr= new General(form1);
            gnr.command = new SqlCommand("SELECT '<a href=\"Sil2.aspx?Restoran_ID='+O.Restoran_ID+'\">SİL </a>' AS LINK, O.Restoran_ID, O.Restoran_İsmi, F.FDESC, B.BOLUM, O.TARIH FROM OKREST AS O LEFT JOIN OKTYPE AS F ON O.Restoran_Türü = F.FCODE LEFT JOIN OKINFO AS B ON O.Restoran_Türü = B.FCODE AND O.Restoran_Bilgileri = B.BCODE", gnr.cnn);
            SqlDataAdapter dA = new SqlDataAdapter(gnr.command);
            dA.Fill(dTable);
            DataGrid dGrid = new DataGrid();
            dGrid.DataSource = dTable;
            dGrid.DataBind();
            form1.Controls.Add(dGrid);
            gnr.closeDB();
            if (Session["User"] != null)
            {
                Response.Write(Session["User"].ToString());
            }
            
           
        }
    }
}