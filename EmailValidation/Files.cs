using System;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using EmailValidation;

namespace EmailValidation
{
    class Files
    {
       
        //check to see if file exists
        public void fileValidation(string fileName)
        {
            if(fileName.Contains(".csv") == false) // checks if the filename contains .csv
            {
                fileName = fileName + ".csv"; // will add .csv to the file if it does not contain it
            }
            
            
            string path = Directory.GetCurrentDirectory(); //gets the current directory to find the file path.
            try //search for the file
            {
                var file = Directory.GetFiles(path,fileName,SearchOption.AllDirectories); //finds the file
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
            string[] data = File.ReadAllLines(filePath);//reads the file
            //organize each user data into firstname lastname and email.
            Email e = new Email();
            foreach(var line in data) //loops through the array and splits the columns into first name last name and email.
            {
                string[] columns = line.Split(",");
                string firstName = columns[0];
                string lastName = columns[1];
                string email = columns[2];
                
               e.vaildidateEmail(email);
            }
        }

    }
}