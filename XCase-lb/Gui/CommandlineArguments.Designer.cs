﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace XCase.Gui {
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
    internal class CommandlineArguments {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CommandlineArguments() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("XCase.Gui.CommandlineArguments", typeof(CommandlineArguments).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Use this argument to explicitly select the diagram(s) for export. Several possible usages are allowed: 
        ///
        ///		--diagram 1;2 
        ///		  export diagrams no. 1 and 2 from the project 
        ///		  (diagram numbers start from 0)
        ///
        ///		--diagram diag1 
        ///		  export diagram diag1 (it must exist in the project)
        ///
        ///		--diagram 1(filename)
        ///		  export diagram no. 1 and save it as filename.xsd/filename.png
        ///
        ///		--diagram diag1(filename)[S]
        ///		  export diagram diag1 as XML schema 
        ///		  (this overrides --noSchemas flag)
        ///
        ///		--diagram [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ARG_DIAGRAM {
            get {
                return ResourceManager.GetString("ARG_DIAGRAM", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Prints this help..
        /// </summary>
        internal static string ARG_HELP {
            get {
                return ResourceManager.GetString("ARG_HELP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Input XCase project from which images and schemas are exported..
        /// </summary>
        internal static string ARG_INPUT {
            get {
                return ResourceManager.GetString("ARG_INPUT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Prints list of diagrams in the input project..
        /// </summary>
        internal static string ARG_LIST {
            get {
                return ResourceManager.GetString("ARG_LIST", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to When used, images are not exported. This setting holds for all diagrams but can be overriden in each diagram when --diagram is used..
        /// </summary>
        internal static string ARG_NOIMAGES {
            get {
                return ResourceManager.GetString("ARG_NOIMAGES", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to When used, XML schemas are not exported. This setting holds for all diagrams but can be overriden in each diagram when --diagram is used..
        /// </summary>
        internal static string ARG_NOSCHEMAS {
            get {
                return ResourceManager.GetString("ARG_NOSCHEMAS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Directory where all exports are saved. If you wish to save images and schemas in seperate directories, use --outputDirPng and --outputDirPng settings. If omitted, files are saved in the same directory as input file..
        /// </summary>
        internal static string ARG_OUTPUTDIR {
            get {
                return ResourceManager.GetString("ARG_OUTPUTDIR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exported PNG images are stored in this directory. This overrides --outputDir directory for images..
        /// </summary>
        internal static string ARG_OUTPUTDIRPNG {
            get {
                return ResourceManager.GetString("ARG_OUTPUTDIRPNG", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exported XML schemas are stored in this directory. This overrides --outputDir directory for schemas..
        /// </summary>
        internal static string ARG_OUTPUTDIRXSD {
            get {
                return ResourceManager.GetString("ARG_OUTPUTDIRXSD", resourceCulture);
            }
        }
    }
}
