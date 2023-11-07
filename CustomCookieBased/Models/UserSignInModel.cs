using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomCookieBased.Models
{
    public class UserSignInModel
    {
        public string userName { get; set; }
        public string Password { get; set; }
        public bool rememberMe { get; set; }
    }
}
