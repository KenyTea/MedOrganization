using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedOrganization.Module
{
    public class ZapropsyNaZakreplenie
    {
        // 4.	Запросы на прикрепление(Дата создания, Дата обработки, Пациент, статус запроса)
        //public ZapropsyNaZakreplenie(Pacient pacient, MedOrgService med)
        //{
                
        //}
        public DateTime DataSozdaniyaZaprosa { get; set; }
        public DateTime DataObrabotkiZaprosa { get; set; }
        public string Status { get; set; }
        public Pacient Pacien { get; set; }
        public MedOrganization MedOrg { get; set; }

    }
}
