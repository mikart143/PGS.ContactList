using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PGS.ContactList.Database.Models;
using PGS.ContactList.DTO;

namespace PGS.ContactList.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, ContactPostDTO>()
                .ForMember(x => x.Surname, map => map.MapFrom(x => x.Surname))
                .ForMember(x => x.Email, map => map.MapFrom(x => x.Email))
                .ForMember(x => x.Name, map => map.MapFrom(x => x.Name))
                .ForMember(x => x.Number, map => map.MapFrom(x => x.Number));
            CreateMap<ContactPostDTO, Contact>()
                .ForMember(x => x.Surname, map => map.MapFrom(x => x.Surname))
                .ForMember(x => x.Email, map => map.MapFrom(x => x.Email))
                .ForMember(x => x.Name, map => map.MapFrom(x => x.Name))
                .ForMember(x => x.Number, map => map.MapFrom(x => x.Number));

            CreateMap<Contact, ContactPutDTO>()
                .ForMember(x => x.Surname, map => map.MapFrom(x => x.Surname))
                .ForMember(x => x.Email, map => map.MapFrom(x => x.Email))
                .ForMember(x => x.Name, map => map.MapFrom(x => x.Name))
                .ForMember(x => x.Number, map => map.MapFrom(x => x.Number));
            CreateMap<ContactPutDTO, Contact>()
                .ForMember(x => x.Surname, map => map.MapFrom(x => x.Surname))
                .ForMember(x => x.Email, map => map.MapFrom(x => x.Email))
                .ForMember(x => x.Name, map => map.MapFrom(x => x.Name))
                .ForMember(x => x.Number, map => map.MapFrom(x => x.Number));

            CreateMap<Contact, ContactDeleteMVCDTO>()
                .ForMember(x => x.Surname, map => map.MapFrom(x => x.Surname))
                .ForMember(x => x.Email, map => map.MapFrom(x => x.Email))
                .ForMember(x => x.Name, map => map.MapFrom(x => x.Name))
                .ForMember(x => x.Number, map => map.MapFrom(x => x.Number))
                .ForMember(x => x.PhotoString, map => map.MapFrom(x => x.PhotoString))
                .ForMember(x => x.ContactId, map => map.MapFrom(x => x.ContactId));
            CreateMap<ContactDeleteMVCDTO, Contact>()
                .ForMember(x => x.Surname, map => map.MapFrom(x => x.Surname))
                .ForMember(x => x.Email, map => map.MapFrom(x => x.Email))
                .ForMember(x => x.Name, map => map.MapFrom(x => x.Name))
                .ForMember(x => x.Number, map => map.MapFrom(x => x.Number))
                .ForMember(x => x.PhotoString, map => map.MapFrom(x => x.PhotoString))
                .ForMember(x => x.ContactId, map => map.MapFrom(x => x.ContactId));

            CreateMap<Contact, ContactDetailsMVCDTO>()
                .ForMember(x => x.Surname, map => map.MapFrom(x => x.Surname))
                .ForMember(x => x.Email, map => map.MapFrom(x => x.Email))
                .ForMember(x => x.Name, map => map.MapFrom(x => x.Name))
                .ForMember(x => x.Number, map => map.MapFrom(x => x.Number))
                .ForMember(x => x.PhotoString, map => map.MapFrom(x => x.PhotoString))
                .ForMember(x => x.ContactId, map => map.MapFrom(x => x.ContactId));
            CreateMap<ContactDetailsMVCDTO, Contact>()
                .ForMember(x => x.Surname, map => map.MapFrom(x => x.Surname))
                .ForMember(x => x.Email, map => map.MapFrom(x => x.Email))
                .ForMember(x => x.Name, map => map.MapFrom(x => x.Name))
                .ForMember(x => x.Number, map => map.MapFrom(x => x.Number))
                .ForMember(x => x.PhotoString, map => map.MapFrom(x => x.PhotoString))
                .ForMember(x => x.ContactId, map => map.MapFrom(x => x.ContactId));
        }
    }
}
