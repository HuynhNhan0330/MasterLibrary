<<<<<<< HEAD
﻿#pragma checksum "..\..\..\..\..\Views\Customer\BuyBookPage\DetailBook.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D1D394C2041998EC54DB26168E61EB7F1D80AE4F18CD59D37987B4DD65805C9D"
=======
﻿#pragma checksum "..\..\..\..\..\Views\Customer\BuyBookPage\DetailBook.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8DBA63DE141CAA871279A1EB5A177D4DE1B886CD6D9870E9A99AB87BB24F863D"
>>>>>>> Import/main
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MasterLibrary.Views.Customer.BuyBookPage;
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


namespace MasterLibrary.Views.Customer.BuyBookPage {
    
    
    /// <summary>
    /// DetailBook
    /// </summary>
    public partial class DetailBook : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\..\..\Views\Customer\BuyBookPage\DetailBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MasterLibrary.Views.Customer.BuyBookPage.DetailBook detailbook;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\..\Views\Customer\BuyBookPage\DetailBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\..\Views\Customer\BuyBookPage\DetailBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Error;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\..\Views\Customer\BuyBookPage\DetailBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border lblMinus;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\..\..\Views\Customer\BuyBookPage\DetailBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txbQuantity;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\..\..\Views\Customer\BuyBookPage\DetailBook.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border lblPlus;
        
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
            System.Uri resourceLocater = new System.Uri("/MasterLibrary;component/views/customer/buybookpage/detailbook.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Customer\BuyBookPage\DetailBook.xaml"
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
            this.detailbook = ((MasterLibrary.Views.Customer.BuyBookPage.DetailBook)(target));
            return;
            case 2:
            this.lbl = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.Error = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.lblMinus = ((System.Windows.Controls.Border)(target));
            return;
            case 5:
            this.txbQuantity = ((System.Windows.Controls.TextBox)(target));
            
            #line 92 "..\..\..\..\..\Views\Customer\BuyBookPage\DetailBook.xaml"
            this.txbQuantity.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lblPlus = ((System.Windows.Controls.Border)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

