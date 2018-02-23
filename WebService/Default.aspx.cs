using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceReference1;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WebService1SoapClient client = new WebService1SoapClient();
        int sum = client.Add(1, 4);
        Response.Write(sum);
    }
}