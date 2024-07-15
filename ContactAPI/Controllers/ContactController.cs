using ContactAPI.model;
using ContactAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult> GetContacts()
        {
            var allContacts = (await _contactService.GetContacts()).ToList();

            return Ok(allContacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetContactById(int id)
        {
            if (id <= 0)
                return BadRequest("Id should be greater than 0");

            var contact = await _contactService.GetContact(id)!;

            if (contact is null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public async Task<ActionResult> AddContact([FromBody] Contact addContactRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var addedContact = await _contactService.AddContact(addContactRequest);

            if (addedContact != null)
            {
                return CreatedAtAction(nameof(GetContactById),
                    new { id = addedContact.Id },
                    addedContact);
            }

            return Problem("Failed to insert the contact");
        }


        [HttpPut]
        public async Task<ActionResult> UpdateContact(Contact updateContactRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var contact = _contactService.GetContact(updateContactRequest.Id);

            if (contact is null)
            {
                return NotFound();
            }

            await _contactService.UpdateContact(updateContactRequest);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            if (id == 0)
                return BadRequest("Id should be an integer value greater than 0");

            await _contactService.DeleteContact(id);

            return Ok();
        }
    }
}