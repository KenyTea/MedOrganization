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
            Family = familiya;
            Name = imya;
            MidleName = otchestvo;
            IIN = iIN;
        }

        //1.	Пациент(Фамилия, Имя, Отчество, ИИН)
        public string Family { get; set; }
        public string Name { get; set; }
        public string MidleName { get; set; }
        public int IIN { get; set; }

        public MedOrganization MedOrganization //m
        {
            get { return MedOrganizationId == null ? null : MedOrgService.Instance[MedOrganizationId.Value]; }
            set { MedOrganizationId = value?.Id; }
        }
        public int? MedOrganizationId { get; internal set; }

        public void PacientInfo()
        {
            //Console.WriteLine(
            //      $@"Familiya = {Family}
            //      Imya = {Imya}
            //      Otchestvo = {Otchestvo}
            //      IIN = {IIN}
            //      Org = {MedOrganization?.NameOrgan}");
            Console.WriteLine("Family: " + Family);
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Midle name: " + MidleName);
            Console.WriteLine("IIN: " + IIN);
            Console.WriteLine("Med Organization: " + MedOrganization?.NameOrgan);
            Console.WriteLine("---------------------------");
        }
    }
}
