﻿#pragma checksum "..\..\..\User Control\SignupPatient.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B72864ABB9EFAFDF5FF7706A06FE047D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
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


namespace BismillahTA.User_Control {
    
    
    /// <summary>
    /// SignupPatient
    /// </summary>
    public partial class SignupPatient : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\User Control\SignupPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox patientName;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\User Control\SignupPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox age;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\User Control\SignupPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton male;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\User Control\SignupPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton female;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\User Control\SignupPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox username;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\User Control\SignupPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox password;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\User Control\SignupPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image loginButton;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\User Control\SignupPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image backButton;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\User Control\SignupPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label;
        
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
            System.Uri resourceLocater = new System.Uri("/BismillahTA;component/user%20control/signuppatient.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\User Control\SignupPatient.xaml"
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
            this.patientName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.age = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.male = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 4:
            this.female = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 5:
            this.username = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.password = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.loginButton = ((System.Windows.Controls.Image)(target));
            
            #line 16 "..\..\..\User Control\SignupPatient.xaml"
            this.loginButton.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.loginButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 8:
            this.backButton = ((System.Windows.Controls.Image)(target));
            
            #line 17 "..\..\..\User Control\SignupPatient.xaml"
            this.backButton.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.backButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 9:
            this.label = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

