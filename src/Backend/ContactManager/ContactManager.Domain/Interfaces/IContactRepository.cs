using ContactManager.Domain.Entities;

namespace ContactManager.Domain.Interfaces;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> GetAllAsync();
    Task<Contact?> GetByIdAsync(Guid id);

    Task<Contact> GetContactByNumber(string number);
    Task AddAsync(Contact contact);
    Task UpdateAsync(Contact contact);
    Task DeleteAsync(Contact contact);
}