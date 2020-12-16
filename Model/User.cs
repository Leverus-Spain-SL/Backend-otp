using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPT_2021.Model
{
    public class User
    {
        public string EMAIL { get; set; }
        public int ROLE { get; set; }
        public string NAME { get; set; }
        public int BANNED { get; set; }

        public string TOKEN { get; set; }

    }
    public enum RoleUser
    {
        ADMIN = 0,
        MODERATOR = 1,
        AUX = 2,
        SUPPORT = 3,
        ARTIST = 4,
        CLIENT = 5,
        PRECLIENT = 6
    }
    public enum BannedUser
    {
        ACTIVE = 0,
        BANNED = 1
    }
}
