using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Xml;

namespace RDT.Models
{

    public class Category : DependencyObject
    {
        public string Name { get; set; }
        public List<Env> Members { get; set; }
    }

    public class Env : DependencyObject
    {
        public string Name { get; set; }
    }

    public class DataModel
    {
        XmlDocument xmldoc = new XmlDocument();
        XmlNodeList xmlnode;
        public bool AutoClose;
        string ConfigFile = AppDomain.CurrentDomain.BaseDirectory + @"\RDTConfig.xml";
        public string[] TargetsArray;
        public string Domain = "";
        public static string WebAddress = "";
        public static string UserName;
        public static ObservableCollection<Category> GeneralList { get; set; }
        public List<string> Passwd = new List<string>();
        public void FetchData()
        {
            GeneralList = new ObservableCollection<Category>();
            xmldoc.Load(ConfigFile);
            xmlnode = xmldoc.GetElementsByTagName("Settings");
            UserName = Environment.UserDomainName + @"\" + Environment.UserName;
            Domain = xmlnode[0].ChildNodes.Item(0).InnerText.Trim() + @"\";
            AutoClose = xmldoc.GetElementsByTagName("Settings")[0].ChildNodes.Item(1).InnerText.Trim().ToLower() == "true" ? true : false;
            WebAddress = xmldoc.GetElementsByTagName("Settings")[0].ChildNodes.Item(2).InnerText.Trim() ?? "https://github.com/";
            xmlnode = xmldoc.GetElementsByTagName("Category");
            for (int i = 0; i < xmlnode.Count; i++)
            {
                string cat = xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                string envs = xmlnode[i].ChildNodes.Item(1).InnerText.Trim();
                List<Env> members = new List<Env>();
                foreach (var item in envs.Split(';'))
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        Env e = new Env();
                        e.Name = item;
                        members.Add(e);
                    }
                }
                GeneralList.Add(new Category() { Name = cat, Members = members });
            }
            foreach (Category cat in GeneralList)
                foreach (Env ENV in cat.Members)
                    ENV.SetValue(ItemHelper.ParentProperty, cat);
        }

        public void loadSelectedTergets()
        {
            string Hosts = "";
            foreach (Category cat in GeneralList)
                foreach (Env env in cat.Members)
                    if (ItemHelper.GetIsChecked(env) == true)
                        Hosts += env.Name + ";";
            Hosts = Hosts.TrimEnd(new char[] { ';', ' ' });
            TargetsArray = Hosts.Split(';');
            TargetsArray = TargetsArray.Distinct().ToArray();
        }

    }

}
