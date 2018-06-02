using MedOrganization.Module;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedOrganization.Services
{
    public class UserService
    {
        User user = new User();
        List<User> listUsers = new List<User>();

        List<User> tempList = new List<User>();

        string path = @"FileWithLogAndPass.txt";

        public void Registration()
        {
            Console.WriteLine("For registration Enter Your user name: ");
            string uName = Console.ReadLine();
            user.PravaDostupa_ = PravaDostupa.User;
            user.Login = uName;
            Console.Clear();
            Console.WriteLine("Enter Your password: ");
            string word1 = Console.ReadLine();
            Console.WriteLine("Please enter password again: ");
            string word2 = Console.ReadLine();
            if (word1 == word2)
            {
                user.Pass = word2;
                listUsers.Add(user);
            }
            else Console.WriteLine("Error");
            return;
        }

        #region test read list
        public void PrintList()
        {
            Console.Clear();

            foreach (var item in listUsers)
            {
                Console.WriteLine("Login: {0} ({1}) ", item.Login, item.Pass);
            }
        }

        public void PrintList(List<User> u)
        {
            Console.Clear();

            foreach (var item in u)
            {
                Console.WriteLine("Login: {0} ({1}) ", item.Login, item.Pass);
            }
        }
        #endregion

        public void LoginService()
        {
            Console.WriteLine("Please enter Your login: ");
            string log = Console.ReadLine();


        }

        public void WriteToFileWithLogAndPass()
        {
            FileInfo fi = new FileInfo(path);

            using (FileStream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default))
                {
                    foreach (var item in listUsers)
                    {
                        sw.WriteLine(item.PravaDostupa_.ToString());
                       // sw.Write(" ");
                        sw.WriteLine(item.Login);
                        //sw.Write(" ");
                        sw.WriteLine(item.Pass);
                        //sw.Write(" ");
                        Console.WriteLine("Add to file");
                    }
                }
            }
        }

        public string ReadFromFileWithLogAndPass()
        {
            FileInfo fi = new FileInfo(path);
            string texts;
            using (FileStream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default))
                {
                    texts = sr.ReadLine();

                    var m = texts.Split('\n');
                    user.PravaDostupa_ = m[0]; 
                    user.Login = m[1];
                    user.Pass = m[2];
                    tempList.Add(user);
                    Console.WriteLine("Read from file");
                    PrintList(tempList);
                }
                return texts;
            }
        }


    }
}
