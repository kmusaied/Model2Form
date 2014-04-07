using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Test_Model2Form.Models;

namespace Test_Model2Form
{
    public partial class ContactUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            Model2Form1.DataSource = new Test_Model2Form.Models.ContactUsInfo();
            Model2Form1.DataBind();
        }

        protected void Model2Form1_SaveClick(object sender, EventArgs e)
        {
            ltrMessage.Text = string.Format("Thanks for Contacting us {0} we will return back to you soon.", ((ContactUsInfo)Model2Form1.UpdatedDataSource).FullName);
        }
    }
}
