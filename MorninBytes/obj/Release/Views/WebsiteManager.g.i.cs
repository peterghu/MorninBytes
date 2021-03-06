﻿#pragma checksum "..\..\..\Views\WebsiteManager.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7946F800D7A36B9F45C77F1ABEE5D8954F8BF782"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MorninBytes.Extensions;
using MorninBytes.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace MorninBytes.Views {
    
    
    /// <summary>
    /// WebsiteManager
    /// </summary>
    public partial class WebsiteManager : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\Views\WebsiteManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtConfigFilePath;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Views\WebsiteManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnLoadWebsites;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Views\WebsiteManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnSaveWebsites;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Views\WebsiteManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListWebsites;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Views\WebsiteManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContextMenu UrlCM;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\Views\WebsiteManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnOpenWebsites;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\..\Views\WebsiteManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnCancelOpenWebsites;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MorninBytes;component/views/websitemanager.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\WebsiteManager.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TxtConfigFilePath = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.BtnLoadWebsites = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.BtnSaveWebsites = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.ListWebsites = ((System.Windows.Controls.ListView)(target));
            
            #line 47 "..\..\..\Views\WebsiteManager.xaml"
            this.ListWebsites.DragEnter += new System.Windows.DragEventHandler(this.ListViewDragEnter);
            
            #line default
            #line hidden
            
            #line 47 "..\..\..\Views\WebsiteManager.xaml"
            this.ListWebsites.Drop += new System.Windows.DragEventHandler(this.ListViewDrop);
            
            #line default
            #line hidden
            
            #line 48 "..\..\..\Views\WebsiteManager.xaml"
            this.ListWebsites.PreviewMouseMove += new System.Windows.Input.MouseEventHandler(this.ListViewPreviewMouseMove);
            
            #line default
            #line hidden
            
            #line 49 "..\..\..\Views\WebsiteManager.xaml"
            this.ListWebsites.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.ListViewPreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.UrlCM = ((System.Windows.Controls.ContextMenu)(target));
            return;
            case 6:
            
            #line 132 "..\..\..\Views\WebsiteManager.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 7:
            this.BtnOpenWebsites = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.BtnCancelOpenWebsites = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

