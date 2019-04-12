using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace MorninBytes.Models
{
    [XmlRoot("WebsitesConfig")]
    [XmlInclude(typeof(Websites))]
    public class WebsitesList
    {
        [XmlArray("WebsitesList")]
        //[XmlArrayItem("WebsiteElement")]
        public ObservableCollection<Websites> MyWebsites = new ObservableCollection<Websites>();

        [XmlElement("Listname")]
        public string Listname { get; set; }


        public WebsitesList() { }
        public WebsitesList FullList { get; set; }

        public ObservableCollection<Websites> GetList()
        {
            return MyWebsites;
        }

        public WebsitesList(string name)
        {
            this.Listname = name;
        }

        public void AddWebsite(Websites website)
        {
            MyWebsites.Add(website);
        }

        public void Set(ObservableCollection<Websites> x)
        {
            this.MyWebsites = x;
        }

        public bool DeleteWebsite(Websites website)
        {
            return MyWebsites.Remove(website);
        }

        public bool Contains(string dest)
        {
            if (MyWebsites.Any(p => p.Url == dest && p.IsEnabled))
            {
                return true;
            }
            return false;
        }

        public bool InsertAt(int index, Websites website)
        {
            try
            {
                MyWebsites.Insert(index, website);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
