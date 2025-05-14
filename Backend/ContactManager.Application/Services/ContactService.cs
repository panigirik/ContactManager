using AutoMapper;
using ContactManager.Application.DTOs;
using ContactManager.Application.Interfaces;
using ContactManager.Domain.Entities;
using ContactManager.Domain.Interfaces;
using ExHandler.Exceptions.ClientSideExceptions;

namespace ContactManager.Application.Services
{
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

            if (contacts == null)
            {
                throw new KeyNotFoundException("No contacts found.");
            }

            var updatedContacts = contacts.Select(contact => 
            {
                contact.BirthDate = DateTime.SpecifyKind(contact.BirthDate, DateTimeKind.Utc);
                return contact;
            }).ToList();

            return _mapper.Map<IEnumerable<ContactDto>>(updatedContacts);
        }

        public async Task<ContactDto?> GetContactByIdAsync(Guid contactId)
        {
            if (contactId == Guid.Empty)
            {
                throw new BadRequestException("Invalid contact ID.", nameof(GetContactByIdAsync));
            }

            var contact = await _contactRepository.GetByIdAsync(contactId);

            if (contact == null)
            {
                throw new NotFoundException(nameof(GetContactByIdAsync), contactId);
            }

            return _mapper.Map<ContactDto>(contact);
        }

        public async Task<ContactDto> GetContactByNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                throw new BadRequestException("Contact number cannot be null or empty.", nameof(number));
            }

            var contact = await _contactRepository.GetContactByNumber(number);

            if (contact == null)
            {
                throw new NotFoundException(nameof(GetContactByNumber), number);
            }

            return _mapper.Map<ContactDto>(contact);
        }

        public async Task CreateContactAsync(ContactDto contactDto)
        {
            if (contactDto == null)
            {
                throw new BadRequestException("Contact data is invalid.", nameof(contactDto));
            }

            var contact = _mapper.Map<Contact>(contactDto);
            contact.BirthDate = DateTime.SpecifyKind(contact.BirthDate, DateTimeKind.Utc);

            if (string.IsNullOrEmpty(contact.ContactName))
            {
                throw new BadRequestException("Contact name is required.", nameof(contactDto));
            }

            await _contactRepository.AddAsync(contact);
        }

        public async Task UpdateContactAsync(ContactDto contactDto)
        {
            if (contactDto == null || contactDto.ContactId == Guid.Empty)
            {
                throw new BadRequestException("Invalid contact data", nameof(contactDto));
            }

            var existingContact = await _contactRepository.GetByIdAsync(contactDto.ContactId);

            if (existingContact == null)
            {
                throw new NotFoundException(nameof(UpdateContactAsync), contactDto.ContactId);
            }

            var contact = _mapper.Map<Contact>(contactDto);
            contact.BirthDate = DateTime.SpecifyKind(contact.BirthDate, DateTimeKind.Utc);
            
            await _contactRepository.UpdateAsync(contact);
        }

        public async Task DeleteContactAsync(Guid contactId)
        {
            if (contactId == Guid.Empty)
            {
                throw new BadRequestException("Invalid contact ID.", nameof(contactId));
            }

            var contact = await _contactRepository.GetByIdAsync(contactId);

            if (contact == null)
            {
                throw new NotFoundException(nameof(DeleteContactAsync), contactId);
            }

            await _contactRepository.DeleteAsync(contact);
        }
    }
}
