using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedOrganization.Module
{
    public class MedOrganization
    {
        public List<Pacient> pacientList;

        public MedOrganization():this("","","")
        {

        }
        public MedOrganization(string nameOrgan, string adres, string telNumber)
        {
            NameOrgan = nameOrgan;
            Adres = adres;
            TelNumber = telNumber;

            pacientList = new List<Pacient>();
        }

        //2.	Мед Организация(Наименование, Адрес, Телефон)
        public int Id { get; set; }
        public string NameOrgan { get; set; }
        public string Adres { get; set; }
        public string TelNumber { get; set; }

        public void MedOrganizationInfo()
        {
            Console.WriteLine($"Nazvanie organizacii ={NameOrgan} \n"+
                              $"Adres organizacii ={Adres} \n"+
                              $"TelNumber organizacii ={TelNumber} \n"+
                              $"ID organizacii ={Id} \n");
        }

        //PacientServise p = new PacientServise();
        
    }
}
