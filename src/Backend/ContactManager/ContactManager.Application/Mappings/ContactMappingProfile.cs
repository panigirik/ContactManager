using AutoMapper;
using ContactManager.Application.DTOs;
using ContactManager.Domain.Entities;

namespace ContactManager.Application.Mappings;

public class ContactMappingProfile: Profile
{
    public ContactMappingProfile()
    {
        CreateMap<ContactDto, Contact>().ForMember(dest => dest.BirthDate,
            opt => opt.MapFrom(src => DateTime.SpecifyKind(src.BirthDate, DateTimeKind.Utc)));
        CreateMap<Contact, ContactDto>().ReverseMap();
    }
}