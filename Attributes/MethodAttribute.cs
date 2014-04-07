using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyWebControls.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MethodAttribute : System.Attribute
    {
        public MethodType MethodType
        {
            get;
            set;
        }
    }
}
