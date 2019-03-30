using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PGS.ContactList.Database.Contexts;
using PGS.ContactList.Database.Models;
using PGS.ContactList.DTO;
using PGS.ContactList.Models;
using PGS.ContactList.Services;

namespace PGS.ContactList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContactsDbContext _context;
        private readonly PhotoService _photoService;
        private readonly IMapper _mapper;
        private readonly FileService _fileService;
        public HomeController(ContactsDbContext context, PhotoService photoService, IMapper mapper, FileService fileService)
        {
            _context = context;
            _photoService = photoService;
            _mapper = mapper;
            _fileService = fileService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Contacts()
        {
            var dbResponse = await _context.Contacts.ToListAsync();
            var response = dbResponse.Select(x =>
            {
                var z = _mapper.Map<ContactDTO>(x);
                if (z.PhotoString == null) z.PhotoString = "default.png";
                z.PhotoString = _fileService.GetFileWebPath(z.PhotoString);
                return z;
            }).ToList();
            return View(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ReDoc()
        {
            return new RedirectResult("~/redoc");
        }


        public IActionResult Swagger()
        {
            return new RedirectResult("~/swagger");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(500);
            }

            Contact contact = _context.Contacts.Find(id);

            if (contact == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<ContactDTO>(contact);
            if (contact.PhotoString == null) response.PhotoString = "default.png";

            response.PhotoString = _fileService.GetFileWebPath(response.PhotoString);
            return View(response);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name, Surname, Number, Email")] ContactDTO contact, IFormFile file, int id)
        {
            if (ModelState.IsValid)
            {
                var insertedContact = _mapper.Map<Contact>(contact);
                insertedContact.ContactId = id;
                _context.Contacts.Update(insertedContact);

                if(file != null) _fileService.DeleteFile(_context.Contacts.Find(id).PhotoString);
                int result;
                if (await _context.SaveChangesAsync() != 0)
                {
                    if (file != null)
                    {
                        result = await _photoService.AddPhoto(file, insertedContact.ContactId);
                        if (result == 201)
                        {
                            return RedirectToAction(nameof(Contacts));
                        }

                        _context.Contacts.Remove(insertedContact);

                        _fileService.DeleteFile(_context.Contacts.Find(id).PhotoString);

                        await _context.SaveChangesAsync();

                        
                    }
                    return RedirectToAction(nameof(Contacts));
                }

                return StatusCode(500);
            }
            return View(contact);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Surname, Number, Email")] ContactPostDTO contact, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var insertedContact = _mapper.Map<Contact>(contact);
                _context.Contacts.Add(insertedContact);

                int result;
                if (await _context.SaveChangesAsync() != 0)
                {
                    if (file != null)
                    {
                        result = await _photoService.AddPhoto(file, insertedContact.ContactId);
                        if (result == 201)
                        {
                            return RedirectToAction(nameof(Contacts));
                        }
                        _context.Contacts.Remove(insertedContact);

                        await _context.SaveChangesAsync();

                        
                    }
                    return RedirectToAction(nameof(Contacts));
                }

                return StatusCode(500);
            }
            return View(contact);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(500);
            }

            Contact contact = _context.Contacts.Find(id);

            if (contact == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<ContactDetailsMVCDTO>(contact);

            if (contact.PhotoString == null) response.PhotoString = "default.png";

            response.PhotoString = _fileService.GetFileWebPath(response.PhotoString);
            return View(response);
        }

        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new StatusCodeResult(500);
            }

            Contact contact = _context.Contacts.Find(id);

            if (contact == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<ContactDeleteMVCDTO>(contact);

            if (contact.PhotoString == null) response.PhotoString = "default.png";

            response.PhotoString = _fileService.GetFileWebPath(response.PhotoString);
            return View(response);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contacts.SingleOrDefaultAsync(m => m.ContactId == id);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            if(contact.PhotoString != null) await _photoService.DeletePhoto(id);
            return RedirectToAction("Contacts");
        }
    }
}
