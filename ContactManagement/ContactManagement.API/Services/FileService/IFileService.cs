using ContactManagement.API.Model;

namespace ContactManagement.API.Services.FileService
{
    public interface IFileService
    {
        public List<Contact> GetContacts();
        public Contact GetContact(long id);
        public void AddContact(Contact newContact);
        public void UpdateContact(Contact updatedContact);
        public void DeleteContact(long id);
    }
}