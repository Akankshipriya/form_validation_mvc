using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class UserRole
    {
        [Key]
        public int RoleId { get; set; }


        public string RoleName { get; set; }
        public List<User> Users { get; set; }
    }
}