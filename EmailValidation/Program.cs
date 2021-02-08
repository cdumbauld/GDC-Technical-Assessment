using System;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using EmailValidation;

namespace EmailValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Enter a .csv file name for email validation:");
            string fileName = Console.ReadLine();//user inputs file name
            
            Files f = new Files();
            f.fileValidation(fileName); //goes to fileValidation in the Files class

            Console.WriteLine("Valid Emails: ");//print the valid emails
            foreach(var emailAddy in Email.validEmails)
            {
                Console.WriteLine(emailAddy);
            }
            Console.WriteLine("\nInvalid Emails: ");//prints the invalid emails.
             foreach(var emailAddy in Email.invalidEmails)
             {   
                 Console.WriteLine(emailAddy);
             }
        }
    }
}
