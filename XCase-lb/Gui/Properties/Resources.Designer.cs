﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace XCase.Gui.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("XCase.Gui.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;DockingManager&gt;
        ///    &lt;ResizingPanel ResizeWidth=&quot;*&quot; ResizeHeight=&quot;*&quot; EffectiveSize=&quot;0,0&quot; Orientation=&quot;Horizontal&quot;&gt;
        ///        &lt;DockablePane ResizeWidth=&quot;200&quot; ResizeHeight=&quot;*&quot; EffectiveSize=&quot;200,842.08&quot; Anchor=&quot;Left&quot;&gt;
        ///            &lt;DockableContent Name=&quot;navigatorWindow&quot; AutoHide=&quot;false&quot; /&gt;
        ///        &lt;/DockablePane&gt;
        ///        &lt;ResizingPanel ResizeWidth=&quot;*&quot; ResizeHeight=&quot;*&quot; EffectiveSize=&quot;1272,842.08&quot; Orientation=&quot;Vertical&quot;&gt;
        ///            &lt;DocumentPanePlaceHolder /&gt;
        ///            &lt;DockablePane ResizeWidth=&quot;*&quot; Resiz [rest of string was truncated]&quot;;.
        /// </summary>
        public static string DefaultLayout {
            get {
                return ResourceManager.GetString("DefaultLayout", resourceCulture);
            }
        }
    }
}
