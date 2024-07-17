using ContactAPI.model;
using ContactAPI.Repository;
using Microsoft.Extensions.Logging;
using Moq;

namespace ContactWebAPI.Tests
{
    public class ContactRepositoryTests
    {
        [Fact]
        public async Task GetAllContacts_ShouldReturnAllContacts()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ContactRepository>>();
            var contactRepository = new ContactRepository(mockLogger.Object);
            var contact1 = new Contact { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };
            ContactRepository.Contacts.TryAdd(contact1.Id, contact1);

            // Act
            var result = await contactRepository.GetAllContacts();

            // Assert
            Assert.Equal(contact1.Name, result.First().Name);
            Assert.Equal(contact1.Email, result.First().Email);
            Assert.Equal(contact1.Id, result.First().Id);
        }

        [Fact]
        public async Task GetContact_ExistingContactId_ShouldReturnContact()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ContactRepository>>();
            var contactRepository = new ContactRepository(mockLogger.Object);
            var contact = new Contact { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };
            ContactRepository.Contacts.TryAdd(contact.Id, contact);

            // Act
            var result = await contactRepository.GetContact(contact.Id);

            // Assert
            Assert.Equal(contact.Name, result!.Name);
            Assert.Equal(contact.Email, result.Email);
            Assert.Equal(contact.Id, result.Id);
        }

        [Fact]
        public async Task GetContact_NonExistingContactId_ShouldReturnNull()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ContactRepository>>();
            var contactRepository = new ContactRepository(mockLogger.Object);

            // Act
            var result = await contactRepository.GetContact(2);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task SaveContact_NewContact_ShouldAddContact()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ContactRepository>>();
            var contactRepository = new ContactRepository(mockLogger.Object);
            var contact = new Contact { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };

            if (!ContactRepository.Contacts.IsEmpty)
            {
                ContactRepository.Contacts.Clear();
            }

            // Act
            var savedContact = await contactRepository.SaveContact(contact!);

            // Assert
            Assert.Equal(contact, savedContact);
        }

        [Fact]
        public async Task SaveContact_ExistingContact_ShouldThrowException()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ContactRepository>>();
            var contactRepository = new ContactRepository(mockLogger.Object);
            var contact = new Contact { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };
            ContactRepository.Contacts.TryAdd(contact.Id, contact);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => contactRepository.SaveContact(contact));
        }

        [Fact]
        public async Task UpdateContact_ExistingContact_ShouldUpdateContact()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ContactRepository>>();
            var contactRepository = new ContactRepository(mockLogger.Object);
            var contact = new Contact { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };
            ContactRepository.Contacts.TryAdd(contact.Id, contact);
            var updatedContact = new Contact { Id = 1, Name = "John Smith", Email = "john.smith@example.com" };

            // Act
            await contactRepository.UpdateContact(updatedContact);

            // Assert
            Assert.Equal(updatedContact, ContactRepository.Contacts[contact.Id]);
        }

        [Fact]
        public async Task DeleteContact_ExistingContactId_ShouldDeleteContact()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ContactRepository>>();
            var contactRepository = new ContactRepository(mockLogger.Object);
            var contact = new Contact { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };
            ContactRepository.Contacts.TryAdd(contact.Id, contact);

            // Act
            await contactRepository.DeleteContact(contact.Id);

            // Assert
            Assert.Empty(ContactRepository.Contacts);
        }

        [Fact]
        public async Task DeleteContact_NonExistingContactId_ShouldThrowException()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ContactRepository>>();
            var contactRepository = new ContactRepository(mockLogger.Object);

            if (ContactRepository.Contacts.Count() > 0)
            {
                ContactRepository.Contacts.Clear();
            }

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => contactRepository.DeleteContact(1));
        }
    }
}