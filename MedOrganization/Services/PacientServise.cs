using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MedOrganization.Module.Services
{
    public enum LastName { Verhovcev, Mihalkov, Ignatiev, Dixon, Christiano, Benner, Pedro, Hussein }
    public enum Name { Sergey, Dima, Svertlana, Olga, Diana, Fernando, Dimaw, Hussein }
    public enum Otchestvo { Egorov, Zhelezin, Severov, Olegov, Diakov, Ferinov, Panov, Husseinov }
    //    Модуль прикрепления пациента:
    //Функционал:
    //1.	Поиск по ФИО, и по ИИН в базе всех существующих пациентов

    //2.	Из результатов поиск должна быть возможность, выбрать пациента и создать
    //запрос на прикрепление, на выбранную организацию.

    public class PacientServise
    {
        public static PacientServise Instance { get; internal set; } // p
        public List<Pacient> PacientList = new List<Pacient>();

        private static readonly string fileName = "Pacient.xml";

        public PacientServise()
        {
            PacientGenerator(10); // !!!!!!!!!!!!!!!!!!!!!!
            Instance = this; // p
            Save();
           // Load();
        }

        public void Save()
        {
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            using (var file = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var writer = XmlWriter.Create(file, new XmlWriterSettings { Encoding = Encoding.UTF8, Indent = true }))
                {
                    writer.WriteStartElement(nameof(PacientList));
                    foreach (var pacient in PacientList)
                    {
                        writer.WriteStartElement(nameof(Pacient));
                        writer.WriteElementString(nameof(Pacient.IIN), pacient.IIN.ToString());
                        writer.WriteElementString(nameof(Pacient.Family), pacient.Family);
                        writer.WriteElementString(nameof(Pacient.Name), pacient.Name);
                        writer.WriteElementString(nameof(Pacient.MidleName), pacient.MidleName);
                        if (pacient.MedOrganization != null)
                            writer.WriteElementString(nameof(Pacient.MedOrganizationId), pacient.MedOrganization.Id.ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
            }
        }

        public void Load()
        {
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (!File.Exists(fileName))
            {
                PacientGenerator();
            }
            else
            {
                using (var file = new FileStream(fileName, FileMode.Open, FileAccess.Read)) // поток
                {
                    using (var reader = XmlReader.Create(file)) // открываем ридер
                    {
                        if (reader.ReadToFollowing(nameof(PacientList))) // читаем до  листа пациентов
                        {
                            while (reader.ReadToFollowing(nameof(Pacient))) // читай пока пациент
                            {
                                var pacient = new Pacient();
                                while (reader.Read()) // читаем пока не закончились элементы
                                {
                                    if (reader.NodeType == XmlNodeType.EndElement)
                                        break;
                                    if (reader.NodeType == XmlNodeType.Whitespace)
                                        continue;
                                    var name = reader.Name; // считали имя
                                    reader.Read(); // говорим читай
                                    switch (name)
                                    {
                                        case nameof(Pacient.IIN): 
                                            pacient.IIN = reader.ReadContentAsInt();
                                            break;
                                        case nameof(Pacient.Family):
                                            pacient.Family = reader.ReadContentAsString();
                                            break;
                                        case nameof(Pacient.Name):
                                            pacient.Name = reader.ReadContentAsString();
                                            break;
                                        case nameof(Pacient.MidleName):
                                            pacient.MidleName = reader.ReadContentAsString();
                                            break;
                                        case nameof(Pacient.MedOrganizationId):
                                            pacient.MedOrganizationId = reader.ReadContentAsInt();
                                            break;
                                    }
                                }
                                PacientList.Add(pacient);
                                PokazVsehPacientov();
                            }
                        }

                    }
                }
            }
        }

        public Pacient this[int iin]
        {
            get
            {
                foreach (Pacient item in PacientList)
                {
                    if (item.IIN == iin)
                        return item;
                }
                return null;
            }
        }

        public Pacient this[string name]
        {
            get
            {
                foreach (Pacient item in PacientList)
                {
                    if (item.Name == name)
                        return item;
                }
                return null;
            }
        }

        private Random rnd = new Random();

        
        private void PacientGenerator(int size = 0)
        {
            if (size == 0)
                size = rnd.Next(1, 20);

            for (int i = 0; i < size; i++)
            {
                string Familiya = ((LastName)rnd.Next(1, 8)).ToString();
                string Imya = ((Name)rnd.Next(1, 8)).ToString();
                string Otchestvo = ((Otchestvo)rnd.Next(1, 8)).ToString();
                int IIN = rnd.Next(100000, 999999);
                Pacient newPac = new Pacient(Familiya, Imya, Otchestvo, IIN);
                PacientList.Add(newPac);
            }
        }



        public void PokazVsehPacientov()
        {
            foreach (Pacient item in PacientList)
            {
                item.PacientInfo();
                Console.WriteLine("____________________________");
            }
        }
        public Pacient SearchIIN(int iin)
        {

            bool yes = false;
            foreach (Pacient item in PacientList)
            {
                if (item.IIN == iin)
                {
                    item.PacientInfo();
                    yes = true;
                    return item;
                }
            }

            if (!yes)
                Console.WriteLine("Takogo pacienta net=(");
            return null;
        }

    }
}
