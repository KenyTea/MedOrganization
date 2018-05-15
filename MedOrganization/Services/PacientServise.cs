using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<Pacient> PacientList = new List<Pacient>();
        public PacientServise()
        {
            PacientGenerator();
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
                size = rnd.Next(1,20);

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
