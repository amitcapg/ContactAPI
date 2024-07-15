using ContactAPI.model;

namespace ContactAPI.Repository
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact?> GetContact(int contactId);
        Task<Contact> SaveContact(Contact contact);
        Task UpdateContact(Contact contact);
        Task DeleteContact(int contactId);
    }
}