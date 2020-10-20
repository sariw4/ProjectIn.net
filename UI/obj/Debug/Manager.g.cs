﻿#pragma checksum "..\..\Manager.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4F95FA66B865A7576E1D0D8F358379B489919BAA33C9CBB77AA0BD67910763F5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using UI;


namespace UI {
    
    
    /// <summary>
    /// Manager
    /// </summary>
    public partial class Manager : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 32 "..\..\Manager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button backm;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\Manager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ManagerPassword;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\Manager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordManager;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\Manager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ManagerMain;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\Manager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid hostingdata;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\Manager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox combo_hosting;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\Manager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox combo_hosting_name;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\Manager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid guestdata;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\Manager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox combo_guest;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\Manager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox combo_guest_name;
        
        #line default
        #line hidden
        
        
        #line 147 "..\..\Manager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid orderdata;
        
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
            System.Uri resourceLocater = new System.Uri("/UI;component/manager.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Manager.xaml"
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
            this.backm = ((System.Windows.Controls.Button)(target));
            return;
            case 2:
            this.ManagerPassword = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.PasswordManager = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 4:
            
            #line 48 "..\..\Manager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_Manager);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ManagerMain = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.hostingdata = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 10:
            this.combo_hosting = ((System.Windows.Controls.ComboBox)(target));
            
            #line 102 "..\..\Manager.xaml"
            this.combo_hosting.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Combo_hosting_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.combo_hosting_name = ((System.Windows.Controls.ComboBox)(target));
            
            #line 104 "..\..\Manager.xaml"
            this.combo_hosting_name.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Combo_hosting_name_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.guestdata = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 14:
            this.combo_guest = ((System.Windows.Controls.ComboBox)(target));
            
            #line 133 "..\..\Manager.xaml"
            this.combo_guest.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Combo_guest_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 15:
            this.combo_guest_name = ((System.Windows.Controls.ComboBox)(target));
            
            #line 135 "..\..\Manager.xaml"
            this.combo_guest_name.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Combo_guest_name_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 16:
            this.orderdata = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 7:
            
            #line 81 "..\..\Manager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.more_details_clickH);
            
            #line default
            #line hidden
            break;
            case 8:
            
            #line 88 "..\..\Manager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.more_details_clickH1);
            
            #line default
            #line hidden
            break;
            case 9:
            
            #line 95 "..\..\Manager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.more_details_clickH2);
            
            #line default
            #line hidden
            break;
            case 13:
            
            #line 126 "..\..\Manager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.more_details_clickg);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

