﻿#pragma checksum "..\..\..\Window3.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9E4B5E27EE5B18050543F2B1A02AF79B00B05682"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
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
using Util.Controls;
using Util.Controls.WPFTest;


namespace Util.Controls.WPFTest {
    
    
    /// <summary>
    /// Window3
    /// </summary>
    public partial class Window3 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\Window3.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid gridList;
        
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
            System.Uri resourceLocater = new System.Uri("/Util.Controls.WPFTest;component/window3.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Window3.xaml"
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
            this.gridList = ((System.Windows.Controls.DataGrid)(target));
            
            #line 11 "..\..\..\Window3.xaml"
            this.gridList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.gridList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 32 "..\..\..\Window3.xaml"
            ((Util.Controls.FButton)(target)).Click += new System.Windows.RoutedEventHandler(this.FButton_Click_Fresh);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 33 "..\..\..\Window3.xaml"
            ((Util.Controls.FButton)(target)).Click += new System.Windows.RoutedEventHandler(this.FButton_Click_Add);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 34 "..\..\..\Window3.xaml"
            ((Util.Controls.FButton)(target)).Click += new System.Windows.RoutedEventHandler(this.FButton_Click_Delete);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 35 "..\..\..\Window3.xaml"
            ((Util.Controls.FButton)(target)).Click += new System.Windows.RoutedEventHandler(this.FButton_Click_Change);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 36 "..\..\..\Window3.xaml"
            ((Util.Controls.FButton)(target)).Click += new System.Windows.RoutedEventHandler(this.FButton_Click_Exit);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

