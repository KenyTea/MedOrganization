using MedOrganization.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedOrganization.Module.Services;
using MedOrganization.Module;

namespace MedOrganization
{
    class Program
    {
        static void Main(string[] args)
        {
            //Есть пациенты
            PacientServise ps = new PacientServise();

            //Есть мед организации
            MedOrgService ms = new MedOrgService();


            ServiceZakreplenie sz = new ServiceZakreplenie();
            string message = "";
            sz.Zakreplenie(ref ms, ref ps, out message);
        }
    }
}
