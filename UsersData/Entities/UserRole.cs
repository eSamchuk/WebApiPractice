using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UsersData.Entities
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
