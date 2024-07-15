using System.Collections.Concurrent;
using ContactAPI.model;

namespace ContactAPI.Repository
{

    public class ContactRepository : IContactRepository
    {
        private static readonly ConcurrentDictionary<int, Contact> ContactStorage = new();
        
        private readonly ILogger<ContactRepository> _logger;

        public static ConcurrentDictionary<int, Contact> Contacts => ContactStorage;

        public ContactRepository(ILogger<ContactRepository> logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await Task.Run(() =>
            {
                var contacts = Contacts.Values.ToList();
                return contacts;
            });
        }

        public async Task<Contact?> GetContact(int contactId)
        {
            return await Task.FromResult(Contacts.GetValueOrDefault(contactId));
        }

        public async Task<Contact> SaveContact(Contact contact)
        {
            await Task.Run(() =>
            {
                if (Contacts.ContainsKey(contact.Id))
                {
                    throw new Exception("Contact already exists");
                }

                if (!Contacts.TryAdd(contact.Id, contact))
                {
                    throw new Exception("Contact is not stored");
                }
            });

            _logger.LogInformation($"Contact added: {contact.Id}");

            return contact;
        }

        public async Task UpdateContact(Contact contact)
        {
            await Task.Run(() =>
            {
                
                Contacts[contact.Id] = contact;
            });

            _logger.LogInformation($"Contact updated: {contact.Id}");
        }

        public async Task DeleteContact(int contactId)
        {

            await Task.Run(() =>
            {
                if (!Contacts.TryGetValue(contactId, out Contact? contact))
                {
                    throw new Exception("Contact does not exist");
                }

                if (!Contacts.TryRemove(contactId, out Contact? existingContact))
                {
                    throw new Exception("Unable to delete contact");
                }
            });

            _logger.LogInformation($"Contact deleted: {contactId}");
        }
    }
}