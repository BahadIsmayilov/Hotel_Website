using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.SDUtilities
{
    public static class SD
    {
        public enum Roles
        {
            Admin,
            Moderator,
            Member
        }

        public const string AdminRole = "Admin";
        public const string ModeratorRole = "Moderator";
        public const string MemberRole = "Member";
    }
}
