using MedOrganization.Module;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MedOrganization.Services
{
    public class UserService
    {
        User user = new User();
        List<User> listUsers = new List<User>();
        List<User> tempList = new List<User>();

        string path = @"FileWithLogAndPass.txt";

        public void Generate()
        {
            user.PravaDostupa_ = PravaDostupa.Admin;
            user.Login = "Root";
            user.Pass = "admin";
            listUsers.Add(user);
        }

        public void Registration()
        {


            Generate();
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
                WriteToFileWithLogAndPass();
                Save();
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
                Console.WriteLine("Status: {0} Login: {1} ({2}) ", item.PravaDostupa_, item.Login, item.Pass);
            }
        }

        public void PrintList(List<User> u)
        {
            Console.Clear();

            foreach (var item in u)
            {
                Console.WriteLine("Status: {0} Login: {1} ({2}) ", item.PravaDostupa_, item.Login, item.Pass);
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

            using (FileStream fs = fi.Open(FileMode.Open, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default))
                {
                    foreach (var item in listUsers)
                    {
                        sw.Write(item.PravaDostupa_.ToString());
                        sw.Write(";");
                        sw.Write(item.Login);
                        sw.Write(";");
                        sw.Write(item.Pass);
                        sw.WriteLine(" ");
                        Console.WriteLine("Add to file"); // проверка на выполнения
                    }
                }
            }
        }

        public string ReadFromFileWithLogAndPass()
        {
            FileInfo fi = new FileInfo(path);
            string texts;
            using (FileStream fs = fi.Open(FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default))
                {
                    texts = sr.ReadLine();
                    var m = texts.Split(';');
                    user.PravaDostupa_ = (PravaDostupa)Enum.Parse(typeof(PravaDostupa), m[0]);
                    user.Login = m[1];
                    user.Pass = m[2];
                    tempList.Add(user);
                    Console.WriteLine("Read from file");
                    //PrintList(tempList);
                }
                return texts;
            }
        }

        public void Save()
        {
            string path = @"Users.xml";

            var document = new XmlDocument();
            var xmlDeclaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            var root = document.DocumentElement;
            document.InsertBefore(xmlDeclaration, root);
            var userList = document.CreateElement(nameof(listUsers));

            foreach (var us in listUsers)
            {
                var nodeU = document.CreateElement(nameof(User));
                var nodeState = document.CreateElement(nameof(User.PravaDostupa_));
                var log = document.CreateElement(nameof(User.Login));
                var pass = document.CreateElement(nameof(User.Pass));

                nodeState.InnerText = (us.PravaDostupa_).ToString();
                log.InnerText = us.Login;
                pass.InnerText = us.Pass;

                userList.AppendChild(nodeU);
                nodeU.AppendChild(nodeState);
                nodeState.AppendChild(log);
                log.AppendChild(pass);
            }
            document.AppendChild(userList);
            document.Save(path);

        }

        public void Save(List<User> adm)
        {
            string path = @"Users.xml";

            var document = new XmlDocument();
            var xmlDeclaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            var root = document.DocumentElement;
            document.InsertBefore(xmlDeclaration, root);
            var userList = document.CreateElement(nameof(adm));

            foreach (var org in listUsers)
            {
                var nodeU = document.CreateElement(nameof(User));
                var nodeState = document.CreateElement(nameof(User.PravaDostupa_));
                var log = document.CreateElement(nameof(User.Login));
                var pass = document.CreateElement(nameof(User.Pass));

                nodeState.InnerText = (user.PravaDostupa_).ToString();
                log.InnerText = user.Login;
                pass.InnerText = user.Pass;

                userList.AppendChild(nodeU);
                nodeU.AppendChild(nodeState);
                nodeState.AppendChild(log);
                log.AppendChild(pass);
            }
            document.AppendChild(userList);
            document.Save(path);

        }
    }
}
