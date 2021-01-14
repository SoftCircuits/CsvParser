// Copyright (c) 2019-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BuildConverters
{
    public class CodeTemplate
    {
        // Section placeholder tags
        private static readonly string StartTemplateTag = "{@Template}";
        private static readonly string EndTemplateTag = "{@EndTemplate}";
        private static readonly string StartSectionTag = "{@Section";
        private static readonly string EndSectionTag = "{@EndSection}";

        // Tag Meanings:
        // {@ClassName} = "NullableBooleanArrayConverter"
        // {@BaseTypeName} = "Boolean"
        // {@BaseTypeCName} = "bool"
        // {@FullTypeName} = "Boolean?[]"
        // {@FullTypeCName} = "bool?[]"

        private static readonly string ClassNameTag = "{@ClassName}";
        private static readonly string BaseTypeNameTag = "{@BaseTypeName}";
        private static readonly string BaseTypeCNameTag = "{@BaseTypeCName}";
        private static readonly string FullTypeNameTag = "{@FullTypeName}";
        private static readonly string FullTypeCNameTag = "{@FullTypeCName}";

        private static readonly string DefaultType = "Default";

        private string TemplateText;
        private List<TemplateSection> Sections;

        public CodeTemplate()
        {
            TemplateText = string.Empty;
            Sections = new List<TemplateSection>();
        }

        public string BuildTemplate(CompleteType type)
        {
            string text = TemplateText;

            // Get placholder groups for the current mode
            foreach (var group in Sections.Where(s => s.Variation == type.Variation).GroupBy(s => s.Placeholder))
            {
                // Look for type-specific section
                TemplateSection section = group.FirstOrDefault(s => s.Type == type.BaseTypeName);
                // If not found, look for default type
                if (section == null)
                {
                    section = group.FirstOrDefault(s => s.Type == DefaultType);
                    if (section == null)
                        throw new Exception($"Template file missing '{StartSectionTag}({group.Key},{type.BaseTypeName},{type.Variation})}}, and no suitable default found.");
                }
                // Replace this placeholder
                text = text.Replace($"{{@{group.Key}}}", section.Text);
            }
            // Final tag replacements
            text = text.Replace(ClassNameTag, type.ClassName);
            text = text.Replace(BaseTypeNameTag, type.BaseTypeName);
            text = text.Replace(BaseTypeCNameTag, type.BaseTypeCName);
            text = text.Replace(FullTypeNameTag, type.FullTypeName);
            text = text.Replace(FullTypeCNameTag, type.FullTypeCName);
            return text;
        }

        #region Loading and parsing template

        /// <summary>
        /// Loads the specified template file.
        /// </summary>
        /// <param name="path">Path to template file.</param>
        public void LoadTemplate(string path)
        {
            // Load text from template file
            string text = File.ReadAllText(path);

            // Parse main template body
            int start = text.IndexOf(StartTemplateTag);
            if (start < 0)
                throw new Exception($"Start tag '{StartTemplateTag}' not found.");
            start += StartTemplateTag.Length;

            int end = text.IndexOf(EndTemplateTag, start);
            if (end < 0)
                throw new Exception($"End tag '{EndTemplateTag}' not found.");

            TemplateText = text.Substring(start, end - start);

            // Parse sections
            ParseSections(text);
        }

        /// <summary>
        /// Parse all sections in the template file.
        /// </summary>
        /// <param name="text">Full text of template file.</param>
        private void ParseSections(string text)
        {
            int pos = 0;

            Sections.Clear();

            pos = text.IndexOf(StartSectionTag, pos);
            while (pos >= 0)
            {
                pos += StartSectionTag.Length;
                int pos2 = text.IndexOf('}', pos);

                string arguments = text.Substring(pos, pos2 - pos);
                arguments = arguments.Trim(' ', '\t', '(', ')');
                TemplateSection section = new TemplateSection(arguments.Split(new[] { ',' }));
                Sections.Add(section);

                pos = pos2 + 1;
                pos2 = text.IndexOf(EndSectionTag, pos);
                if (pos2 < 0)
                    throw new Exception($"'{StartSectionTag}' without matching '{EndSectionTag}'.");
                section.Text = text.Substring(pos, pos2 - pos);

                pos = pos2 + EndSectionTag.Length;
                pos = text.IndexOf(StartSectionTag, pos);
            }
        }

        #endregion

    }
}
