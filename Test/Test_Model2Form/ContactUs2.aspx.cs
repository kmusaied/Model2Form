using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Test_Model2Form
{
    public partial class ContactUs2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            Model2Form1.DataSource = new Test_Model2Form.Models.ContactUsInfo2();
            Model2Form1.DataBind();
        }
    }
}
