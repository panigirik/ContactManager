namespace ContactManager.Application.DTOs;

public class ContactDto
{
    public Guid ContactId { get; set; }
    
    public string? MobilePhone { get; set; }
    
    public string? ContactName { get; set; }
    
    public string? JobTitle { get; set; }

    public DateTime BirthDate { get; set; } = DateTime  .UtcNow;
}