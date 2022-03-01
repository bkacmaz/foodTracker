using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ogrenciVerisi
{
    public partial class Main : System.Web.UI.Page
    {
        General gnr;
        protected void Page_Load(object sender, EventArgs e)
        {
            gnr = new General(form1);
            Session["User"] = "Berfin";
        }
    }
}