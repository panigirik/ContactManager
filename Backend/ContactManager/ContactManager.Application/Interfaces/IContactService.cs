using ContactManager.Application.DTOs;
using ContactManager.Domain.Entities;

namespace ContactManager.Application.Interfaces;

public interface IContactService
{
    Task<IEnumerable<ContactDto>> GetAllContactsAsync();
    Task<ContactDto?> GetContactByIdAsync(Guid contactId);
    
    Task<ContactDto> GetContactByNumber(string number);
    Task CreateContactAsync(ContactDto contactDto);
    Task UpdateContactAsync(ContactDto contactDto);
    Task DeleteContactAsync(Guid contactId);
}