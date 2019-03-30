using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PGS.ContactList.DTO
{
    public class ContactPostDTO
    {

        
        [StringLength(32, ErrorMessage = "To long")]
        public string Name { get; set; }


        [StringLength(32, ErrorMessage = "Too long")]
        public string Surname { get; set; }

        [RegularExpression("(?<!\\w)(\\(?(\\+|00)?48\\)?)?[ -]?\\d{3}[ -]?\\d{3}[ -]?\\d{3}(?!\\w)", ErrorMessage = "The phone number is not valid")]
        [StringLength(32)]
        public string Number { get; set; }


        [EmailAddress(ErrorMessage = "Email address is not valid")]
        [StringLength(32, ErrorMessage = "Email is to long")]
        public string Email { get; set; }

    }
}
