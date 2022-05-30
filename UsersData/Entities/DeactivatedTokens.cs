using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UsersData.Entities
{
    public class DeactivatedToken
    {
        [Key]
        public int Id { get; set; }

        public string Token { get; set; }

    }
}
