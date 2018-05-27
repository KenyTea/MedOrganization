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
        private static readonly string fileName = "Pacient.xml";
        public List<Pacient> PacientList = new List<Pacient>();
        public PacientServise()
        {
            Load();
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
                        writer.WriteElementString(nameof(Pacient.Familiya), pacient.Familiya);
                        writer.WriteElementString(nameof(Pacient.Imya), pacient.Imya);
                        writer.WriteElementString(nameof(Pacient.Otchestvo), pacient.Otchestvo);
                        if (pacient.MedOrganization != null)
                            writer.WriteElementString(nameof(Pacient.MedOrganization), pacient.MedOrganization.Id.ToString());
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
                using (var file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = XmlReader.Create(file))
                    {
                        if (reader.ReadToFollowing(nameof(PacientList)))
                        {
                            while (reader.ReadToFollowing(nameof(Pacient)))
                            {
                                var pacient = new Pacient();
                                if(reader.ReadToFollowing(nameof(Pacient.IIN)))
                                {
                                    pacient.IIN = reader.ReadElementContentAsInt();
                                }
                                if (reader.ReadToFollowing(nameof(Pacient.Familiya)))
                                {
                                    pacient.Familiya = reader.ReadElementContentAsString();
                                }
                                if (reader.ReadToFollowing(nameof(Pacient.Imya)))
                                {
                                    pacient.Imya = reader.ReadElementContentAsString();
                                }
                                if (reader.ReadToFollowing(nameof(Pacient.Otchestvo)))
                                {
                                    pacient.Otchestvo = reader.ReadElementContentAsString();
                                }
                                //if (reader.ReadToFollowing(nameof(Pacient.MedOrganization)))
                                //{
                                //    TODO pacient.MedOrganization = meds.GetById reader.ReadElementContentAsInt();
                                //}

                                PacientList.Add(pacient);
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



        public void PokazVsehPacientov(List<Pacient> pac)
        {
            foreach (Pacient item in pac)
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
