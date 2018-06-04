using MedOrganization.Module.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MedOrganization.Module
{
    public class ServiceZakreplenie
    {
        //public MedOrgService MedOrgServiceStat { get; set; }
        //public PacientServise PacientServiseStat { get; set; }
        List<Pacient> pricrepPacient = new List<Pacient>();
        List<MedOrganization> pricrepMedOrgan = new List<MedOrganization>();

        public bool Zakreplenie(/*ref MedOrgService ms, ref PacientServise ps,*/ out string mesage)
        {
            #region Select Pation
            //foreach (Pacient p in PacientServiseStat.PacientList.Take(10))
            foreach (Pacient p in PacientServise.Instance.PacientList.Take(10))
                p.PacientInfo();

            Console.WriteLine("           --------------------------------------");
            Pacient selectPation = new Pacient();
            Console.WriteLine("                 Select patient, enter IIN");
           
            int selectIin = Int32.Parse(Console.ReadLine());
            //selectPation = PacientServiseStat[selectIin];
            selectPation = PacientServise.Instance[selectIin];
            Console.WriteLine("          --------------------------------------");
            if (selectPation == null)
            {
                mesage = "                   This patient does not exist!";
                return false;
            }

            #endregion
            #region Select MedOrg
            //foreach (MedOrganization o in MedOrgServiceStat.MedOrgList.Take(10))
            foreach (MedOrganization o in MedOrgService.Instance.MedOrgList.Take(10))
                o.MedOrganizationInfo();

            Console.WriteLine("           --------------------------------------");
            MedOrganization selectOrg = new MedOrganization();
            Console.WriteLine("               Select med organization, enter id");
            int selectOrgId = Int32.Parse(Console.ReadLine());
            //selectOrg = MedOrgServiceStat[selectOrgId];
            selectOrg = MedOrgService.Instance[selectOrgId];
            Console.WriteLine("           --------------------------------------");

            #endregion

            Console.Clear();


            //var so = MedOrgServiceStat.MedOrgList.FirstOrDefault(f => f.Id == selectOrgId);
            var so = MedOrgService.Instance.MedOrgList.FirstOrDefault(f => f.Id == selectOrgId);
            foreach (var p in so.PacientList)
            {
                if (p.IIN == selectIin)
                {
                    mesage = "This patient is already attached!";
                    Console.WriteLine("This patient is already attached!");
                    return false;
                }
            }

            so.PacientList.Add(selectPation);
            //PacientServiseStat.PacientList.FirstOrDefault(f => f.IIN == selectIin)
            PacientServise.Instance.PacientList.FirstOrDefault(f => f.IIN == selectIin)
                .MedOrganization = selectOrg;

            mesage = "This patient is successfully attached!";
            Console.WriteLine("This patient is successfully attached!");
            //MedOrgService.Instance.PokazVsehOrg();
            return true;
          
        }

        public void SaveZakreplenir()
        {
            ZapropsyNaZakreplenie zpz = new ZapropsyNaZakreplenie();

            string path = @"Zpz.xml";

            var document = new XmlDocument();
            var xmlDeclaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            var root = document.DocumentElement;
            document.InsertBefore(xmlDeclaration, root);
            var userList = document.CreateElement(nameof(PacientServise.Instance.PacientList));

            foreach (var us in PacientServise.Instance.PacientList)
            {
                var nodeU = document.CreateElement(nameof(User));
                nodeU.SetAttribute(nameof(User.Login), us.Login);
                nodeU.SetAttribute(nameof(User.Pass), us.Pass);
                nodeU.SetAttribute(nameof(User.PravaDostupa_), (us.PravaDostupa_).ToString());
                userList.AppendChild(nodeU);
            }
            document.AppendChild(userList);
            document.Save(path);
        }

        #region
        //public bool Zakreplenie(ref MedOrgService ms, ref PacientServise ps, out string mesage)
        //{
        //    #region Select Pation
        //    foreach (Pacient p in ps.PacientList.Take(10))
        //        p.PacientInfo();

        //    Console.WriteLine("           --------------------------------------");
        //    Pacient selectPation = new Pacient();
        //    Console.WriteLine("                 Select patient, enter IIN");

        //    int selectIin = Int32.Parse(Console.ReadLine());
        //    selectPation = ps[selectIin];
        //    Console.WriteLine("          --------------------------------------");
        //    if (selectPation == null)
        //    {
        //        mesage = "                   This patient does not exist!";
        //        return false;
        //    }

        //    #endregion
        //    #region Select MedOrg
        //    foreach (MedOrganization o in ms.MedOrgList.Take(10))
        //        o.MedOrganizationInfo();

        //    Console.WriteLine("           --------------------------------------");
        //    MedOrganization selectOrg = new MedOrganization();
        //    Console.WriteLine("                   Select med organization");
        //    int selectOrgId = Int32.Parse(Console.ReadLine());
        //    selectOrg = ms[selectOrgId];
        //    Console.WriteLine("           --------------------------------------");

        //    #endregion

        //    Console.Clear();


        //    var so = ms.MedOrgList.FirstOrDefault(f => f.Id == selectOrgId);

        //    foreach (var p in so.PacientList)
        //    {
        //        if (p.IIN == selectIin)
        //        {
        //            mesage = "This patient is already attached!";
        //            return false;
        //        }
        //    }

        //    so.PacientList.Add(selectPation);
        //    ps.PacientList.FirstOrDefault(f => f.IIN == selectIin)
        //        .MedOrganization = selectOrg;

        //    mesage = "This patient is successfully attached!";
        //    return true;

        //    //else if(select == 2)
        //    //{
        //    //    var so = ms.MedOrgList.FirstOrDefault(f => f.Id == selectOrgId);

        //    //    foreach (var p in so.PacientList)
        //    //    {
        //    //        if (p.Imya == name)
        //    //        {
        //    //            mesage = "Данный пациент уже прикреплен!";
        //    //            return false;
        //    //        }
        //    //    }

        //    //    so.PacientList.Add(selectPation);
        //    //    ps.PacientList.FirstOrDefault(f => f.Imya == name)
        //    //        .MedOrganization = selectOrg;

        //    //    //mesage = "Данный пациент прикреплен успешно!";
        //    //    //return true;
        //    //}
        //    //mesage = "Данный пациент прикреплен успешно!";
        //    //return true;
        //}
        #endregion
    }
}

