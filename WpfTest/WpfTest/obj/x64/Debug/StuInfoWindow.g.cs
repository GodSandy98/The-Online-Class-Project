#pragma checksum "..\..\..\StuInfoWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E4299D2B44A939FB92BAA6D18FDBF2646F315222E508EB00DC7981BF2425127A"
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
    /// StuInfoWindow
    /// </summary>
    public partial class StuInfoWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\StuInfoWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MinBtn;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\StuInfoWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExitBtn;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\StuInfoWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label studentLabel;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\StuInfoWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label entryTimeLabel;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\StuInfoWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label accuracyLabel;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\StuInfoWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label classIDLabel;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\StuInfoWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label leaveTimeLabel;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\StuInfoWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label statementNumLabel;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\StuInfoWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label genderLabel;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\StuInfoWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label totalTimeLabel;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\StuInfoWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label focusTimeLabel;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\StuInfoWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox statementPointCheckBox;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfTest;component/stuinfowindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\StuInfoWindow.xaml"
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
            this.MinBtn = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\StuInfoWindow.xaml"
            this.MinBtn.Click += new System.Windows.RoutedEventHandler(this.MinBtn_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ExitBtn = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\StuInfoWindow.xaml"
            this.ExitBtn.Click += new System.Windows.RoutedEventHandler(this.ExitBtn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.studentLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.entryTimeLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.accuracyLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.classIDLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.leaveTimeLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.statementNumLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.genderLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.totalTimeLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.focusTimeLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 12:
            this.statementPointCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

