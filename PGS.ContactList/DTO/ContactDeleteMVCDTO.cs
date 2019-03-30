using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PGS.ContactList.DTO
{
    public class ContactDeleteMVCDTO
    {
        public int ContactId { get; set; }


        public string Name { get; set; }


        public string Surname { get; set; }


        public string Number { get; set; }


        public string Email { get; set; }


        public string PhotoString { get; set; }
    }
}
