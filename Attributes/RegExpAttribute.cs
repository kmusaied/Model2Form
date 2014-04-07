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
    public sealed class RegExpAttribute : ValidationAttributeBase
    {

        public override string ControlType
        {
            get
            {
                return new System.Web.UI.WebControls.RegularExpressionValidator().GetType().AssemblyQualifiedName;
            }
        }
        public string ValidationExpression;
        public bool SetFocusOnError = true;
        
    }
}
