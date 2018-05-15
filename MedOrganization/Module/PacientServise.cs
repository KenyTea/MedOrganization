using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedOrganization.Module
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
        List<Pacient> pac = new List<Pacient>();
        public void PacientGenerator(int size)
        {
            Random r = new Random();
            for (int i = 0; i < size; i++)
            {
                string Familiya = ((LastName)r.Next(1, 8)).ToString();
                string Imya = ((Name)r.Next(1, 8)).ToString();
                string Otchestvo = ((Otchestvo)r.Next(1, 8)).ToString();
                int IIN = r.Next(100000, 999999);
                Pacient newPac = new Pacient(Familiya, Imya, Otchestvo, IIN);
                pac.Add(newPac);
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
            foreach (Pacient item in pac)
            {
                if (item.IIN == iin)
                {
                    item.PacientInfo();
                    yes = true;
                    return item;
                }
            }

            if (!yes)
            {
                Console.WriteLine("Takogo pacienta net=(");
                return null;
            }
        }

    }
}
