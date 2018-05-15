using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedOrganization.Module
{
    public enum OrgName { Sunkar=1, SBSmed, Densaulyk, AlmaLife }
    public enum Streets { Abay = 1, ToleBI, Pravda, Shalyapina, Lenina, Morechka, Rozybakieva}

    public class MedOrgService
    {
        Random r = new Random();

        public List<MedOrganization> lmed = new List<MedOrganization>();

        public MedOrganization this[int id]
        {
            get
            {
                foreach (MedOrganization item in lmed)
                {
                    if (item.Id == id)
                        return item;
                }
                return null;
            }
        }

        public MedOrgService()
        {
            OrgGenerator();
        }

        public void OrgGenerator(int size=0)
        {
            if (size == 0)
                size = r.Next(1, 20);

            for (int i = 0; i < size; i++)
            {
                string NameOrgan = ((OrgName)r.Next(1, 4)).ToString();
                string Adres = ((Streets)r.Next(1, 7)).ToString();
                string TelNumber = "+7"+ (r.Next(701, 708).ToString()) + (r.Next(1000000, 9999999).ToString());
                MedOrganization newPac = new MedOrganization(NameOrgan, Adres, TelNumber);
                newPac.Id = r.Next(100000, 999999);
                lmed.Add(newPac);
            }
        }

        public void SearchOrg(string nameOrgan, string adres)
        {
            bool yes = false;
            foreach (MedOrganization item in lmed)
            {
                if (item.NameOrgan == nameOrgan && item.Adres == adres)
                {
                    item.MedOrganizationInfo();
                    yes = true;
                    break;
                }
            }

            if (!yes)
                Console.WriteLine("Takoi organizacii net=(");
        }
        public void PokazVsehOrg(List<MedOrganization> mo)
        {
            foreach (MedOrganization item in mo)
            {
                item.MedOrganizationInfo();
                Console.WriteLine("____________________________");
            }
        }
    }
}
