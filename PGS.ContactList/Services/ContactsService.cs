using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PGS.ContactList.Database.Contexts;
using PGS.ContactList.Database.Models;
using PGS.ContactList.DTO;

namespace PGS.ContactList.Services
{
    public class ContactsService
    {
        private readonly ContactsDbContext _context;
        private readonly FileService _fileService;
        private readonly IMapper _mapper;
        public ContactsService(ContactsDbContext context, FileService fileService, IMapper mapper)
        {
            _context = context;
            _fileService = fileService;
            _mapper = mapper;
        }
        public async Task<List<ContactDTO>> GetContacts()
        {
            var contactsList =  await _context.Contacts.ToListAsync();
            var response = contactsList.Select(x => _mapper.Map<ContactDTO>(x)).ToList();
            return response;
        }

        public async Task<ContactDTO> GetContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            var response = _mapper.Map<ContactDTO>(contact);
            return response;
        }

        public async Task<int> DeleteContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contact);

            if (await _context.SaveChangesAsync() != 0)
            {
                if (_fileService.DeleteFile(contact.PhotoString)) return StatusCodes.Status200OK;

                return StatusCodes.Status500InternalServerError;
            }

            return StatusCodes.Status500InternalServerError;
        }

        

        public async Task<int> AddContact(ContactPostDTO contactPost)
        {
            var contact = _mapper.Map<Contact>(contactPost);

            await _context.Contacts.AddAsync(contact);

            return await _context.SaveChangesAsync() != 0 ? StatusCodes.Status201Created: StatusCodes.Status500InternalServerError;
        }

        public async Task<int> UpdateContact(int id, ContactPutDTO contactPut)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null) return StatusCodes.Status404NotFound;

            _mapper.Map<ContactPutDTO, Contact>(contactPut, contact);

            _context.Contacts.Update(contact);

            return  await  _context.SaveChangesAsync() != 0 ? StatusCodes.Status200OK : StatusCodes.Status500InternalServerError;
        }
    }
}
