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
    public abstract class ControlAttributeBase : System.Attribute
    {
        public bool IsDataControl = false;
        public string DataSourcePropertyName = "DataSource";
        public string DataBindMethodName = "DataBind";
        public abstract string ControlTypeName { get; }
        public abstract string BindingProperty {get;}
        public string width ="230px";
        public string height;
        public bool Cleared = true;
        public bool Hide = false;
        
        public virtual void CustomConfig(object source, Control control, Attribute att)
        {
            control.GetType().GetProperty("Width").SetValue(control, Unit.Parse(width), null);
            control.GetType().GetProperty("Height").SetValue(control, Unit.Parse(height), null);
        }
    }
}
