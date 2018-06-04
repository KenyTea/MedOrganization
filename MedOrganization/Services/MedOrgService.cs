using MedOrganization.Module.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MedOrganization.Module
{


    public enum OrgName { Sunkar = 1, SBSmed, Densaulyk, AlmaLife }
    public enum Streets { Abay = 1, ToleBI, Pravda, Shalyapina, Lenina, Morechka, Rozybakieva }

    public class MedOrgService
    {
        public static MedOrgService Instance { get; private set; } // m

        public List<MedOrganization> MedOrgList = new List<MedOrganization>();

        Random r = new Random();

        public MedOrgService()
        {
            OrgGenerator(10);
            Instance = this; // m
            Load();
        }

        public MedOrganization this[int id]
        {
            get
            {
                foreach (MedOrganization item in MedOrgList)
                {
                    if (item.Id == id)
                        return item;
                }
                return null;
            }
        }

        public void Save()
        {
            string pathMed = @"MedOrganithations.xml";

            var document = new XmlDocument();
            var xmlDeclaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            var root = document.DocumentElement;
            document.InsertBefore(xmlDeclaration, root);
            var medOrganList = document.CreateElement(nameof(MedOrgList));

            foreach (var org in MedOrgList)
            {
                var nodeOrg = document.CreateElement(nameof(MedOrganization));
                var nodeiD = document.CreateElement(nameof(MedOrganization.Id));
                nodeiD.InnerText = org.Id.ToString();
                nodeOrg.AppendChild(nodeiD);
                var name = document.CreateElement(nameof(MedOrganization.NameOrgan));
                name.InnerText = org.NameOrgan;
                nodeOrg.AppendChild(name);
                var address = document.CreateElement(nameof(MedOrganization.Adress));
                address.InnerText = org.Adress;
                nodeOrg.AppendChild(address);

                var pacients = document.CreateElement(nameof(MedOrganization.PacientList));
                foreach (var pacient in org.PacientList)
                {
                    var nodePacient = document.CreateElement(nameof(Pacient));
                    nodePacient.InnerText = pacient.IIN.ToString();
                    pacients.AppendChild(nodePacient);
                }
                nodeOrg.AppendChild(pacients);

                medOrganList.AppendChild(nodeOrg);
            }
            document.AppendChild(medOrganList);
            document.Save(pathMed);

        }

        public void Load()
        {
            string pathMed = @"C:\Users\AVassilyev\Source\Repos\MedOrganization\MedOrganization\bin\Debug\MedOrganithations.xml";
            if (!File.Exists(pathMed))
            {
                OrgGenerator();
            }
            else
            {
                var document = new XmlDocument();
                document.Load(pathMed);

                foreach (XmlElement node in document.DocumentElement)
                {
                    var org = new MedOrganization();
                    org.Id = int.Parse(node[nameof(MedOrganization.Id)].InnerText);
                    org.NameOrgan = node[nameof(MedOrganization.NameOrgan)].InnerText;
                    org.Adress = node[nameof(MedOrganization.Adress)].InnerText;
                    var nodeList = node[nameof(MedOrganization.PacientList)];
                    foreach (XmlElement nodePacient in nodeList)
                    {
                        var id = int.Parse(nodePacient.InnerText);
                        org.PacientList.Add(PacientServise.Instance[id]);
                    }
                    MedOrgList.Add(org);
                }
            }
        }

        public void OrgGenerator(int size = 0)
        {
            if (size == 0)
                size = r.Next(1, 20);

            for (int i = 0; i < size; i++)
            {
                string NameOrgan = ((OrgName)r.Next(1, 4)).ToString();
                string Adres = ((Streets)r.Next(1, 7)).ToString();
                string TelNumber = "+7" + (r.Next(701, 708).ToString()) + (r.Next(1000000, 9999999).ToString());
                MedOrganization newPac = new MedOrganization(NameOrgan, Adres, TelNumber);
                newPac.Id = r.Next(100000, 999999);
                MedOrgList.Add(newPac);
            }
        }

        public void SearchOrg(string nameOrgan, string adres)
        {
            PokazVsehOrg();
            bool yes = false;
            foreach (MedOrganization item in MedOrgList)
            {
                if (item.NameOrgan == nameOrgan && item.Adress == adres)
                {
                    item.MedOrganizationInfo();
                    yes = true;
                    break;
                }
            }

            if (!yes)
                Console.WriteLine("Takoi organizacii net=(");
        }
        public void PokazVsehOrg()
        {
            foreach (MedOrganization item in MedOrgList)
            {
                item.MedOrganizationInfo();
                Console.WriteLine("____________________________");
            }
        }
    }
}
