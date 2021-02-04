using System;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EmailValidation
{
    class Program
    {
        public static List<string> validEmails = new List<string>();
        public static List<string> invalidEmails = new List<string>();

        static void Main(string[] args)
        {

            Console.WriteLine("Enter a .csv file name for email validation:");
            string fileName = Console.ReadLine();//user inputs file name
            if(fileName.Contains(".csv")) // checks if the filename contains .csv
            {
                fileValidation(fileName);
                
            }
            else                           //will add .csv to the filename
            {
                fileName = fileName + ".csv";
                fileValidation(fileName);
            }
            
            
            Console.WriteLine("Valid Emails: ");//print the valid emails
            foreach(var email in validEmails)
            {
                Console.WriteLine(email);
            }
            Console.WriteLine("\nInvalid Emails: ");//prints the invalid emails.
             foreach(var email in invalidEmails)
             {   
                 Console.WriteLine(email);
             }
        }


        //check to see if file exists
        static void fileValidation(string fileName)
        {
            string path = Directory.GetCurrentDirectory();
            try //search for the file
            {
                var file = Directory.GetFiles(path,fileName,SearchOption.AllDirectories);
                string filePath = path + "\\" + fileName;
                 readFile(filePath);
            }
            catch (FileNotFoundException) // catches if no file exist
            {
                Console.WriteLine("Error: File Not Found!");
                Environment.Exit(0);

            }
            
        }


        //read the file
        static void readFile(string filePath)
        {
            string[] data = File.ReadAllLines(filePath);
            //organize each user data into firstname lastname and email.
            foreach(var line in data)
            {
                string[] columns = line.Split(",");
                string firstName = columns[0];
                string lastName = columns[1];
                string email = columns[2];
                
                if(vaildidateEmail(email))
                {
                    validEmails.Add(email);
                }else
                {
                    invalidEmails.Add(email);
                }
            }
        }
  

        //validate email
        static bool vaildidateEmail(string email)
        {
            var atChecker = new EmailAddressAttribute();
            string [] topLevelDom = {".com",".edu",".org",".net",".gov"};
            bool valid;
            int emailLen = email.Length;
            valid = atChecker.IsValid(email); // checks to see if the @ sign is in the email
            
            if(valid == true)
            {
                //to check if the .com is in the email
                string validateTopLevel = email.Substring(emailLen - 4);
                var atIndex = email.IndexOf("@");
                for(int i =0; i< topLevelDom.Length;i++)
                {
                    if(validateTopLevel.Contains(topLevelDom[i]))//if .com etc is in email
                    {
                        string domName = email.Substring(atIndex + 1);
                        domName = domName.Remove(domName.IndexOf("."));
                        if(domName.Equals("")) //checks to see if the there is a domain name after the @ and before the .com
                        {
                            return false;
                        }
                        
                        //check to see if the recipient name is all valid characters
                        for(int letter = 0; letter<domName.Length;letter++)
                        {
                            char character = domName[letter];
                                
                            if((character >= 48 && character <= 57)  //number characters
                                || (character >= 65 && character <=90)    //upper case letters  
                                || (character >= 97 && character <= 122)) //lower case letters 
                            {
                                valid = true;
                            }
                            else //character not valid
                            {
                                valid = false;
                            }
                            
                        }
                        break;
                    }
                    else //if .com is not in email
                    {
                        valid = false;
                    }              
                }
            }
            else //if email is not valid no @ included
            {
                return false;
            }
            
            return valid;
        }

    }
}
