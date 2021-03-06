﻿using MedOrganization.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedOrganization.Module.Services;
using MedOrganization.Services;

namespace MedOrganization
{
    #region Вопросы
    /*
      1.	Назовите явное имя параметра, передаваемого в метод set свойства класса?

      2.	Что обозначает ключевое слово “virtual” для метода или свойства? --> В классе наледника можно переопределить методы, 
модификатор override. 
      3.	Чем перекрытый метод отличается от перегруженного метода? --> Перекрытие означает, 
что метод в производном классе скрывает метод с той же самой сигнатурой в базовом классе.
Поэтому в C# рекомендуется для методов производных классов, которые перекрывают методы базовых классов,
использовать ключевое слово new. Если вы забудете это сделать, то компилятор вас предупредит,
что вы скрываете одноименную функцию с той же сигнатурой в базовом классе.

      4.	Можно ли объявить перекрытый метод статическим, если перекрываемый метод не является статическим? --> 
Если в родительском классе объявить перекрытый метод статисеским,
то до него можно достучаться не создавая побъект от класса.

      5.	Что такое assembly? -->
В этом абстрактном классе содержатся статические методы, которые позволяют загружать сборку,
исследовать ее и производить с ней различные манипуляции.

Сборка (assembly) - один или несколько файлов, содержащий логический набор функциональности
(код и другие данные, связанные с кодом). Бывают статические сборки, хранящиеся на диске, и динамические,
которые создаются во время выполнения программы. Сборка - это базовый блок приложения, все ресурсы,
относящиеся к ней, доступны или только внутри этого блока, или экспортируются наружу.
При выполнении сборка задает область видимости имен и следит за ее соблюдением.

      6.	В чем различие между Value Type и Reference Type? -->

Value Type находятся в стеке, а Reference Type в куче.

Value Type - Типы значений: (хранятся в стеке)

Целочисленные типы (byte, sbyte, char, short, ushort, int, uint, long, ulong)
Типы с плавающей запятой (float, double)
Тип decimal
Тип bool
Перечисления enum
Структуры (struct)
============================
Reference Type - Ссылочные типы: (Хранятся в куче)

Тип object
Тип string
Классы (class)
Интерфейсы (interface)
Делегаты (delegate)

      7.	Когда объект удаляется сборщиком мусора? -->

Объект удаляется сборщиком мусора, когда на него не остается ссылок.

В методе Test создается объект Country. С помощью оператора new в куче для хранения объекта CLR выделяет участок памяти.
А в стек добавляет адрес на этот участок памяти. В главном методе Main мы вызываем метод Test.
И после того, как Test отработает, место в стеке очищается, а сборщик мусора очищает ранее выделенный под хранение объекта
country участок памяти.

Сборщик мусора не запускается сразу после удаления из стека ссылки на объект, размещенный в куче.
Он запускается в то время, когда среда CLR обнаружит в этом потребность, например, когда программе требуется дополнительная память.

Как правило, объекты в куче располагаются неупорядочено, между ними могут иметься пустоты.
Куча довольно сильно фрагментирована. Поэтому после очистки памяти в результате очередной сборки мусора
оставшиеся объекты перемещаются в один непрерывный блок памяти.
Вместе с этим происходит обновление ссылок, чтобы они правильно указывали на новые адреса объектов.

Поколение 0 - объекты не проверялись сборщиком мусора.
Поколение 1 - объекты пережившие одну проверку сборщика мусора, а так же объекты промеченные на удаления,
но не удалённые так-как в управляемой куче было достаточно свободного местаю.
Поколение 2 - объекты пережившие более одной проверки сборщика мутора.

      8.	В чем различие между Finalize и Dispose?

Dispose - обеспечивает явный контроль над ресурсами, используемыми объектом, а Finalize - неявный, используемый сборщиком мусора.

Dispose нужен для освобождения ресурсов "здесь и сейчас" 
(не совсем так на самом деле. Вызов Dispose сигнализирует, что вы хотите освободить ресурс, но не факт,
что это обязательно случится вот прямо тут же). Необходимость и преимущество интерфейса IDisposable именно в том,
что его реализация позволяет освобождать ресурсы не тогда, когда до них доберется сборщик мусора,
а тогда, когда это нужно программисту. Ресурсы могут быть дорогими, и держать их в памяти неопределенно
долгое время может быть слишком расточительным. Деструкторов в C# нет вовсе. Есть финализаторы.
Разница в том, что время вызова финализатора не определено. Стоит также отметить,
что если Dispose предназначен для вызова вручную, то финализатор вручную вызвать нельзя. Это делается автоматически.

Finalize выполняется перед уничтожением объекта. Можно сказать, что это "последний шанс" освободить ресурсы корректно.
Можно также считать, что Finalize - это "последняя воля умирающего" объекта.
Определять этот метод имеет смысл только в случае, если класс имеет доступ к каким-либо неуправляемым ресурсам.
Mетод Finalize() будет автоматически вызываться сборщиком мусора перед удалением соответствующего объекта из памяти.

9.	Что такое Boxing и Unboxing?

Упаковка (boxing) позволяет преобразовать размерный тип в ссылочный.
При упаковке объекта размерного типа происходят следующие действия:
1. Выделяется память в управляемой куче.
2. Совершается копирование полей размерного типа в память, которая была выделена в куче.
3. Возвращается адрес объекта.
Некоторые компиляторы автоматически создают IL-код, необходимый для упаковки объекта размерного типа.
Процесс извлечения адреса полей из упакованного объекта называется распаковкой (unboxing).
Распаковка не является полной противоположностью упаковке. В отличие от упаковки при распаковке не происходит никакого копирования.
Однако обычно вслед за распаковкой следует копирование полей, поэтому обе операции (распаковка и копирование) являются противоположностью операции упаковки.

По ходу написания кода может возникнуть ситуация, когда с переменной значимого типа приходится работать как 
с переменной ссылочного типа: например, если эту переменную нужно передать в метод, который принимает на вход ссылку, а не значение.
Просто передать переменную не получится, потому что принимающая сторона будет воспринимать ее значение как ссылку: например,
если в переменной значимого типа хранится значение "12345", то метод, считающий что ему дали ссылку подумает, что "12345" — это место,
в котором нужно искать собственно значение. Ничего хорошего из этого, сами понимаете, не выйдет.
В таких ситуациях и производится запаковка: создается дополнительный объект-обертка, в него помещается значение из переменной,
потом создается дополнительная переменная ссылочного типа, которая хранит ссылку на эту обертку, и уже такая переменная передается
в метод.
Сами видите — много лишних телодвижений, да плюс сборщик мусора, который следит за памятью, должен этот дополнительный объект
потом удалить, когда он уже станет не нужен.

Ну а распаковка — это обратный процесс, когда из переменной ссылочного типа вы извлекаете местоположение непосредственно данных,
создаете новую переменную значимого типа и копируете в нее эти данные.

     10.	Что такое CLR? Что такое IL? Что такое CLS?
CLR ("Common Language Runtime", "общеязыковая исполняющая среда") - это компонент .NET Framework,
основной задачей которого является управление интерпретацией и исполнением кода IL. 
CLR отвечает за изоляцию памяти приложений, проверку типов, безопасность кода, преобразование IL в машинный код.

IL (Intermediate Language) - код, содержащий набор инструкций, не зависящих от платформы.
Иными словами, после компиляции исходного кода он преобразуется не в код для какой-то определенной платформы,
а в промежуточный код на языке IL.

CLS ("Common Language Specification", общеязыковая спецификация) - это набор правил, 
следуя которым разработчики достигают бесконфликтной работы во всех языках .NET.

     11.	Что такое managed code?
Управляемый код (managed code) - это код, работающий в среде CLR.
Содержит метаданные, в которых находится информация для среды выполнения - о типах, членах и ссылках, используемых в коде.
         */
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            PacientServise ps = new PacientServise();
            MedOrgService ms = new MedOrgService();
            UserService us = new UserService();
            ServiceZakreplenie sz = new ServiceZakreplenie();
            us.Menu();
            //us.Menu2();
            ////Есть пациенты

            ////Есть мед организации


            //string message = "";
            //sz.Zakreplenie(ref ms, ref ps, out string message);

            //ms.Save();
            //ps.Save();

            //us.Generate();
            // us.Registration();
            //us.LoginService();

            //Console.WriteLine("Print");
            // us.PrintList();
            //Console.WriteLine("WriteToFileWithLogAndPass");
            //us.WriteToFileWithLogAndPass();
            //us.Save();
            //Console.WriteLine("ReadFromFileWithLogAndPass");
            //us.ReadFromFileWithLogAndPass();
            //us.Load();
        }
    }
}
