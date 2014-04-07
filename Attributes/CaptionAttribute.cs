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
    [AttributeUsage(AttributeTargets.All)]
    public sealed class CaptionAttribute : System.Attribute
    {
        public CaptionAttribute(string name)
        {
            this.Text = name;
        }

        public CaptionAttribute()
        {

        }

        public  string LabelTypeName 
        {
            get
            { 
                return new Label().GetType().AssemblyQualifiedName;
            }
        }
        
        public string Text;
        public bool Required=true;
    }
}
