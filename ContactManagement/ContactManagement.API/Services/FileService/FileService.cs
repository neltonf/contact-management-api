

using ContactManagement.API.Model;
using System.Text.Json;

namespace ContactManagement.API.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly string _filePath;

        public FileService()
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "data.json");
            InitializeFile();
        }

        public void AddContact(Contact newContact)
        {
            if (!File.Exists(_filePath)) InitializeFile();

            var jsonString = File.ReadAllText(_filePath);

            var contacts = string.IsNullOrWhiteSpace(jsonString)
                ? new List<Contact>()
                : JsonSerializer.Deserialize<List<Contact>>(jsonString) ?? new List<Contact>();

            var nextId = contacts.Count > 0 ? contacts.Max(c => c.Id) + 1 : 1;
            newContact.Id = nextId;

            contacts.Add(newContact);

            var updatedJsonString = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(_filePath, updatedJsonString);
        }

        public void DeleteContact(long id)
        {
            if (!File.Exists(_filePath)) InitializeFile();

            var jsonString = File.ReadAllText(_filePath);

            var contacts = string.IsNullOrWhiteSpace(jsonString)
                ? new List<Contact>()
                : JsonSerializer.Deserialize<List<Contact>>(jsonString) ?? new List<Contact>();

            var contactToRemove = contacts.FirstOrDefault(c => c.Id == id);

            if (contactToRemove != null)
            {

                contacts.Remove(contactToRemove);

                var updatedJsonString = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(_filePath, updatedJsonString);
            }
        }

        public Contact GetContact(long id)
        {
            if (!File.Exists(_filePath))
            {
                InitializeFile();
                return null;
            }

            var jsonString = File.ReadAllText(_filePath);

            var contacts = string.IsNullOrWhiteSpace(jsonString)
                ? new List<Contact>()
                : JsonSerializer.Deserialize<List<Contact>>(jsonString) ?? new List<Contact>();

            var contact = contacts.FirstOrDefault(c => c.Id == id);

            return contact;
        }

        public List<Contact> GetContacts()
        {
            if (!File.Exists(_filePath)) InitializeFile();

            var jsonString = File.ReadAllText(_filePath);

            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return new List<Contact>();
            }

            return JsonSerializer.Deserialize<List<Contact>>(jsonString) ?? new List<Contact>();
        }

        public void UpdateContact(Contact updatedContact)
        {
            if (!File.Exists(_filePath)) InitializeFile();

            var jsonString = File.ReadAllText(_filePath);

            var contacts = string.IsNullOrWhiteSpace(jsonString)
                ? new List<Contact>()
                : JsonSerializer.Deserialize<List<Contact>>(jsonString) ?? new List<Contact>();

            var contactToUpdate = contacts.FirstOrDefault(c => c.Id == updatedContact.Id);
            if (contactToUpdate != null)
            {
                contactToUpdate.FirstName = updatedContact.FirstName;
                contactToUpdate.LastName = updatedContact.LastName;
                contactToUpdate.Email = updatedContact.Email;

                var updatedJsonString = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(_filePath, updatedJsonString);
            }
        }

        private void InitializeFile()
        {
            if (!File.Exists(_filePath))
            {
                using FileStream fs = File.Create(_filePath);
            }
        }
    }
}
