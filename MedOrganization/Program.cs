using MedOrganization.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedOrganization.Module.Services;
using MedOrganization.Module;
using MedOrganization.Services;

namespace MedOrganization
{
    class Program
    {
        static void Main(string[] args)
        {
            ////Есть пациенты
            //PacientServise ps = new PacientServise();

            ////Есть мед организации
            //MedOrgService ms = new MedOrgService();


            //ServiceZakreplenie sz = new ServiceZakreplenie();
            //string message = "";
            //sz.Zakreplenie(ref ms, ref ps, out message);

            //ms.Save();
            //ps.Save();

            Console.WriteLine();
            Console.WriteLine();

            UserService us = new UserService();
            Console.WriteLine("Registration");
           // us.Generate();
            us.Registration();
            //Console.WriteLine("Print");
           // us.PrintList();
            //Console.WriteLine("WriteToFileWithLogAndPass");
            //us.WriteToFileWithLogAndPass();
            //Console.WriteLine("ReadFromFileWithLogAndPass");
            //us.ReadFromFileWithLogAndPass();
        }
    }
}
