using BasicCrud.Model;
using Microsoft.EntityFrameworkCore;

namespace BasicCrud.Data
{
    public class ContactsAPIDBContext : DbContext
    {
        public ContactsAPIDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }

    }
}
