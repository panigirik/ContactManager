using ContactManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Persistence.Data;

public class ContactManagerDbContext : DbContext
{
    public ContactManagerDbContext(DbContextOptions<ContactManagerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(c => c.ContactId);
            entity.Property(c => c.MobilePhone);
            entity.Property(c => c.ContactName);
            entity.Property(c => c.JobTitle);
            entity.Property(c => c.BirthDate).IsRequired();
        });
    }
}
