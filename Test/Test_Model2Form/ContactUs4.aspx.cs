using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Test_Model2Form.Models;

namespace Test_Model2Form
{
    public partial class ContactUs4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Model2Form1.DataSource = new ContactUsInfo4();
            Model2Form1.DataBind();
        }

        protected void Model2Form1_LookupControlCreated(object sender, EasyWebControls.FieldEventArgs e)
        {
            if (e.FieldName == "Type")
            {
                DropDownList list = (DropDownList)sender;
                list.Items.Add(new ListItem("Suggestion","1"));
                list.Items.Add(new ListItem("Complaint","2"));
                
            }
        }
    }
}
