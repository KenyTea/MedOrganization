using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedOrganization.Module
{
    public enum PravaDostupa { User = 1, Admin }
    public class User
    {
        //3.	Пользователи(Логин, Пароль, Права доступа, Мед организация)
        public string Login { get; set; }
        public string Pass { get; set; }
        public PravaDostupa PravaDostupa_ { get; set; }
    }
}
