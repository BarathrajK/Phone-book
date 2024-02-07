using PhoneBookApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp
{
    public class Program
    {

      public static void Main(string[] args)
        {
            Console.WriteLine("-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
            Console.WriteLine("\t \t PhoneBook \t \t ");
            Console.WriteLine("-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");

            PhoneBookService phoneBookService = new PhoneBookService();
            phoneBookService.PhoneBookDetails();
            Console.ReadLine();
        }
    }
}
