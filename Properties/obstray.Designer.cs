﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OBStray.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    public sealed partial class obstray : global::System.Configuration.ApplicationSettingsBase {
        
        private static obstray defaultInstance = ((obstray)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new obstray())));
        
        public static obstray Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\\\Users\\\\mateus\\\\Documents\\\\Projetos\\\\OBS\\\\rundir\\\\OBS.exe")]
        public string obsdir {
            get {
                return ((string)(this["obsdir"]));
            }
            set {
                this["obsdir"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\\\Users\\\\mateus\\\\AppData\\\\Roaming\\\\OBS\\\\")]
        public string obsconfigdir {
            get {
                return ((string)(this["obsconfigdir"]));
            }
            set {
                this["obsconfigdir"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\\\Users\\\\mateus\\\\AppData\\\\Roaming\\\\OBS\\\\profiles")]
        public string obsprofiledir {
            get {
                return ((string)(this["obsprofiledir"]));
            }
            set {
                this["obsprofiledir"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1000")]
        public string maxbitrate {
            get {
                return ((string)(this["maxbitrate"]));
            }
            set {
                this["maxbitrate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2,0")]
        public string downscale {
            get {
                return ((string)(this["downscale"]));
            }
            set {
                this["downscale"] = value;
            }
        }
    }
}