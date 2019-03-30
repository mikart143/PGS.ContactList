using Microsoft.EntityFrameworkCore;
using PGS.ContactList.Database.Models;

namespace PGS.ContactList.Database.Contexts
{
    public class ContactsDbContext:DbContext
    {
        public ContactsDbContext(DbContextOptions<ContactsDbContext> options): base(options) { }

        public DbSet<Contact> Contacts { get; set; }
    }
}
