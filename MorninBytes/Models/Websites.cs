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
        public enum Types {URI, FileSystem};

        //private readonly Func<string, string> _temp;
        private bool _isEnabled = true;
        private DateTime? _lastProcessed;
        private string _webSiteEditDisplay = "Collapsed";
        private string _webSiteTextDisplay = "Visible";
        private bool _webSiteEditFocus = false;
        private string _url = "";
        private string _type = "";
        private string _pathEdit = "";

        /* Default constructor */
        public Websites()
        {
            this.ID = "421415";
            this.Url = "www.google.com";
            this.PathEdit = this.Url;
            this.TextHighLights = "awaiting update";
            this.LastProcessed = new DateTime(1999, 11, 11, 12, 50, 50);
            this.IsEnabled = true;
        }

        public Websites(string url)
        {
            this.ID = Guid.NewGuid().ToString();
            this.Url = url;
            this.TextHighLights = "awaiting update";
            this.LastProcessed = null;
            this.IsEnabled = true;
        }
        
        [XmlAttribute("ID", DataType = "string")]
        //[XmlElement("ID")]
        public string ID { get; set; }

        //[XmlAttribute("Url", DataType = "string")]
        [XmlElement("Url")]
        public string Url {
            get { return this._url; }
            set {
                this._url = value;
                RaisePropertyChangedEvent("Url");
            }
        }

        [XmlElement("LastProcessed")]
        public DateTime? LastProcessed {
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

        [XmlElement("Type")]
        public string Type
        {
            get { return this._type; }
            set
            {
                this._type = value;
                RaisePropertyChangedEvent("Type");
            }
        }

        public string PathEdit
        {
            get { return this._pathEdit; }
            set
            {
                this._pathEdit = value;
                RaisePropertyChangedEvent("PathEdit");
            }
        }

        public ICommand WebsiteEdit_DoubleClick
        {
            get { return new DelegateCommand<object>(ShowWebsiteEdit); }
        }

        public ICommand WebsiteEdit_LostFocus
        {
            get { return new DelegateCommand<object>(WebsiteCloseEdit); }
        }
        public ICommand WebsiteEdit_ExitNoSave
        {
            get { return new DelegateCommand<object>(WebsiteCloseEdit); }
        }


        public ICommand WebsiteEdit_ExitUpdate
        {
            get { return new DelegateCommand<object>(UpdateUrl); }
        }

        private void UpdateUrl()
        {
            //var temp = this.Url;
            Url = PathEdit.Trim();
            //validate URL again
            
            //remove focus from the textbox
            WebsiteCloseEdit();
        }

        private void ShowWebsiteEdit()
        {
            
            PathEdit = Url;
            WebsiteEditDisplay = "Visible";
            WebsiteTextDisplay = "Collapsed";
            WebsiteEditFocus = true;
        }

        private void WebsiteCloseEdit()
        {
            WebsiteEditDisplay = "Collapsed";
            WebsiteTextDisplay = "Visible";
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
