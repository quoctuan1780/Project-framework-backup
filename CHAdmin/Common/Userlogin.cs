using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHAdmin.Common
{
    [Serializable]
    public class Userlogin
    {
        public long UserID { get; set; }
        public string userName { get; set; }
    }
}