using BasicCrud.Data;
using BasicCrud.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasicCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsAPIDBContext dbContext;

        public ContactsController(ContactsAPIDBContext dbContext) {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public  IActionResult GetAllContacts () {
            return Ok( dbContext.Contacts.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> AddContacts (AddContactRequest addContactRequest) {
            var contacts = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                Phone = addContactRequest.Phone,
                FullName = addContactRequest.FullName,
            };

            await dbContext.Contacts.AddAsync(contacts);
            await dbContext.SaveChangesAsync();
        
            return Ok(contacts);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContact updateContact) 
        {
            var contact =  dbContext.Contacts.Find(id);

            if (contact != null) { 
                contact.Email = updateContact.Email;
                contact.Phone = updateContact.Phone;
                contact.FullName = updateContact.FullName;

                await dbContext.SaveChangesAsync();

                return Ok(contact);
            }


            return NotFound();
            
        }
    }
}
