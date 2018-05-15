using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedOrganization.Module
{
    public class Pacient
    {
        public Pacient()
        {

        }
        public Pacient(string familiya, string imya, string otchestvo, int iIN)
        {
            Familiya = familiya;
            Imya = imya;
            Otchestvo = otchestvo;
            IIN = iIN;
        }

        //1.	Пациент(Фамилия, Имя, Отчество, ИИН)
        public string Familiya { get; set; }
        public string Imya { get; set; }
        public string Otchestvo { get; set; }
        public int IIN { get; set; }
        public MedOrganization MedOrganization { get; set; }

        public void PacientInfo()
        {
            Console.WriteLine(
                              $"Familiya = {Familiya}\n" +
                              $"Imya = {Imya}\n" +
                              $"Otchestvo = {Otchestvo}\n" +
                              $"IIN = {IIN}\n");
        }
    }
}
