using ContactManager.Domain.Entities;
using ContactManager.Domain.Interfaces;
using ContactManager.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Persistence.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly ContactManagerDbContext _dbContext;

    public ContactRepository(ContactManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Contact>> GetAllAsync()
    {
        return await _dbContext.Contacts.ToListAsync();
    }

    public async Task<Contact?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Contacts.FindAsync(id);
    }

    public async Task<Contact> GetContactByNumber(string number)
    {
        return await _dbContext.Contacts.FirstOrDefaultAsync(c => c.MobilePhone == number);
    }
    
    public async Task AddAsync(Contact contact)
    {
        await _dbContext.Contacts.AddAsync(contact);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Contact contact)
    {
        _dbContext.Contacts.Update(contact);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Contact contact)
    {
        _dbContext.Contacts.Remove(contact);
        await _dbContext.SaveChangesAsync();
    }
}