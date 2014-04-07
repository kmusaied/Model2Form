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
    public sealed class TextBoxAttribute : ControlAttributeBase
    {
        public override string ControlTypeName
        {
            get
            {
                return new System.Web.UI.WebControls.TextBox().GetType().AssemblyQualifiedName;
            }
        }
        public TextBoxMode TextMode;
        //public string style="text-align:left;";
        public override string BindingProperty { get { return "Text"; } }
        public string Text;
        public int MaxLength;
        public string CssClass = string.Empty;
    }
}
