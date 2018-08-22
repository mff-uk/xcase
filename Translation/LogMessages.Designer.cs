﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace XCase.Translation {
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
    internal class LogMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal LogMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("XCase.Translation.LogMessages", typeof(LogMessages).Assembly);
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
        ///   Looks up a localized string similar to No namespace prefix defined for {0}..
        /// </summary>
        internal static string NO_NAMESPACE_PREFIX {
            get {
                return ResourceManager.GetString("NO_NAMESPACE_PREFIX", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Class {0} is marked abstract but there are no specializations of the class in the diagram..
        /// </summary>
        internal static string XS_ABSTRACT_NOT_SPECIALIZED {
            get {
                return ResourceManager.GetString("XS_ABSTRACT_NOT_SPECIALIZED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} has multiplicity {1} which cannot be expressed in the schema because it is not translated into element declaration but into groups references and attribute group can be referenced only once. Consider assigning an element label to {2} or moving its attributes into attribute container..
        /// </summary>
        internal static string XS_ASSOCIATION_MULTIPLICITY_LOST {
            get {
                return ResourceManager.GetString("XS_ASSOCIATION_MULTIPLICITY_LOST", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Multiplicity {0} of attribute {1}.{2} can not be contained in the schema because it is  translated as a xml schema attribute. Consider moving the attribute to an attribute container. .
        /// </summary>
        internal static string XS_ATTRIBUTE_MULTIPLICITY_LOST {
            get {
                return ResourceManager.GetString("XS_ATTRIBUTE_MULTIPLICITY_LOST", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Attributes under choice can not be translated in &quot;exclusive or&quot; semantics, the had to be translated as optional.
        /// </summary>
        internal static string XS_ATTRIBUTES_IN_CHOICE {
            get {
                return ResourceManager.GetString("XS_ATTRIBUTES_IN_CHOICE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Some attributes under {0} were propagated upwards. Consider giving an element label to all classes of the class union. .
        /// </summary>
        internal static string XS_ATTRIBUTES_IN_CLASS_UNION {
            get {
                return ResourceManager.GetString("XS_ATTRIBUTES_IN_CLASS_UNION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name of a class is empty..
        /// </summary>
        internal static string XS_CLASS_NAME_EMPTY {
            get {
                return ResourceManager.GetString("XS_CLASS_NAME_EMPTY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Root element name {0} changed to {1} to avoid duplicate names. .
        /// </summary>
        internal static string XS_DUPLICATE_ROOT_ELEMENTS {
            get {
                return ResourceManager.GetString("XS_DUPLICATE_ROOT_ELEMENTS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} must be named before translation..
        /// </summary>
        internal static string XS_ELEMENT_NAME_MISSING {
            get {
                return ResourceManager.GetString("XS_ELEMENT_NAME_MISSING", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Xml schema implementation of type {0} is incorrect and cannot be used (all xml schema elements in should be used without namespace prefix) . .
        /// </summary>
        internal static string XS_INCORRECT_XSD {
            get {
                return ResourceManager.GetString("XS_INCORRECT_XSD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type {0} does not have XSD implementation specified..
        /// </summary>
        internal static string XS_MISSING_TYPE_XSD {
            get {
                return ResourceManager.GetString("XS_MISSING_TYPE_XSD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Project has no namespace. .
        /// </summary>
        internal static string XS_NO_DEFAULT_NAMESPACE {
            get {
                return ResourceManager.GetString("XS_NO_DEFAULT_NAMESPACE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Schema has no global element. Consider giving an element label to some of the root classes..
        /// </summary>
        internal static string XS_NO_ROOT {
            get {
                return ResourceManager.GetString("XS_NO_ROOT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Class {0} does not have an element label and is specialized. Consider making it abstract..
        /// </summary>
        internal static string XS_NON_ABSTRACT_CLASS {
            get {
                return ResourceManager.GetString("XS_NON_ABSTRACT_CLASS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specialized class {0} does not have an element label. Its attributes were translated to an attribute group {1} but the translation does not reference it. Consider giving an element label to all specializations..
        /// </summary>
        internal static string XS_SPECIALIZED_ATTRIBUTE_GROUP {
            get {
                return ResourceManager.GetString("XS_SPECIALIZED_ATTRIBUTE_GROUP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Alias of the attribute {0} must have been changed to {1} to be a valid name for an attribute..
        /// </summary>
        internal static string XS_TRANSLATED_ATTRIBUTE_ALIAS {
            get {
                return ResourceManager.GetString("XS_TRANSLATED_ATTRIBUTE_ALIAS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name of the class {0} must have been changed to {1} to be a valid name for a complex type..
        /// </summary>
        internal static string XS_TRANSLATED_CLASS_NAME {
            get {
                return ResourceManager.GetString("XS_TRANSLATED_CLASS_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type {0} of attribute {1}.{2} is not specified, using xs:string as default..
        /// </summary>
        internal static string XS_TYPE_TRANSLATED_AS_STRING {
            get {
                return ResourceManager.GetString("XS_TYPE_TRANSLATED_AS_STRING", resourceCulture);
            }
        }
    }
}