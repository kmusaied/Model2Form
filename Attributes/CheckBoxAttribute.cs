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
    public sealed class CheckBoxAttribute : ControlAttributeBase
    {
        public override string ControlTypeName
        {
            get
            {
                return new System.Web.UI.WebControls.CheckBox().GetType().AssemblyQualifiedName;
            }
        }
        public override string BindingProperty { get { return "Checked"; } }
        public string Text;
    }
}
