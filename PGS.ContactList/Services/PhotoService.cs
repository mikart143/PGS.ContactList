using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PGS.ContactList.Database.Contexts;

namespace PGS.ContactList.Services
{
    public class PhotoService
    {
        private readonly ContactsDbContext _context;
        private readonly FileService _fileService;

        public PhotoService(FileService fileService, ContactsDbContext context)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<int> AddPhoto(IFormFile file, int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return StatusCodes.Status404NotFound;
            if (contact.PhotoString != null) return StatusCodes.Status409Conflict;
            if (!_fileService.IsImage(file.OpenReadStream())) return StatusCodes.Status406NotAcceptable;

            var fileHash = MD5.Create().ComputeHash(file.OpenReadStream());

            var fileName = Convert.ToBase64String(fileHash).Replace("/", "") + "."+file.FileName.Split(".").Last();

            contact.PhotoString = fileName;
            _context.Contacts.Update(contact);

            if (await _fileService.SaveFile(file.OpenReadStream(), fileName))
            {
                if (await _context.SaveChangesAsync() != 0) return StatusCodes.Status201Created;

                _fileService.DeleteFile(fileName);

                return StatusCodes.Status500InternalServerError;
            }


            return StatusCodes.Status500InternalServerError;

        }

        public async Task<int> UpdatePhoto(IFormFile file, int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return StatusCodes.Status404NotFound;
            if (contact.PhotoString == null) return StatusCodes.Status409Conflict;
            if (!_fileService.IsImage(file.OpenReadStream())) return StatusCodes.Status406NotAcceptable;

            var fileHash = MD5.Create().ComputeHash(file.OpenReadStream());

            var fileName = Convert.ToBase64String(fileHash).Replace("/", "") + "." + file.FileName.Split(".").Last();

            var fileNameToRemove = contact.PhotoString;

            contact.PhotoString = fileName;
            _context.Contacts.Update(contact);

            if (_fileService.DeleteFile(fileNameToRemove) && await _fileService.SaveFile(file.OpenReadStream(), fileName))
            {
                if (await _context.SaveChangesAsync() != 0) return StatusCodes.Status200OK;

                _fileService.DeleteFile(fileName);

                return StatusCodes.Status500InternalServerError;
            }


            return StatusCodes.Status500InternalServerError;
        }

        public async Task<int> DeletePhoto(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null) return StatusCodes.Status404NotFound;

            var fileName = contact.PhotoString;

            contact.PhotoString = null;
            _context.Contacts.Update(contact);

            if (_fileService.DeleteFile(fileName))
            {
                if (await _context.SaveChangesAsync() != 0) return StatusCodes.Status200OK;

                return StatusCodes.Status500InternalServerError;
            }

            return StatusCodes.Status500InternalServerError;

        }

        public async Task<FileStream> GetPhoto(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null) throw new NoNullAllowedException("Contact not found");

            var fileName = contact.PhotoString;

            return _fileService.GetFile(fileName);
        }

        public async Task<string> GetFileWebPath(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null) return null;

            return _fileService.GetFileWebPath(contact.PhotoString);
        }
    }
}