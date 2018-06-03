using MedOrganization.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedOrganization.Module.Services;
using MedOrganization.Services;

namespace MedOrganization
{
    class Program
    {
        static void Main(string[] args)
        {
            PacientServise ps = new PacientServise();
            MedOrgService ms = new MedOrgService();
            UserService us = new UserService();
            us.Menu();
            //us.Menu2();
            ////Есть пациенты

            ////Есть мед организации


           // ServiceZakreplenie sz = new ServiceZakreplenie();
            //string message = "";
            //sz.Zakreplenie(ref ms, ref ps, out string message);

            //ms.Save();
            //ps.Save();

            //us.Generate();
            // us.Registration();
            //us.LoginService();

            //Console.WriteLine("Print");
            // us.PrintList();
            //Console.WriteLine("WriteToFileWithLogAndPass");
            //us.WriteToFileWithLogAndPass();
            //us.Save();
            //Console.WriteLine("ReadFromFileWithLogAndPass");
            //us.ReadFromFileWithLogAndPass();
            //us.Load();
        }
    }
}
