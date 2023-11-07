using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomCookieBased.Data
{
    public class AppUser
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public string Password { get; set; }
        public List<AppUserRole> userRoles { get; set; }
    }
    public class AppRole
    {
        public int Id { get; set; }
        public string Definition { get; set; }
        public List<AppUserRole> userRoles { get; set; }

    }
    public class AppUserRole
    {
        public int userID { get; set; }
        public AppUser AppUser { get; set; }
        public int roleId { get; set; }
        public AppRole AppRole { get; set; }

    }
}
