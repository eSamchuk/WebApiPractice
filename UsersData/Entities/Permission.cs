using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UsersData.Entities
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        public string PermissonName { get; set; }

        public string Description { get; set; }

        public List<RolePermission> RolePermissions { get; set; }
    }
}
