// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BuildConverters
{
    /// <summary>
    /// Code used to generate type converters.
    /// </summary>
    class Program
    {
        static TypeInfo[] TypeData = new TypeInfo[]
        {
            new TypeInfo(typeof(string)),
            new TypeInfo(typeof(char)),
            new TypeInfo(typeof(bool)),
            new TypeInfo(typeof(byte)),
            new TypeInfo(typeof(sbyte)),
            new TypeInfo(typeof(short)),
            new TypeInfo(typeof(ushort)),
            new TypeInfo(typeof(int)),
            new TypeInfo(typeof(uint)),
            new TypeInfo(typeof(long)),
            new TypeInfo(typeof(ulong)),
            new TypeInfo(typeof(float)),
            new TypeInfo(typeof(double)),
            new TypeInfo(typeof(decimal)),
            new TypeInfo(typeof(Guid)),
            new TypeInfo(typeof(DateTime)),
        };

        const string DataConvertersPlaceholder = "{@DataConvertersData}";
        const string DataConverterMembersPlaceholder = "{@DataConverterMembers}";
        const string DataConverterInitializersPlaceholder = "{@DataConverterInitializers}";
        static StringBuilder DataConvertersData;

        static StringBuilder TestMembers;
        static StringBuilder TestInitializers;

        static void Main(string[] args)
        {
            // Set up target paths
            string templatePath = @"D:\Users\Jonathan\source\repos\CsvParser\BuildConverters\Templates";
            string outputPath = @"D:\Users\Jonathan\source\repos\CsvParser\CsvParser\Converters";
            string testPath = @"D:\Users\Jonathan\source\repos\CsvParser\TestCsvParser";

            // Load templates
            string dataConvertersTemplate = File.ReadAllText(Path.Combine(templatePath, "DataConverters.template"));
            string testDataTemplate = File.ReadAllText(Path.Combine(templatePath, "DataConvertersTestClass.template"));
            DataConvertersData = new StringBuilder();
            TestMembers = new StringBuilder();
            TestInitializers = new StringBuilder();

            // Load converter code template
            CodeTemplate template = new CodeTemplate();
            template.LoadTemplate(Path.Combine(templatePath, "Converter.template"));

            IEnumerable<CompleteType> completeTypes = BuildCompleteTypes();
            foreach (CompleteType type in completeTypes)
            {
                // Write convert class
                string path = Path.Combine(outputPath, $"{type.ClassName}.cs");
                File.WriteAllText(path, template.BuildTemplate(type));
                // Write DataConvert row
                DataConvertersData.AppendLine($"            [typeof({type.FullTypeCName})] = () => new {type.ClassName}(),");
                // Write test data member
                TestMembers.AppendLine($"        public {type.FullTypeCName} {type.TypeName}Member {{ get; set; }}");
            }

            // Write DataConverters.cs
            File.WriteAllText(Path.Combine(outputPath, $"DataConverters.cs"),
                dataConvertersTemplate.Replace(DataConvertersPlaceholder, DataConvertersData.ToString()));

            // DataConverterTestType.cs
            for (int i = 0; i < 5; i++)
            {
                TestInitializers.AppendLine("            new DataConvertersTestClass {");
                foreach (CompleteType type in completeTypes)
                    TestInitializers.AppendLine($"                {type.TypeName}Member = {type.SampleData},");
                TestInitializers.AppendLine("            },");
            }
            string content = testDataTemplate.Replace(DataConverterMembersPlaceholder, TestMembers.ToString());
            content = content.Replace(DataConverterInitializersPlaceholder, TestInitializers.ToString());
            File.WriteAllText(Path.Combine(testPath, $"DataConvertersTestClass.cs"), content);
        }

        // Build a list of all type variations
        private static IEnumerable<CompleteType> BuildCompleteTypes()
        {
            foreach (TypeInfo type in TypeData)
            {
                yield return new CompleteType(type, TypeVariation.Standard);
                yield return new CompleteType(type, TypeVariation.Array);
                if (type.IsValueType)
                {
                    yield return new CompleteType(type, TypeVariation.Nullable);
                    yield return new CompleteType(type, TypeVariation.NullableArray);
                }
            }
        }
    }
}
