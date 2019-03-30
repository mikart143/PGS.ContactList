using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PGS.ContactList.Database.Models
{
    public class Contact
    {

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        [StringLength(32)]
        public string Surname { get; set; }

        [Required]
        [StringLength(12)]
        public string Number { get; set; }

        [Required]
        [StringLength(32)]
        public string Email { get; set; }


        public string PhotoString { get; set; }
    }
}
