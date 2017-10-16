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
            bool ret = MyWebsites.Remove(website);
            return ret;
        }

    }
}
