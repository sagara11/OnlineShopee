using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopee.Areas.Admin.code
{
    [Serializable]
    public class UserSession
    {
        public string UserName { get; internal set; }
    }
}