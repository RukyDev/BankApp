using Abp.UI;
using BankApi.Data;
using BankApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        //[HttpGet("Staffs")]
        //public IActionResult GetUser()
        //{
        //    var Users = _context.Staffs.AsQueryable();
        //    return Ok(Users);
        //}

        //[HttpPost("signUp")]
        //public IActionResult SignUp([FromBody] Staff staff)
        //{
        //    if (staff == null)
        //    {
        //        throw new UserFriendlyException("Please Input Valid Records");
        //    }
        //    else
        //    {
        //        _context.Add(staff);
        //        _context.SaveChanges();
        //        return Ok(new
        //        {
        //            StatusCode = 200,
        //            Message = "Staff Created Successfully"
        //        }) ;
        //    }
        //}

        //[HttpPost("Login")]
        //public IActionResult Login([FromBody] Staff staff)
        //{
        //    var getStaff = _context.Staffs.Where(c => c.email == staff.email && c.password == staff.password).FirstOrDefault();
        //    if (getStaff != null)
        //    {
        //        return Ok(new
        //        {
        //            StatusCode = 200,
        //            Message = "Logged in Successful"
        //        });
        //    }
        //    else
        //    {
        //        return NotFound(new
        //        {
        //            StatusCode = 404,
        //            Message = "Staff Not Found"
        //        });
        //    }
        //}
    }
}
