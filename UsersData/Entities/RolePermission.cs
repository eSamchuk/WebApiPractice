using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UsersData.Entities
{
    public class RolePermission
    {
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public int PermissionId { get; set; }

        public Permission Permission { get; set; }
    }
}
