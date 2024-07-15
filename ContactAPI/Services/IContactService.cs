using ContactAPI.model;

namespace ContactAPI.Services
{
    public interface IContactService
    {
        Task DeleteContact(int contactId);
        Task<Contact?>? GetContact(int id);
        Task<IEnumerable<Contact>> GetContacts();
        Task<Contact> AddContact(Contact contact);
        Task UpdateContact(Contact contact);
    }
}