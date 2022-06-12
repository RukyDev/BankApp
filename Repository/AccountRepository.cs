using Abp.UI;
using AutoMapper;
using BankApi.Interfaces;
using BankApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Service
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IConfiguration Configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = Configuration;
        }

        public async Task<IdentityResult> SignUp(SignUp signUp)
        {
            var user = new ApplicationUser
            {
                FirstName = signUp.FirstName,
                LastName = signUp.LastName,
                Email = signUp.Email,
                UserName = signUp.Email
            };

            return await _userManager.CreateAsync(user, signUp.PassWord);
        }

        public async Task<string> Login(Login login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.PassWord, false, false);
            if (!result.Succeeded)
            {
                return null;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authSignInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:validIssuer"],
                audience: _configuration["JWT:validIssuer"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public  List<ApplicationUser> GetAlluser()
        {
            var usr = _userManager.Users.ToList();
            if (usr == null)
            {
                throw new ArgumentNullException(nameof(usr));
            }
            return usr;
            
        }

        public async Task<string> GetGoldPrice()
        {
            var body = "";
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_configuration["Credentials:baseUrl"]),
                Headers =
                        {
                        { _configuration["Credentials:keyName"], _configuration["Credentials:Key"] },
                        { _configuration["Credentials:HostName"], _configuration["Credentials:Host"] },
                        },
            };          
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    body = await response.Content.ReadAsStringAsync();

                }
                else
                {

                    throw new UserFriendlyException("Request Not Successful");
                }
                return body;
                //Console.WriteLine(body);
            }
            
        }

       
    }
}
