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
        public bool Zakreplenie(ref MedOrgService ms, 
            ref PacientServise ps, 
            out string mesage)
        {
            #region Select Pation
            foreach (Pacient p in ps.PacientList.Take(10))
                p.PacientInfo();

            Console.WriteLine("--------------------------------------");
            Pacient selectPation = new Pacient();
            Console.WriteLine("Выберите пациента");
            int selectIin = Int32.Parse(Console.ReadLine());
            selectPation = ps[selectIin];
            Console.WriteLine("--------------------------------------");
            if (selectPation == null)
            {
                mesage = "Данный пациент не существует!";
                return false;
            }
            #endregion

            #region Select MedOrg
            foreach (MedOrganization o in ms.lmed.Take(10))
                o.MedOrganizationInfo();

            Console.WriteLine("--------------------------------------");
            MedOrganization selectOrg = new MedOrganization();
            Console.WriteLine("Выберите мед организацию");
            int selectOrgId = Int32.Parse(Console.ReadLine());
            selectOrg = ms[selectOrgId];
            Console.WriteLine("--------------------------------------");

            #endregion

            Console.Clear();

            var so = ms.lmed.FirstOrDefault(f => f.Id == selectOrgId);
            
            foreach (var p in so.pacientList)
            {
                if (p.IIN == selectIin)
                {
                    mesage = "Данный пациент уже прикреплен!";
                    return false;
                }
            }

            so.pacientList.Add(selectPation);
            ps.PacientList.FirstOrDefault(f=>f.IIN == selectIin)
                .MedOrganization = selectOrg;

            mesage = "Данный пациент прикреплен успешно!";
            return true;
        }
    }
}
