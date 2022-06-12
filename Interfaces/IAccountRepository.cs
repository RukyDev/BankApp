using BankApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Interfaces
{
    public interface IAccountRepository
    {
         Task<IdentityResult> SignUp(SignUp signUp);
        Task<string> Login(Login login);
        List<ApplicationUser> GetAlluser();
        Task<string> GetGoldPrice();


    }
}
