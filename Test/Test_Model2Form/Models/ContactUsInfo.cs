using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_Model2Form.Models
{
    [Serializable]
    public class ContactUsInfo
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        
    }
}
