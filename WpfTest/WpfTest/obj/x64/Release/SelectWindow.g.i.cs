﻿#pragma checksum "..\..\..\SelectWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "27268AF06E023BF24F1DE6791FD86D482B4B127E2566E86DEEF8C69879E0B9D3"
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
using System.Windows.Forms.Integration;
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
using WpfTest;


namespace WpfTest {
    
    
    /// <summary>
    /// SelectWindow
    /// </summary>
    public partial class SelectWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 413 "..\..\..\SelectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LonginWindowLabel;
        
        #line default
        #line hidden
        
        
        #line 416 "..\..\..\SelectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SettingBtn;
        
        #line default
        #line hidden
        
        
        #line 419 "..\..\..\SelectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MinBtn;
        
        #line default
        #line hidden
        
        
        #line 422 "..\..\..\SelectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MaxBtn;
        
        #line default
        #line hidden
        
        
        #line 425 "..\..\..\SelectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExitBtn;
        
        #line default
        #line hidden
        
        
        #line 445 "..\..\..\SelectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button joinRoomBtn;
        
        #line default
        #line hidden
        
        
        #line 451 "..\..\..\SelectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button createRoomBtn;
        
        #line default
        #line hidden
        
        
        #line 466 "..\..\..\SelectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox classInfoListBox;
        
        #line default
        #line hidden
        
        
        #line 467 "..\..\..\SelectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox classStuInfoListBox;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfTest;component/selectwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\SelectWindow.xaml"
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
            
            #line 9 "..\..\..\SelectWindow.xaml"
            ((WpfTest.SelectWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.onLoad);
            
            #line default
            #line hidden
            return;
            case 9:
            this.LonginWindowLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.SettingBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 11:
            this.MinBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 12:
            this.MaxBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 13:
            this.ExitBtn = ((System.Windows.Controls.Button)(target));
            
            #line 426 "..\..\..\SelectWindow.xaml"
            this.ExitBtn.Click += new System.Windows.RoutedEventHandler(this.ExitBtn_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.joinRoomBtn = ((System.Windows.Controls.Button)(target));
            
            #line 445 "..\..\..\SelectWindow.xaml"
            this.joinRoomBtn.Click += new System.Windows.RoutedEventHandler(this.JoinRoomBtn_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.createRoomBtn = ((System.Windows.Controls.Button)(target));
            
            #line 451 "..\..\..\SelectWindow.xaml"
            this.createRoomBtn.Click += new System.Windows.RoutedEventHandler(this.CreateRoomBtn_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.classInfoListBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 466 "..\..\..\SelectWindow.xaml"
            this.classInfoListBox.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ClassInfoListBox_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 17:
            this.classStuInfoListBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 467 "..\..\..\SelectWindow.xaml"
            this.classStuInfoListBox.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ClassInfoListBox_MouseDoubleClick);
            
            #line default
            #line hidden
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
            case 2:
            
            #line 366 "..\..\..\SelectWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.StartClassBtn_Click);
            
            #line default
            #line hidden
            break;
            case 3:
            
            #line 367 "..\..\..\SelectWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.ExtendComboBox_MouseEnter);
            
            #line default
            #line hidden
            
            #line 367 "..\..\..\SelectWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.ExtendComboBox_MouseLeave);
            
            #line default
            #line hidden
            break;
            case 4:
            
            #line 369 "..\..\..\SelectWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CopyRoomInfoBtn_Click);
            
            #line default
            #line hidden
            break;
            case 5:
            
            #line 372 "..\..\..\SelectWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UpdateClassInfoBtn_Click);
            
            #line default
            #line hidden
            break;
            case 6:
            
            #line 396 "..\..\..\SelectWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.StartClassBtn_Click);
            
            #line default
            #line hidden
            break;
            case 7:
            
            #line 397 "..\..\..\SelectWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.ExtendComboBox_MouseEnter);
            
            #line default
            #line hidden
            
            #line 397 "..\..\..\SelectWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.ExtendComboBox_MouseLeave);
            
            #line default
            #line hidden
            break;
            case 8:
            
            #line 399 "..\..\..\SelectWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CopyRoomInfoBtn_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

