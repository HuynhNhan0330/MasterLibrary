// Updated by XamlIntelliSenseFileGenerator 12/7/2022 10:37:02 PM
#pragma checksum "..\..\..\..\Views\Customer\MainCustomerWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "14B9B339A64CD2C6788CA87620F751020C1DC1DF48F9466155FC0CF8091EC69A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Haley.Abstractions;
using Haley.Enums;
using Haley.Events;
using Haley.MVVM;
using Haley.MVVM.Converters;
using Haley.Models;
using Haley.Utils;
using Haley.WPF;
using Haley.WPF.Controls;
using MasterLibrary.UserControlML;
using MasterLibrary.Views.Customer;
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


namespace MasterLibrary.Views.Customer
{


    /// <summary>
    /// MainCustomerWindow
    /// </summary>
    public partial class MainCustomerWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden


#line 102 "..\..\..\..\Views\Customer\MainCustomerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton btnBuyBook;

#line default
#line hidden


#line 136 "..\..\..\..\Views\Customer\MainCustomerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton btnBookLocation;

#line default
#line hidden


#line 169 "..\..\..\..\Views\Customer\MainCustomerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton btnSetting;

#line default
#line hidden


#line 204 "..\..\..\..\Views\Customer\MainCustomerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton btnSignout;

#line default
#line hidden


#line 233 "..\..\..\..\Views\Customer\MainCustomerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock _CustomerName;

#line default
#line hidden


#line 274 "..\..\..\..\Views\Customer\MainCustomerWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame mainFrame;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MasterLibrary;component/views/customer/maincustomerwindow.xaml", System.UriKind.Relative);

#line 1 "..\..\..\..\Views\Customer\MainCustomerWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler)
        {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.MainCustomer = ((MasterLibrary.Views.Customer.MainCustomerWindow)(target));
                    return;
                case 2:
                    this.btnBuyBook = ((System.Windows.Controls.RadioButton)(target));
                    return;
                case 3:
                    this.btnBookLocation = ((System.Windows.Controls.RadioButton)(target));
                    return;
                case 4:
                    this.btnSetting = ((System.Windows.Controls.RadioButton)(target));
                    return;
                case 5:
                    this.btnSignout = ((System.Windows.Controls.RadioButton)(target));
                    return;
                case 6:
                    this._CustomerName = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 7:
                    this.mainFrame = ((System.Windows.Controls.Frame)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Window MainCustomer;
    }
}

