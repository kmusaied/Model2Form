using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace EasyWebControls
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ValidationAttributeBase : System.Attribute
    {

        public abstract string ControlType
        {
            get;
        }

        public readonly string ControlToValidatePropertyName = "ControlToValidate";

        public string BindingProperty { get { return "Text"; } }
        public string ErrorMessage = "*";
        public ValidatorDisplay Display = ValidatorDisplay.Dynamic;
        public string Text = "*";
        public string InitialValue = "";

        public virtual void CustomConfig(object source, Control control, Attribute att)
        {

        }


    }
}
