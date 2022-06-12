using Abp.UI;
using BankApi.Data;
using BankApi.Interfaces;
using BankApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   

    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAccountRepository _accountRepository;

        public CustomerController(AppDbContext context, IAccountRepository accountRepository)
        {
            _context = context;
            _accountRepository = accountRepository;
        }

        [HttpPost("CreateCustomer")]

        public IActionResult CreateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new UserFriendlyException("Please Input Valid Record");

            }
            else
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Customer Created Successfully"
                });
            }

        }

        [HttpPut("UpdateCustomer")]
        public IActionResult UpdateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new UserFriendlyException("Please Input Valid Record");
            }
            else
            {
                var getCustomer = _context.Customers.AsNoTracking().FirstOrDefault(c=>c.Id == customer.Id);
                if (getCustomer == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Customer Not Found"
                    });
                }
                else
                {
                    _context.Entry(customer.AccountBalance).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Customer Updated"
                    });
                }
            }
         }

        [HttpDelete("DeletCustomer/{id}")]
        public IActionResult DeleteCustomer(Customer customer)
        {
            if (true)
            {
                if (customer == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Customer Not Found"
                    });

                }
                else
                {
                    var getCustomer = _context.Customers.Find(customer.Id);
                    if (getCustomer == null)
                    {
                        return NotFound(new
                        {
                            StatusCode = 404,
                            Message = "Customer Not Found"
                        });
                    }
                    else
                    {
                        _context.Remove(getCustomer);
                        _context.SaveChanges();
                        return Ok(new
                        {
                            StatusCode = 200,
                            Message = "Customer Deleted Successfully"
                        });
                    }
                }
            }
        }

        [HttpGet("GetAllCustomer")]
        public IActionResult GetallCustomer( )
        {
            var CustomerList = _context.Customers.AsQueryable();
            return Ok(new
            {
                StatusCode = 200,
                CustomerDetails = CustomerList
            });
        }

        [HttpGet("GetCustomerById/{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var getCustomer = _context.Customers.Find(id);
            if (getCustomer == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Customer Not Found"
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    CustomerDetails = getCustomer
                });
            }
        }
       
        [HttpGet("GetGoldPrice")]
        public async Task<string> GetGoldPrice()
        {
            var items = await _accountRepository.GetGoldPrice();
            if (items == null)
            {
                throw new UserFriendlyException("Request Not Successfull");
            }
            else
            {
                return items;
            }
        }
    }
}
