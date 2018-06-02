using MedOrganization.Module.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedOrganization.Module
{
    public class ServiceZakreplenie
    {
        public bool Zakreplenie(ref MedOrgService ms, ref PacientServise ps, out string mesage)
        {
            #region Select Pation
            //int selectIin = 9;
            //string name = "";
            foreach (Pacient p in ps.PacientList.Take(10))
                p.PacientInfo();

            Console.WriteLine("           --------------------------------------");
            Pacient selectPation = new Pacient();
            Console.WriteLine("                 Select patient, enter IIN");
            // Console.WriteLine("For search by IIN press 1 | for search by Name, press 2 | for exit press 0");
            //int.TryParse(Console.ReadLine(), out int select);
            //switch (select)
            //{
            //    case 1:
            // Console.WriteLine("                       Enter IIN");
            int selectIin = Int32.Parse(Console.ReadLine());
            selectPation = ps[selectIin];
            Console.WriteLine("          --------------------------------------");
            if (selectPation == null)
            {
                mesage = "                   This patient does not exist!";
                return false;
            }
            //        break;
            //    case 2:
            //        Console.WriteLine("                      Enter Name");
            //        name = Console.ReadLine();
            //        selectPation = ps[name];
            //        break;
            //}
            //#endregion
            #endregion
            #region Select MedOrg
            foreach (MedOrganization o in ms.MedOrgList.Take(10))
                o.MedOrganizationInfo();

            Console.WriteLine("           --------------------------------------");
            MedOrganization selectOrg = new MedOrganization();
            Console.WriteLine("                   Select med organization");
            int selectOrgId = Int32.Parse(Console.ReadLine());
            selectOrg = ms[selectOrgId];
            Console.WriteLine("           --------------------------------------");

            #endregion

            Console.Clear();

            //if (select == 1)
            //{
            var so = ms.MedOrgList.FirstOrDefault(f => f.Id == selectOrgId);

            foreach (var p in so.PacientList)
            {
                if (p.IIN == selectIin)
                {
                    mesage = "This patient is already attached!";
                    return false;
                }
            }

            so.PacientList.Add(selectPation);
            ps.PacientList.FirstOrDefault(f => f.IIN == selectIin)
                .MedOrganization = selectOrg;

            mesage = "This patient is successfully attached!";
            return true;

            //else if(select == 2)
            //{
            //    var so = ms.MedOrgList.FirstOrDefault(f => f.Id == selectOrgId);

            //    foreach (var p in so.PacientList)
            //    {
            //        if (p.Imya == name)
            //        {
            //            mesage = "Данный пациент уже прикреплен!";
            //            return false;
            //        }
            //    }

            //    so.PacientList.Add(selectPation);
            //    ps.PacientList.FirstOrDefault(f => f.Imya == name)
            //        .MedOrganization = selectOrg;

            //    //mesage = "Данный пациент прикреплен успешно!";
            //    //return true;
            //}
            //mesage = "Данный пациент прикреплен успешно!";
            //return true;
        }
    }
}

