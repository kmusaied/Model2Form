using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyWebControls;

namespace Test_Model2Form.Models
{
    [Serializable]
    [Caption("Contact Us Form")]
    public class ContactUsInfo2
    {
        [Hidden]
        public int ID { get; set; }

        [Caption("Full Name")]
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        [TextBox(TextMode=System.Web.UI.WebControls.TextBoxMode.MultiLine)]
        public string Message { get; set; }

    }
}
