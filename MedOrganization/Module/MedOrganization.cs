using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedOrganization.Module
{
    public class MedOrganization
    {
        public MedOrganization() : this("", "", "")
        { }
        public MedOrganization(string nameOrgan, string adres, string telNumber)
        {
            NameOrgan = nameOrgan;
            Adress = adres;
            TelNumber = telNumber;
            PacientList = new List<Pacient>();
        }

        public int Id { get; set; }
        public string NameOrgan { get; set; }
        public string Adress { get; set; }
        public string TelNumber { get; set; }
        public List<Pacient> PacientList { get; set; }

        public void MedOrganizationInfo()
        {
            Console.WriteLine($"Nazvanie organizacii ={NameOrgan} \n" +
                              $"Adres organizacii ={Adress} \n" +
                              $"TelNumber organizacii ={TelNumber} \n" +
                              $"ID organizacii ={Id} \n");
            foreach (var pacient in PacientList)
            {
                Console.WriteLine($"    Pacient ={pacient.Family} {pacient.Name} {pacient.IIN}");
            }
        }
    }
}
