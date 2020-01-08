using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Csh_bezGUI_a
{
    class Program
    {
        static string ShaHashing(string unHashedString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(unHashedString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
        static void Main(string[] args)
        {
            string line;
            short numberOfPasswords=0;
            string passwords;
            string odabir;
            string hashedPassword=string.Empty;
            string filePath = @"C:\Users\Bruno\Desktop\passwordStorage\password.txt";
            Console.WriteLine("To enter press 1, To check passwords press 2, keep entering passwords: \n");
            Console.WriteLine("To stop entering, enter: 0\n");
            odabir = Console.ReadLine();
            while (true)
            {
                if (odabir == "0")
                    break;
                //For entering passwords
                if (odabir == "1")
                {
                    while (true)
                    {
                        passwords = Console.ReadLine();
                        hashedPassword = ShaHashing(passwords);
                        using (StreamWriter writer = File.AppendText(filePath))
                        {
                            writer.WriteLine(hashedPassword);
                            hashedPassword = "";
                        }
                        if (passwords == "0")
                            break;
                    }
                    break;
                }
                //Password checking
                if (odabir == "2")
                {
                    short doesPassExistFlag = 0;
                    string passwordForChecking = Console.ReadLine();
                    passwordForChecking = ShaHashing(passwordForChecking);
                    System.IO.StreamReader file = new System.IO.StreamReader(filePath);
                    Console.WriteLine("Is password in the base?");
                    while ((line = file.ReadLine()) != null)
                    {
                        string tempDoesPassExist=String.Compare(line, passwordForChecking, false) == 0 ? "yes" : "no";
                        if (tempDoesPassExist == "yes")
                            doesPassExistFlag = 1;
                        numberOfPasswords++;
                    }
                    if (doesPassExistFlag == 1)
                        Console.WriteLine("YES IT IS!");
                    else
                        Console.WriteLine("No it's not.");
                }
                break;
            }
            Console.ReadLine();
        }
    }
}
