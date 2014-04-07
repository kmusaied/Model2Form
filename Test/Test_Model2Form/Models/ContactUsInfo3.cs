using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyWebControls;

namespace Test_Model2Form.Models
{
    [Serializable]
    [Caption("Contact Us Form")]
    public class ContactUsInfo3
    {
        [Hidden]
        public int ID { get; set; }

        [Caption("Full Name")]
        [Required]
        public string FullName { get; set; }

        [RegExp(ValidationExpression=@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", Text="Invalid Email Format")]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [TextBox(TextMode = System.Web.UI.WebControls.TextBoxMode.MultiLine)]
        [Required]
        public string Message { get; set; }

    }
}
