using ContactAPI.model;
using ContactAPI.Repository;

namespace ContactAPI.Services
{
    public class ContactService : IContactService
    {

        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IEnumerable<Contact>> GetContacts()
        {
            return await _contactRepository.GetAllContacts();
        }

        public async Task<Contact?>? GetContact(int id)
        {
            return await _contactRepository.GetContact(id)!;
        }

        public async Task<Contact> AddContact(Contact contact)
        {
            return await _contactRepository.SaveContact(contact);
        }

        public async Task DeleteContact(int contactID)
        {
            await _contactRepository.DeleteContact(contactID);
        }

        public async Task UpdateContact(Contact contact)
        {
            await _contactRepository.UpdateContact(contact);
        }
    }
}