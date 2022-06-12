using System.ComponentModel.DataAnnotations.Schema;

namespace BankApi.Models
{
    [Table("tbl_Customers")]
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }    
        public string LastName { get; set; }
        public string PhoneNo { get; set; } 
        public string Email { get; set; }   
        public string AccountNumber { get; set; }
        public string AccountBalance { get; set; }  

    }
}
