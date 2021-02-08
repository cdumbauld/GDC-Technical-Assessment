using System;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using EmailValidation;
namespace EmailValidation
{
    class Email
    {
        public static List<string> validEmails = new List<string>();
        public static List<string> invalidEmails = new List<string>();
        
        public void vaildidateEmail(string emailAddy)
        {
            var atChecker = new EmailAddressAttribute();
            //checks to see if the email has an @ sign
            if(atChecker.IsValid(emailAddy))
            {
                checkDomName(emailAddy);//goes to check the domain name if @ sign is in the email.
            }
            else
            {
                invalidEmails.Add(emailAddy);//no @ sign
            }
        }
        
        public void checkDomName(string emailAddy)//checking the domain name and top level domain name
        {
            string [] topLevelDom = {".com",".edu",".org",".net",".gov"};
            bool valid = false;
            for(int i =0; i<topLevelDom.Length;i++)//loops through the top leve domain name
            {
                //checks to see if the email contains any of the top level domain names
                if(emailAddy.Substring(emailAddy.Length - 4).Contains(topLevelDom[i])) 
                {
                    string domName = emailAddy.Substring(emailAddy.IndexOf("@")+1);//domain name will be assigned the string after the @ sign
                    domName = domName.Remove(domName.IndexOf("."));//removes the .com etc from the domName

                    if(domName.Equals(""))//domName doesn't equal anything meaning there was nothing between the @ and the .com
                    {
                        valid = false;
                        break;
                    }
                    else
                    {
                        valid = true;
                        break;
                    }
                }
            }
            if(valid == true)
            {
                checkRecipientName(emailAddy);//goes to check the recipient name
            }
            else
            {
                invalidEmails.Add(emailAddy);//invalid email
            }
        }

        public void checkRecipientName(string emailAddy)
        {
            string repName = emailAddy.Substring(0,emailAddy.IndexOf("@"));//repname is assigned the string before the @ sign
            bool valid = false;
            for(int letter =0; letter <repName.Length;letter++)//loops through each letter in the email address
            {   //valid characters
                if((repName[letter] >= 33 && repName[letter] <= 57)  //number characters and special characters
                     || (repName[letter] >= 65 && repName[letter] <=90)    //upper case letters  
                     || (repName[letter] >= 95 && repName[letter] <= 122)) //lower case letters 
                {
                    valid = true;
                }
                 else //character not valid
                {
                    
                    valid = false;
                    break;
                }
            }
            if(valid == true)
            {
                validEmails.Add(emailAddy);//valid email passes all the tests
            }
            else
            {
                invalidEmails.Add(emailAddy);//invalid email fails the last test.
            }
        }

    }
}