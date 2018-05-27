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
        public MedOrganization MedOrganization
        {
            get { return MedOrganizationId ==null? null: MedOrgService.Instance[MedOrganizationId.Value]; }
            set { MedOrganizationId = value?.Id; }
        }
        public int? MedOrganizationId { get; internal set; }

        public void PacientInfo()
        {
            Console.WriteLine(
$@"Familiya = {Familiya}
Imya = {Imya}
Otchestvo = {Otchestvo}
IIN = {IIN}
Org = {MedOrganization?.NameOrgan}");
        }
    }
}
