using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Microsoft.Win32;
using MorninBytes.Views;
using MorninBytes.Models;
using MorninBytes.ViewModels;

/* TODO
 * 
 check for duplicate websites
 add settings to remove overwrite prompt
 */

namespace MorninBytes.ViewModels
{
    public class WebsiteUIViewModel : ObservableObject
    {
        public const string DefaultUserSettingsFilename = "settings.xml";

        private string _newSite;
        private int _siteDelay = 10;
        private float _webSiteProgressBar = 0;
        private string _configFilePath = "";
        private string _configFilePathText = "";
        private string _lblAddWebsiteStatus;
        private string _lblConfigPathStatus;
        private WebsitesList _myWebsiteList = new WebsitesList();
        private string _lblManagerStatus;

        public IAsyncCommand OpenWebsitesCommand { get; private set; }


        public WebsiteUIViewModel()
        {
            OpenWebsitesCommand = AsyncCommand.Create(token => OpenWebsitesAsync(_siteDelay, MyWebsiteList, token));

            SiteDelay = Properties.Settings.Default.WebsiteDelay;

            if (Properties.Settings.Default.WebsiteConfigPath == "")
                Properties.Settings.Default.WebsiteConfigPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            ConfigFilePath = Properties.Settings.Default.WebsiteConfigPath;
            ConfigFilePathText = ConfigFilePath;

            if (ConfigFilePath.Contains("xml"))
                DeserializeSettings();

        }

        public async Task<int> OpenWebsitesAsync(
            int delay, ObservableCollection<Websites> websitesList, CancellationToken token = new CancellationToken())
        {

            await Task.Delay(TimeSpan.FromSeconds(2), token).ConfigureAwait(false);
            delay = delay * 1000;
            string url = "http://www.google.com";
            float websiteCount = websitesList.Count;

            for (int i = 0; i < websitesList.Count; i++)
            {
                Websites web = websitesList.ElementAt(i);

                if (web.IsEnabled)
                {
                    System.Diagnostics.Process.Start(web.Url);
                    UpdateProgress(i, websiteCount);
                    LblManagerStatus = "Opened website: " + web.Url;
                    web.LastProcessed = DateTime.Now;
                    Thread.Sleep(delay);
                }

                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }
            }

            WebSiteProgressBar = 0;

            var client = new HttpClient();
            using (var response = await client.GetAsync(url, token).ConfigureAwait(false))
            {
                var data = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                return data.Length;
            }
        }


        public float WebSiteProgressBar
        {
            get { return this._webSiteProgressBar; }
            set
            {
                _webSiteProgressBar = value;
                RaisePropertyChangedEvent("WebSiteProgressBar");
            }
        }


        public string NewSite
        {
            get { return _newSite; }
            set
            {
                _newSite = value;
                RaisePropertyChangedEvent("NewSite");
            }
        }

        public int SiteDelay
        {
            get { return _siteDelay; }
            set
            {
                _siteDelay = value;
                Properties.Settings.Default.WebsiteDelay = _siteDelay;
                Properties.Settings.Default.Save();
                RaisePropertyChangedEvent("SiteDelay");
            }
        }

        public string ConfigFilePath
        {
            get { return _configFilePath; }
            set
            {
                _configFilePath = value;
                Properties.Settings.Default.WebsiteConfigPath = _configFilePath;
                Properties.Settings.Default.Save();
                RaisePropertyChangedEvent("ConfigFilePath");
            }
        }

        public string ConfigFilePathText
        {
            get { return _configFilePathText; }
            set
            {
                _configFilePathText = value;
                RaisePropertyChangedEvent("ConfigFilePathText");
            }
        }

        public string LblConfigPathStatus
        {
            get { return _lblConfigPathStatus; }
            set
            {
                _lblConfigPathStatus = value;
                RaisePropertyChangedEvent("LblConfigPathStatus");
            }
        }

        public string LblManagerStatus
        {
            get { return _lblManagerStatus; }
            set
            {
                _lblManagerStatus = value;
                RaisePropertyChangedEvent("LblManagerStatus");
            }
        }

        public ObservableCollection<Websites> MyWebsiteList
        {
            get { return _myWebsiteList.GetList(); }
            set
            {
                _myWebsiteList.Set(value);
                RaisePropertyChangedEvent("MyWebsiteList");
            }
        }

        /* ICommands */
        public ICommand AddWebsiteToList
        {
            get { return new DelegateCommand<object>(ProcessUri); }
        }



        public ICommand SaveMySettingsCmd
        {
            get { return new DelegateCommand<object>(SerializeSettings); }
        }

        public ICommand OpenMySettingsCmd
        {
            get { return new DelegateCommand<object>(DeserializeSettings); }
        }

        public ICommand OpenConfigFileCmd
        {
            get { return new DelegateCommand<object>(OpenConfigFile); }
        }

        public ICommand UpdateFilePathTextCmd
        {
            get { return new DelegateCommand<object>(UpdateConfigFileText); }
        }

        public ICommand RemoveWebsiteCmd
        {
            get { return new DelegateCommand<object>(RemoveWebsite); }
        }

        public ICommand AboutMenu_Click
        {
            get { return new DelegateCommand<object>(OpenHelpWindow); }
        }

        public ICommand ExitMenu_Click
        {
            get { return new DelegateCommand<object>(ExitWindow); }
        }


        /* Delegate Commands */
        private void OpenConfigFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";


            if (ConfigFilePathText.Contains("\\"))
            {
                openFileDialog.InitialDirectory = ConfigFilePathText.Remove(ConfigFilePathText.LastIndexOf("\\"));
            }
            else
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


            if (openFileDialog.ShowDialog() == true)
            {
                ConfigFilePathText = openFileDialog.FileName;
            }
        }

        private void UpdateConfigFileText(object parameter)
        {
            _configFilePath = (string)parameter;
            RaisePropertyChangedEvent("ConfigFilePath");
        }

        private void SerializeSettings()
        {
            string dest = ConfigFilePathText;
            string newfile = dest.Substring(dest.LastIndexOf("\\") + 1);
            bool proceed = true;

            Type[] myTypes = { typeof(Websites) };
            
            try
            {
                if (File.Exists(dest))
                {
                    string sMessageBoxText = String.Format("Overwrite the file \"{0}\"?", newfile);

                    switch (MsgBoxConfigSave(sMessageBoxText))
                    {
                        case MessageBoxResult.OK:
                            //
                            break;
                        case MessageBoxResult.Cancel:
                            proceed = false;
                            break;
                    }
                }

                if (proceed)
                {
                    if (!dest.Contains(".xml"))
                    {
                        dest = dest + ".xml";
                        newfile = newfile + ".xml";
                    }

                    FileStream fs = new FileStream(dest, FileMode.Create);
                    XmlSerializer serializer = new XmlSerializer(typeof(WebsitesList), myTypes);
                    serializer.Serialize(fs, _myWebsiteList);
                    UpdateManagerStatus(String.Format("Config XML \"{0}\" successfully saved!", newfile));
                    fs.Close();
                    ConfigFilePath = ConfigFilePathText;
                }

            }
            catch (Exception ex)
            {
                LblConfigPathStatus = ex.Message;
                UpdateManagerStatus("Error saving config XML file.");
            }
        }

        private MessageBoxResult MsgBoxConfigSave(string x)
        {
            string sCaption = "MorninBytes";

            MessageBoxButton btnMessageBox = MessageBoxButton.OKCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;
            MessageBoxResult rsltMessageBox = MessageBox.Show(x, sCaption, btnMessageBox, icnMessageBox);

            return rsltMessageBox;
        }

        private void UpdateManagerStatus(string input)
        {
            LblManagerStatus = input;
        }

        private void DeserializeSettings()
        {
            //TODO: disable load config button until this is completed

            Type[] myTypes = { typeof(Websites) };

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(WebsitesList), myTypes);
                FileStream fs = new FileStream(ConfigFilePathText, FileMode.Open);
                WebsitesList x = (WebsitesList)serializer.Deserialize(fs);
                MyWebsiteList = x.GetList();
                fs.Close();
                LblConfigPathStatus = "";
                ConfigFilePath = ConfigFilePathText;
                UpdateManagerStatus(String.Format("Successfully loaded file \"{0}\"", ConfigFilePathText.Substring(ConfigFilePathText.LastIndexOf("\\") + 1)));
                //serializer.Serialize(Console.Out, _myWebsiteList);
                //Console.ReadLine();
            }
            catch (Exception ex)
            {
                LblConfigPathStatus = ex.Message;
                UpdateManagerStatus("Error loading config file");
                //LblConfigPathStatus = "Error: Invalid file specified";
            }

        }

        public void UrlListClick()
        {
            System.Windows.MessageBox.Show("hello");
        }


        public string LblAddWebsiteStatus
        {
            get { return _lblAddWebsiteStatus; }
            set
            {
                _lblAddWebsiteStatus = value;
                RaisePropertyChangedEvent("LblAddWebsiteStatus");
            }
        }

        private void ProcessUri()
        {
            var tempSite = NewSite;

            if (!NewSite.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                tempSite = "http://" + NewSite;

            if (!IsValidUri(tempSite))
                return;

            AddToSiteList(tempSite);
            NewSite = string.Empty;
        }

        public bool IsValidUri(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                LblAddWebsiteStatus = "Please enter a website!";
                return false;
            }
            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            {
                LblAddWebsiteStatus = "Invalid URI entered";
                return false;
            }
            Uri tmp;
            if (!Uri.TryCreate(uri, UriKind.Absolute, out tmp) || !(tmp.Scheme == Uri.UriSchemeHttp || tmp.Scheme == Uri.UriSchemeHttps))
            {
                LblAddWebsiteStatus = "Invalid URI protocol";
                return false;
            }

            return true;
        }

        private void AddToSiteList(string item)
        {
            Websites x = new Websites(item, 5);
            _myWebsiteList.AddWebsite(x);

            /*
            if (!_siteList.Contains(item))
            {
                _siteList.Add(item);
            }*/
        }

        private void UpdateProgress(int i, float size)
        {
            float x = ((float)(i + 1) / size) * 100;
            WebSiteProgressBar = x;
        }
        
        private void RemoveWebsite(object o)
        {
            Websites tmp = (Websites)o;

            if (_myWebsiteList.DeleteWebsite(tmp))
            {
                UpdateManagerStatus(String.Format("Removed the URL \"{0}\"", tmp.Url));
            }
            else
            {
                UpdateManagerStatus(String.Format("Failed to remove the URL \"{0}\"", tmp.Url));
            }
        }


        private void OpenHelpWindow()
        {
            HelpWindow help = new HelpWindow();
            help.Show();
        }

        private void ExitWindow()
        {
            System.Windows.Application.Current.Shutdown();
        }

    }
}
