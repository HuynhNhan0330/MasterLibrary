﻿#pragma checksum "..\..\..\..\..\Views\Admin\HistoryPage\ExpenseDetail.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7F5878E6EBA895F38DD22F72788E24F0BB71CD602DA9C7543D5001CF6949359C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MasterLibrary.Views.Admin.HistoryPage;
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


namespace MasterLibrary.Views.Admin.HistoryPage {
    
    
    /// <summary>
    /// ExpenseDetail
    /// </summary>
    public partial class ExpenseDetail : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\..\..\Views\Admin\HistoryPage\ExpenseDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MasterLibrary.Views.Admin.HistoryPage.ExpenseDetail ExpenseDetailML;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\..\..\Views\Admin\HistoryPage\ExpenseDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label bookNameInput;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\..\..\Views\Admin\HistoryPage\ExpenseDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label authorNameInput;
        
        #line default
        #line hidden
        
        
        #line 132 "..\..\..\..\..\Views\Admin\HistoryPage\ExpenseDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label debutDayInput;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\..\..\..\Views\Admin\HistoryPage\ExpenseDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label bookPriceInput;
        
        #line default
        #line hidden
        
        
        #line 158 "..\..\..\..\..\Views\Admin\HistoryPage\ExpenseDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label quantityInput;
        
        #line default
        #line hidden
        
        
        #line 176 "..\..\..\..\..\Views\Admin\HistoryPage\ExpenseDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label dateInput;
        
        #line default
        #line hidden
        
        
        #line 185 "..\..\..\..\..\Views\Admin\HistoryPage\ExpenseDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label sumBillInput;
        
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
            System.Uri resourceLocater = new System.Uri("/MasterLibrary;component/views/admin/historypage/expensedetail.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Admin\HistoryPage\ExpenseDetail.xaml"
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
            this.ExpenseDetailML = ((MasterLibrary.Views.Admin.HistoryPage.ExpenseDetail)(target));
            return;
            case 2:
            
            #line 65 "..\..\..\..\..\Views\Admin\HistoryPage\ExpenseDetail.xaml"
            ((System.Windows.Controls.Button)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.Button_MouseEnter);
            
            #line default
            #line hidden
            
            #line 66 "..\..\..\..\..\Views\Admin\HistoryPage\ExpenseDetail.xaml"
            ((System.Windows.Controls.Button)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.Button_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 3:
            this.bookNameInput = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.authorNameInput = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.debutDayInput = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.bookPriceInput = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.quantityInput = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.dateInput = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.sumBillInput = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

