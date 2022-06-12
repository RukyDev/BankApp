using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Models
{
    public class Login
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PassWord { get; set; }
    }
}
