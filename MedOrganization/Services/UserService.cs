﻿using MedOrganization.Module;
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
        List<User> listUsers = new List<User>();
        List<User> tempList = new List<User>();

        string path = @"FileWithLogAndPass.txt";

        public void Generate()
        {
            User user = new User();
            user.PravaDostupa_ = PravaDostupa.Admin;
            user.Login = "Root";
            user.Pass = "admin";
            listUsers.Add(user);
            User u = new User();
            u.PravaDostupa_ = PravaDostupa.User;
            u.Login = "Alex";
            u.Pass = "111";
            listUsers.Add(u);
            User us = new User();
            us.PravaDostupa_ = PravaDostupa.User;
            us.Login = "Sergey";
            us.Pass = "222";
            listUsers.Add(us);
            User use = new User();
            use.PravaDostupa_ = PravaDostupa.User;
            use.Login = "Max";
            use.Pass = "333";
            listUsers.Add(use);

            Save();
            Load();
        }

        public void Registration()
        {           
            User newUser = new User();
            newUser.PravaDostupa_ = PravaDostupa.User;

            Console.WriteLine("For registration Enter Your user name: ");
            string uName = Console.ReadLine();
            foreach (var item in tempList)
            {
                if (item.Login == uName)
                {
                    Console.WriteLine("Such name already exists");
                }
                else newUser.Login = uName;
            }

            Console.Clear();
            Console.WriteLine("Enter Your password: ");
            string word1 = Console.ReadLine();
            Console.WriteLine("Please enter password again: ");
            string word2 = Console.ReadLine();
            if (word1 == word2)
            {
                newUser.Pass = word2;
                listUsers.Add(newUser);
                WriteToFileWithLogAndPass();
                Save();
                PrintList(listUsers);
            }
            else Console.WriteLine("Error");
            return;
        }

        #region print list
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
            Console.WriteLine();
            Console.WriteLine("Please enter Your login: ");
            string log = Console.ReadLine();
            foreach (var item in tempList)
            {
                if (item.Login == log)
                {
                    Console.WriteLine("Enter Your password ");
                    string pass = Console.ReadLine();
                    if (item.Pass == pass)
                    {
                        Console.WriteLine("Welcom " + log);
                    }
                    else Console.WriteLine("Password entered incorrectly");
                }
                else Console.WriteLine("You made a mistake in the name or you are not registered");
            }

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

        public void ReadFromFileWithLogAndPass()
        {
            User newUser = new User();
            List<User> tempList = new List<User>();
            FileInfo fi = new FileInfo(path);
            string texts;
            using (FileStream fs = fi.Open(FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default))
                {
                    texts = sr.ReadToEnd();
                    var m = texts.Split(';');
                    newUser.PravaDostupa_ = (PravaDostupa)Enum.Parse(typeof(PravaDostupa), m[0]);
                    newUser.Login = m[1];
                    newUser.Pass = m[2];
                    Console.WriteLine("Read from file");
                }
            }
            tempList.Add(newUser);
            // PrintList(tempList);
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
                nodeU.SetAttribute(nameof(User.Login), us.Login);
                nodeU.SetAttribute(nameof(User.Pass), us.Pass);
                nodeU.SetAttribute(nameof(User.PravaDostupa_), (us.PravaDostupa_).ToString());
                userList.AppendChild(nodeU);
            }
            document.AppendChild(userList);
            document.Save(path);

        }

        public void Load()
        {
            string path = @"Users.xml";

            if (!File.Exists(path))
            {
                Console.WriteLine("The file not found");
            }
            else
            {
                var xmldoc = new XmlDocument();
                xmldoc.Load(path);
                // var userr = listUsers.DocumentElement;


                foreach (XmlElement node in xmldoc.DocumentElement)
                {
                    var use = new User();
                    use.Login = node.GetAttribute(nameof(User.Login));
                    use.Pass = node.GetAttribute(nameof(User.Pass));
                    use.PravaDostupa_ = (PravaDostupa)Enum.Parse(typeof(PravaDostupa), node.GetAttribute(nameof(User.PravaDostupa_)));

                    tempList.Add(use);
                    PrintList(tempList);
                }
            }
        }

        //public void Save(List<User> adm)
        //{
        //    string path = @"Users.xml";

        //    var document = new XmlDocument();
        //    var xmlDeclaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
        //    var root = document.DocumentElement;
        //    document.InsertBefore(xmlDeclaration, root);
        //    var userList = document.CreateElement(nameof(adm));

        //    foreach (var us in listUsers)
        //    {
        //        var nodeU = document.CreateElement(nameof(User));
        //        var nodeState = document.CreateElement(nameof(User.PravaDostupa_));
        //        var log = document.CreateElement(nameof(User.Login));
        //        var pass = document.CreateElement(nameof(User.Pass));

        //        nodeState.InnerText = (us.PravaDostupa_).ToString();
        //        log.InnerText = us.Login;
        //        pass.InnerText = us.Pass;

        //        userList.AppendChild(nodeU);
        //        nodeU.AppendChild(nodeState);
        //        nodeState.AppendChild(log);
        //        log.AppendChild(pass);
        //    }
        //    document.AppendChild(userList);
        //    document.Save(path);

        //}

        public void Menu()
        {
            Generate();
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("            --------------MENU--------------");
            Console.WriteLine("For register, press 1 | for enter, press 2 | for exit perss 0 ");

            int.TryParse(Console.ReadLine(), out int choice);
            while (true)
            {
            switch (choice)
            {
                    case 1: Registration(); break;
                    case 2: LoginService(); break;
                default:
                    break;
            }

            }
        }

        public void Menu2()
        {

        }
    }
}
