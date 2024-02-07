using PhoneBookApp.Model;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PhoneBookApp.Service
{
    public class PhoneBookService
    {
        public PhoneBookService() { }
        List<PhoneBoolModel> bookModels = new List<PhoneBoolModel>();
        public void PhoneBookDetails()
        {
            while (true)
            {
                string section;
                Console.Write("Enter Section [A-Z] (type q to quit):-");
                section = Console.ReadLine().ToUpper();

                if (section == "Q")
                {
                    Environment.Exit(0);
                }

                if (section.Length != 1 || !char.IsLetter(section[0]))
                {
                    Console.WriteLine("Invalid input. Please enter a single letter from A-Z.");
                    continue;
                }

                PhoneBoolModel phoneBook = new PhoneBoolModel();

                phoneBook.Section = section;

                int options = PhoneBookOptions(section);

                if (options == 1)
                {
                    Console.WriteLine($"Option : {options}");
                    Console.Write("Enter Contact Name :-");
                    phoneBook.Name = Console.ReadLine();
                    AddContact(phoneBook, section);
                }

                else if (options == 2)
                {
                    int enteredSerialNumber;
                    Console.Write($"Enter your Serial Number (SNo):-");
                    enteredSerialNumber = Convert.ToInt32(Console.ReadLine());
                    var filterContacts = bookModels.FindAll(i => i.Section == section);
                    var validBookModel = filterContacts.Find(i => i.SNo == enteredSerialNumber);
                    int validSerialNumber = validBookModel != null ? validBookModel.SNo : 0;
                    if (enteredSerialNumber == validSerialNumber)
                    {
                        Console.WriteLine($"Dialing {validBookModel.Name} {validBookModel.Number}...");
                    }
                    else
                    {
                        Console.WriteLine("Invalid serial number. Please try again.");
                    }
                }
                else if (options == 3)
                {
                    continue;
                }
            }
        }
        public int PhoneBookOptions(string section)
        {
            var filterContacts = bookModels.FindAll(i => i.Section == section);

            if (filterContacts.Count > 0)
            {
                Console.WriteLine($"Contacts in {section}...\n");
                Console.WriteLine($"SNo \t\t\t Name \t\t\t Mobile");
                Console.WriteLine($"---------------------------------------------------------------");
                var sNo = 1;
                foreach (var contact in filterContacts)
                {
                    contact.SNo = sNo;
                    Console.WriteLine($"{contact.SNo}\t\t\t {contact.Name} \t\t\t {contact.Number}\n");
                    sNo++;
                }

                Console.WriteLine($"> Total Contacts :- {filterContacts.Count} \n");
            }
            else
            {
                Console.WriteLine($"> No contacts available in section '{section}'\n");
            }

            Console.WriteLine("Options:");
            Console.WriteLine("1) Add New Contact");
            Console.WriteLine("2) Dial");
            Console.WriteLine("3) Go back");

            int options;

            if (!int.TryParse(Console.ReadLine(), out options) || options < 1 || options > 3)
            {
                Console.WriteLine("Invalid input. Please enter a valid option (1-3).");
            }

            return options;
        }

        public void AddContact(PhoneBoolModel phoneBook, string section)
        {

            if (IsValidContactName(phoneBook.Name))
            {
                string slicedName = phoneBook.Name.Substring(0, 1).ToUpper();
                if (slicedName != section)
                {
                    Console.WriteLine($"Section and entered name do not match.\n");
                    Console.Write($"Do you want to change section? (yes/no):");
                    if (Console.ReadLine().ToLower() == "yes")
                    {
                        phoneBook.Section = slicedName;
                        Console.Write("Enter Contact Number :-");
                        phoneBook.Number = Console.ReadLine();

                        if (IsValidNumber(phoneBook.Number))
                        {
                            Console.WriteLine($"Contact {phoneBook.Name} added successfully.\n");
                            bookModels.Add(phoneBook);
                        }
                        else
                        {
                            Console.WriteLine("InValid phone number.");
                            AddContact(phoneBook, section);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Contact '{phoneBook.Name}' not added.");
                    }
                }
                else
                {
                    phoneBook.Section = slicedName;
                    Console.Write("Enter Contact Number :-");
                    phoneBook.Number = Console.ReadLine();

                    if (IsValidNumber(phoneBook.Number))
                    {
                        Console.WriteLine($"Contact {phoneBook.Name} added successfully.\n");
                        bookModels.Add(phoneBook);
                    }
                    else
                    {
                        Console.WriteLine("Invalid phone number.");
                        AddContact(phoneBook, section);
                    }
                }
            }
            else
            {
                Console.Write("Enter Contact Name :-");
                phoneBook.Name = Console.ReadLine();
                AddContact(phoneBook, section);
            }
        }

        public bool IsValidContactName(string contactName)
        {
            if (string.IsNullOrWhiteSpace(contactName))
            {
                Console.WriteLine($"Enter Valid Contact Name ");
            }
            string pattern = @"^[A-Za-z\s-]+$";
            return Regex.IsMatch(contactName, pattern);
        }

        public bool IsValidNumber(string Number)
        {
            if (string.IsNullOrWhiteSpace(Number))
            {
                Console.WriteLine($"Enter InValid Contact Number ");
            }
            string pattern = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            return Regex.IsMatch(Number, pattern);
        }
    }
}
