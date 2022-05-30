using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UsersData.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Token { get; set; }

        public string PasswordHash { get; set; }

        public List<UserRole> UserRoles { get; set; }

        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryDate { get; set; }

    }
}
