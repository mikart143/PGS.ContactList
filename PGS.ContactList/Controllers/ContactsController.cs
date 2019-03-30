using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using PGS.ContactList.DTO;
using PGS.ContactList.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PGS.ContactList
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ContactsController : ControllerBase
    {

        private readonly ContactsService _service;

        public ContactsController(ContactsService service)
        {

            _service = service;
        }
        // GET: api/<controller>
        [HttpGet]
        [EnableQuery()]
        [SwaggerResponse(HttpStatusCode.OK, typeof(List<ContactDTO>))]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void))]
        public async Task<IActionResult> Get()
        {
            var contactsList = await _service.GetContacts();
            return contactsList.Count == 0 ? (IActionResult) StatusCode(404) : StatusCode(200, contactsList);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [EnableQuery()]
        [SwaggerResponse(HttpStatusCode.OK, typeof(ContactDTO))]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void))]
        public async Task<IActionResult> Get(int id)
        {
            var contact = await _service.GetContact(id);
            return contact == null? (IActionResult) StatusCode(404): StatusCode(200, contact);
        }

        // POST api/<controller>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, typeof(void))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void))]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void))]
        public async Task<IActionResult> Post([FromBody]ContactPostDTO contact)
        {
            var response =  await _service.AddContact(contact);

            return StatusCode(response);
        }



        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(void))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void))]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void))]
        public async Task<IActionResult> Put(int id, [FromBody]ContactPutDTO contact)
        {
            var response = await _service.UpdateContact(id, contact);

            return StatusCode(response);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(void))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void))]
        public async Task<IActionResult> Delete(int id)
        {
            var response =  await _service.DeleteContact(id);

            return StatusCode(response);
        }
    }
}
