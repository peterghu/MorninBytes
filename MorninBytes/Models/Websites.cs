using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Xml.Serialization;
using MorninBytes.ViewModels;
using System.Windows.Input;

namespace MorninBytes.Models
{
    [XmlType("Websites")]
    public class Websites : ObservableObject
    {
        //private readonly Func<string, string> _temp;
        private bool _isEnabled = true;
        private DateTime _lastProcessed;
        private string _webSiteEditDisplay = "Collapsed";
        private string _webSiteTextDisplay = "Visible";
        private bool _webSiteEditFocus = false;
        private string _url = "";

        public Websites()
        {
            this.ID = 421415;
            this.Url = "www.google.com";
            this.TextHighLights = "awaiting update";
            this.LastProcessed = new DateTime(1999, 11, 11, 12, 50, 50);
            this.IsEnabled = true;
        }



        /*
        public WebsiteVisit(Func<string, string> convertion)
        {
            _temp = convertion;
        }
        */

        public Websites(string url, int id)
        {
            this.ID = id;
            this.Url = url;
            this.TextHighLights = "awaiting update";
            this.LastProcessed = new DateTime(1999, 11, 11, 12, 50, 50);
            this.IsEnabled = true;
        }

        //public string ConvertText(string inputText)
        // {
        //     return _temp(inputText);
        // }
        
        [XmlAttribute("ID", DataType = "int")]
        //[XmlElement("ID")]
        public int ID { get; set; }

        //[XmlAttribute("Url", DataType = "string")]
        [XmlElement("Url")]
        public string Url { get { return this._url; }
            set {
                this._url = value;
                RaisePropertyChangedEvent("Url");
            }
        }

        [XmlElement("LastProcessed")]
        public DateTime LastProcessed {
            get { return _lastProcessed; }
            set
            {
                _lastProcessed = value;
                RaisePropertyChangedEvent("LastProcessed");
            }
        }

        [XmlElement("TextHighLights")]
        public string TextHighLights { get; set; }

        [XmlElement("IsEnabled")]
        public bool IsEnabled {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                RaisePropertyChangedEvent("IsEnabled");
            }
        }

        public ICommand WebsiteEdit_DoubleClick
        {
            get { return new DelegateCommand<object>(WebsiteEdit); }
        }

        public ICommand WebsiteEdit_LostFocus
        {
            get { return new DelegateCommand<object>(WebsiteCloseEdit); }
        }

        public ICommand UpdateUrlCmd
        {
            get { return new DelegateCommand<object>(UpdateUrl); }
        }

        private void UpdateUrl()
        {
            var temp = this.Url;
            
            //validate URL again


            //remove focus from the textbox
            WebsiteCloseEdit();
        }

        private void WebsiteEdit()
        {
            WebsiteEditDisplay = "Visible";
            WebsiteTextDisplay = "Collapsed";
            WebsiteEditFocus = true;
        }


        private void WebsiteCloseEdit()
        {
            WebsiteTextDisplay = "Visible";
            WebsiteEditDisplay = "Collapsed";
            WebsiteEditFocus = false;

        }

        public string WebsiteEditDisplay
        {
            get { return _webSiteEditDisplay; }
            set
            {
                _webSiteEditDisplay = value;
                RaisePropertyChangedEvent("WebsiteEditDisplay");
            }
        }

        public bool WebsiteEditFocus
        {
            get { return _webSiteEditFocus; }
            set
            {
                _webSiteEditFocus = value;
                RaisePropertyChangedEvent("WebsiteEditFocus");
            }
        }

        public string WebsiteTextDisplay
        {
            get { return _webSiteTextDisplay; }
            set
            {
                _webSiteTextDisplay = value;
                RaisePropertyChangedEvent("WebsiteTextDisplay");
            }
        }
    }

    
}
