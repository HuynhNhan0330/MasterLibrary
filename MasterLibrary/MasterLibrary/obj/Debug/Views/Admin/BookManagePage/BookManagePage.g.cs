﻿#pragma checksum "..\..\..\..\..\Views\Admin\BookManagePage\BookManagePage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C371A89B818E9F28FA700E285CC04274E610106E97D8C54348E5E8FA37FE48BE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MasterLibrary.Views.Admin.BookManagePage;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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


namespace MasterLibrary.Views.Admin.BookManagePage {
    
    
    /// <summary>
    /// BookManagePage
    /// </summary>
    public partial class BookManagePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\..\..\Views\Admin\BookManagePage\BookManagePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MasterLibrary.Views.Admin.BookManagePage.BookManagePage BookManagePageML;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\..\Views\Admin\BookManagePage\BookManagePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txbFilter;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\..\..\Views\Admin\BookManagePage\BookManagePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listview_managebook;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\..\..\Views\Admin\BookManagePage\BookManagePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Deleting;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\..\..\Views\Admin\BookManagePage\BookManagePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Updating;
        
        #line default
        #line hidden
        
        
        #line 234 "..\..\..\..\..\Views\Admin\BookManagePage\BookManagePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Adding;
        
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
            System.Uri resourceLocater = new System.Uri("/MasterLibrary;component/views/admin/bookmanagepage/bookmanagepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Admin\BookManagePage\BookManagePage.xaml"
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
            this.BookManagePageML = ((MasterLibrary.Views.Admin.BookManagePage.BookManagePage)(target));
            return;
            case 2:
            this.txbFilter = ((System.Windows.Controls.TextBox)(target));
            
            #line 78 "..\..\..\..\..\Views\Admin\BookManagePage\BookManagePage.xaml"
            this.txbFilter.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged_Find);
            
            #line default
            #line hidden
            return;
            case 3:
            this.listview_managebook = ((System.Windows.Controls.ListView)(target));
            return;
            case 4:
            this.Deleting = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 5:
            this.Updating = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 6:
            this.Adding = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

