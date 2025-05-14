using AutoMapper;
using ContactManager.Application.DTOs;
using ContactManager.Application.Interfaces;
using ContactManager.Domain.Entities;
using ContactManager.Domain.Interfaces;

namespace ContactManager.Application.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;
    
    public ContactService(IContactRepository contactRepository, IMapper mapper)
    {
        _contactRepository = contactRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ContactDto>> GetAllContactsAsync()
    {
        var contacts = await _contactRepository.GetAllAsync();

        foreach (var contact in contacts)
        {
            contact.BirthDate = DateTime.SpecifyKind(contact.BirthDate, DateTimeKind.Utc);
        }

        return _mapper.Map<IEnumerable<ContactDto>>(contacts);
    }


    public async Task<ContactDto?> GetContactByIdAsync(Guid contactId)
    {
        var contactdto =  await _contactRepository.GetByIdAsync(contactId);
        return _mapper.Map<ContactDto>(contactdto);
    }

    public async Task<ContactDto> GetContactByNumber(string number)
    {
        var contact = await _contactRepository.GetContactByNumber(number);
        return _mapper.Map<ContactDto>(contact);
    }

    public async Task CreateContactAsync(ContactDto contactDto)
    {
        var contact = _mapper.Map<Contact>(contactDto);
        contact.BirthDate = DateTime.SpecifyKind(contact.BirthDate, DateTimeKind.Utc);
        await _contactRepository.AddAsync(contact);
    }

    public async Task UpdateContactAsync(ContactDto contactDto)
    {
        var contact = _mapper.Map<Contact>(contactDto);
        contact.BirthDate = DateTime.SpecifyKind(contact.BirthDate, DateTimeKind.Utc);
        await _contactRepository.UpdateAsync(contact);
    }

    public async Task DeleteContactAsync(Guid contactId)
    {
        var contact = await _contactRepository.GetByIdAsync(contactId);
        if (contact == null)
        {
            throw new KeyNotFoundException($"Contact with ID {contactId} was not found.");
        }

        await _contactRepository.DeleteAsync(contact);
    }

}