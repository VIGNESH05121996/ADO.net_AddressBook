using System;

namespace ADO.netAddressBook
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welccome To ADO.netAddressBook");
            AddressBookService addressBook = new AddressBookService();
            addressBook.CreateTable();
            Contact contact = new Contact();
            contact.FirstName = "Praveen";
            contact.LastName = "Muthu";
            contact.Address = "HAPP";
            contact.City = "Trichy";
            contact.State = "TamilNadu";
            contact.Zip = 620025;
            contact.PhoneNumber = 9625478231;
            contact.Email = "praveen@gmail.com";
            addressBook.InsertNewContact(contact);
        }
    }
}
