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
    public sealed class RadioAttribute : ControlAttributeBase
    {
        public override string ControlTypeName
        {
            get
            {
                return new RadioButtonList().GetType().AssemblyQualifiedName;
            }
        }
        public RepeatDirection RepeatDirection = RepeatDirection.Vertical;
        public override string BindingProperty
        {
            get { return "SelectedValue"; }
        }
        public string DataTextField;
        public string DataValueField;
        public bool AutoPostBack = false;
        public string IndexChangeEventName = "SelectedIndexChanged";
        public RadioAttribute()
        {
            IsDataControl = true;
        }
    }
}
