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
    public sealed class CheckBoxListAttribute : ControlAttributeBase
    {
        public override string ControlTypeName
        {
            get
            {
                return new System.Web.UI.WebControls.CheckBoxList().GetType().AssemblyQualifiedName;
            }
        }
       
        public string Text;
        public string DataTextField;
        public string DataValueField;
        public bool AutoPostBack = false;
        public readonly string IndexChangeEventName = "SelectedIndexChanged";
        public CheckBoxListAttribute()
        {
            IsDataControl = true;
        }

        public override string BindingProperty
        {
            get { return "SelectedValue"; }
        }
    }
}
