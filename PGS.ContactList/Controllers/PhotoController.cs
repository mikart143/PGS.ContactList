using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using PGS.ContactList.DTO;
using PGS.ContactList.Services;

namespace PGS.ContactList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("multipart/form-data")]
    public class PhotoController : ControllerBase
    {
        private readonly PhotoService _service;
        public PhotoController(PhotoService service)
        {
            _service = service;
        }

        [HttpPost("{id}")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(void))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void))]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void))]
        [SwaggerResponse(HttpStatusCode.Conflict, typeof(void))]
        [SwaggerResponse(HttpStatusCode.NotAcceptable, typeof(void))]
        public async Task<IActionResult> PostPhoto([FromForm] IFormFile file, int id)
        {
            var response = await _service.AddPhoto(file, id);

            return StatusCode(response);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(void))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void))]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void))]
        [SwaggerResponse(HttpStatusCode.Conflict, typeof(void))]
        [SwaggerResponse(HttpStatusCode.NotAcceptable, typeof(void))]
        public async Task<IActionResult> PutPhoto([FromForm] IFormFile file, int id)
        {
            var response = await _service.UpdatePhoto(file, id);

            return StatusCode(response);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(void))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void))]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void))]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeletePhoto(id);

            return StatusCode(response);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(File))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void))]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void))]
        public async Task<IActionResult> Get(int id){
            
            FileStream responseFile = await _service.GetPhoto(id);

            if (responseFile == null) return StatusCode(404);

            var fileName = responseFile.Name.Split("\\").Last();

            return File(responseFile, "application/octet-stream", fileName);

        }

        [HttpGet("{id}/path")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(File))]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void))]
        public async Task<IActionResult> GetPath(int id)
        {

            var response =  await _service.GetFileWebPath(id);

            return response == null ? (IActionResult) StatusCode(404) : StatusCode(200, new PhotoPath() {Path = response});

        }
    }
}