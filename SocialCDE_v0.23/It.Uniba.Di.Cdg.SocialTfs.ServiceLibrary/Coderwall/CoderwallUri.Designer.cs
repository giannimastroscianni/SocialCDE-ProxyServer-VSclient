﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.18449
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

namespace It.Uniba.Di.Cdg.SocialTfs.ServiceLibrary.Coderwall {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class CoderwallUri : global::System.Configuration.ApplicationSettingsBase {
        
        private static CoderwallUri defaultInstance = ((CoderwallUri)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new CoderwallUri())));
        
        public static CoderwallUri Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("/username.json")]
        public string REPUTATION {
            get {
                return ((string)(this["REPUTATION"]));
            }
        }
    }
}