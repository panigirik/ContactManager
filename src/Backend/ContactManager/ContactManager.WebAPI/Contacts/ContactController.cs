using ContactManager.Application.DTOs;
using ContactManager.Application.Interfaces;
using ContactManager.Domain.Entities;
using ContactManager.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Contacts;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;
    private readonly IContactRepository _contactRepository;

    public ContactController(IContactService contactService, IContactRepository contactRepository)
    {
        _contactService = contactService;
        _contactRepository = contactRepository;
    }

    /// <summary>
    /// Получить все контакты.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactDto>>> GetAllContacts()
    {
        var contacts = await _contactService.GetAllContactsAsync();
        return Ok(contacts);
    }

    /// <summary>
    /// Получить контакт по ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ContactDto>> GetContactById(Guid id)
    {
        var contact = await _contactService.GetContactByIdAsync(id);
        if (contact == null)
            return NotFound();

        return Ok(contact);
    }

    /// <summary>
    /// Создать новый контакт.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateContact([FromBody] ContactDto contactDto)
    {
        await _contactService.CreateContactAsync(contactDto);
        return NoContent();
    }

    [HttpGet("number/{number}")]
    public async Task<IActionResult> GetContactByNumber(string number)
    {
        var contact = await _contactService.GetContactByNumber(number);
        return Ok(contact);
    }


    /// <summary>
    /// Обновить контакт.
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> UpdateContact([FromBody] ContactDto contactDto)
    {
        await _contactService.UpdateContactAsync(contactDto);
        return NoContent();
    }



    /// <summary>
    /// Удалить контакт.
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteContact(Guid contactId)
    {
        await _contactService.DeleteContactAsync(contactId);
        return NoContent();
    }

}
