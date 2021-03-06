using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using XCase.Model;

namespace XCase.Translation.XmlSchema
{
	/// <summary>
	/// Naming support class serves as a helper class that manages type names and
	/// ensures that type names for created complex types are distinct. It also 
	/// checks for the distinctness of the global element declarations.
	/// </summary>
	public class NamingSupport
	{
		/// <summary>
		/// Counts usages of type names 
		/// </summary>
		private static Dictionary<string, int> counters;

		/// <summary>
		/// List of names of global elements already declared
		/// </summary>
		public List<String> usedRootLabels;
        public Dictionary<string, string> namespacePrefixes;
        public int seqAttrGroupID = 0;

		/// <summary>
		/// Initializes the class, resets all lists and counters
		/// </summary>
		public void Initialize()
		{
			if (typeNameSuggestions == null)
				typeNameSuggestions = new TypeNameSuggestions();
			else
				typeNameSuggestions.Clear();
			
			if (counters == null)
				counters = new Dictionary<string, int>();
			else
			{
				counters.Clear();
			}

			if (usedRootLabels == null)
				usedRootLabels = new List<string>();
			else 
				usedRootLabels.Clear();

            if (namespacePrefixes == null)
                namespacePrefixes = new Dictionary<string, string>();
            else
                namespacePrefixes.Clear();
		}

		/// <summary>
		/// Gets a unique name for global element. Adds record to <see cref="usedRootLabels"/>.
		/// The name is based upon <paramref name="psmClass"/>' <see cref="PSMClass.ElementName"/>.
		/// </summary>
		/// <param name="psmClass">The class for which the name is returned.</param>
		/// <returns>unique global element name</returns>
		public string GetNameForGlobalElement(PSMClass psmClass)
		{
			string normalized = NormalizeTypeName(psmClass, r => r.ElementName);
			string elementName =
				NameSuggestor<string>.SuggestUniqueName(usedRootLabels, normalized, item => item, true, false);
			if (elementName != normalized)
			{
				Log.AddWarning(string.Format(LogMessages.XS_DUPLICATE_ROOT_ELEMENTS, psmClass.ElementName, elementName));
			}
			usedRootLabels.Add(elementName);
			return elementName;
		}

        public string GetUniqueIDForAttributeGroup()
        {
            seqAttrGroupID++;
            return "AttrGroup"+seqAttrGroupID.ToString();
        }

        public string GetNamespaceURIForPackage(string packageName, string namespacePrefix, string projectName)
        {
            string modifiedPackageName = packageName;
            string sep = "";
            Uri nameSpace;

            if (packageName != "default")
            {
                int ix = modifiedPackageName.IndexOf("::");
                if (ix != -1)
                    modifiedPackageName = modifiedPackageName.Substring(ix + 2);
                modifiedPackageName = modifiedPackageName.Replace(':', '/');
                if ((projectName != null) && (projectName != ""))
                    sep = "/";
                nameSpace = new Uri(namespacePrefix + projectName + sep + modifiedPackageName);
            }
            else
                nameSpace = new Uri(namespacePrefix + projectName);
            return nameSpace.AbsoluteUri;
        }

        public string GetXsdFileNameForPackage(string packageName, string projectName)
        {
            string fileName = packageName;
            string sep = "";
            if (packageName != "default")
            {
                int ix = fileName.IndexOf("::");
                if (ix != -1)
                    fileName = fileName.Substring(ix + 2);
                fileName = fileName.Replace(':', '_');
                if ((projectName != null) && (projectName != ""))
                    sep = "_";
                fileName = projectName + sep + fileName + ".xsd";
            }
            else
                fileName = projectName + ".xsd";
            return fileName;
        }

        public string GetPrefixForNamespaceURI(string namespaceURI)
        {
            if (namespaceURI.EndsWith("/"))
                namespaceURI = namespaceURI.Substring(0, namespaceURI.Length - 1);

            if (namespacePrefixes.ContainsKey(namespaceURI))
                return namespacePrefixes[namespaceURI];
            
            string prefixCandidate = namespaceURI.ToLower();
            int ix = prefixCandidate.LastIndexOf('/');
            prefixCandidate = prefixCandidate.Substring(ix + 1);

            int cntChars = 4;
            while (namespacePrefixes.ContainsValue(prefixCandidate.Substring(0, cntChars)))
            {
                cntChars++;
                if (prefixCandidate.Length < cntChars)
                    prefixCandidate.PadRight(2*prefixCandidate.Length,'a');
            }

            prefixCandidate = prefixCandidate.Substring(0, cntChars);
            namespacePrefixes[namespaceURI] = prefixCandidate;

            return namespacePrefixes[namespaceURI];
        }

		private string whitespaceReplacement = "-";

		/// <summary>
		/// String by which whitespace is replaced in element names (default is "-").
		/// </summary>
		/// <value><see cref="String"/></value>
		public string WhitespaceReplacement
		{
			get
			{
				return whitespaceReplacement;
			}
			set
			{
				if (normalizationRegex == null)
				{
					normalizationRegex = new Regex("\\w", RegexOptions.CultureInvariant);
				}
				whitespaceReplacement = value;
			}
		}

		private Regex normalizationRegex;

		/// <summary>
		/// Logs where errors and warnings are written.
		/// </summary>
		/// <value><see cref="TranslationLog"/></value>
		public TranslationLog Log { get; set; }

		/// <summary>
		/// Declaration of a handler that returns name for an element. 
		/// </summary>
		public delegate string GetItemName<Element>(Element item);


		/// <summary>
		/// Normalizes the name of the type (replaces whitespaces with <see cref="WhitespaceReplacement"/>).
		/// </summary>
		/// <typeparam name="Element">The type of the element.</typeparam>
		/// <param name="element">The element whose name should be normalized.</param>
		/// <param name="nameGetter">The function (conviniently a lambda expression) that 
		/// returns name for <paramref name="element"/>.</param>
		/// <returns></returns>
		public string NormalizeTypeName<Element>(Element element, GetItemName<Element> nameGetter)
		{
			string typeName = nameGetter(element);
			if (normalizationRegex == null)
			{
				normalizationRegex = new Regex("\\s", RegexOptions.CultureInvariant);
			}
			if (normalizationRegex.IsMatch(typeName))
			{
				string replace = normalizationRegex.Replace(typeName, WhitespaceReplacement);
				if (element is PSMAttribute)
					Log.AddWarning(string.Format(LogMessages.XS_TRANSLATED_ATTRIBUTE_ALIAS, ((PSMAttribute)element).AliasOrName, replace));
				else
					Log.AddWarning(string.Format(LogMessages.XS_TRANSLATED_CLASS_NAME, element, replace));
				return replace;
			}
			else return typeName;
		}

		/// <summary>
		/// Translates the name of the type. The returned type name is unique compared to 
		/// the results of previous calls of <see cref="TranslateTypeName"/>. 
		/// </summary>
		/// <param name="psmClass">The PSM class for which type name should be returned.</param>
		/// <returns></returns>
		public string TranslateTypeName(PSMClass psmClass)
		{
			if (string.IsNullOrEmpty(psmClass.Name))
			{
				Log.AddError(LogMessages.XS_CLASS_NAME_EMPTY);
				return "empty";
			}
			string normalizedTypeName = NormalizeTypeName(psmClass, p => p.Name);

			if (!counters.ContainsKey(normalizedTypeName) || counters[normalizedTypeName] == 0)
			{
				counters[normalizedTypeName] = 1;
				return String.Format("{0}", normalizedTypeName);
			}
			return String.Format("{0}{1}", normalizedTypeName, ++counters[normalizedTypeName]);
		}

		/// <summary>
		/// Gets name for a complex type. If there is a suggestion for the 
		/// type name in <see cref="typeNameSuggestions"/>, it returns the suggestion. Otherwise
		/// it returrns a result of <see cref="TranslateTypeName"/> call. 
		/// </summary>
		/// <param name="psmClass">The PSM class.</param>
		/// <returns>name of for a complex type</returns>
		public string GetNameForComplexType(PSMClass psmClass)
		{
			if (typeNameSuggestions.ContainsKey(psmClass) && !String.IsNullOrEmpty(typeNameSuggestions[psmClass]))
			{
				return typeNameSuggestions[psmClass];
			}
			else
				return TranslateTypeName(psmClass);
		}

		/// <summary>
		/// Contains suggestion for type names. 
		/// </summary>
		/// /// <seealso cref="GetNameForComplexType"/>
		public TypeNameSuggestions typeNameSuggestions;

		/// <summary>
		/// Maps PSMClasses to strings.
		/// </summary>
		public class TypeNameSuggestions : Dictionary<PSMClass, string>
		{
			
		}
	}
}