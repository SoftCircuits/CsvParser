// Copyright (c) 2019-2020 Jonathan Wood (www.softcircuits.com)
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
        /// <summary>
        /// Size of test data array in DataConvertersTestClass.
        /// </summary>
        private const int DataConvertersTestClassArraySize = 5;

        // Replacement tags
        private const string DataConvertersTag = "{@DataConvertersData}";
        private const string DataConverterMembersTag = "{@DataConverterMembers}";
        private const string DataConverterInitializersTag = "{@DataConverterInitializers}";

        // Target paths
        private const string TemplatePath = @"D:\Users\jwood\source\repos\CsvParser\BuildConverters\Templates";
        private const string OutputPath = @"D:\Users\jwood\source\repos\CsvParser\CsvParser\Converters";
        private const string TestPath = @"D:\Users\jwood\source\repos\CsvParser\TestCsvParser";

        static readonly TypeInfo[] TypeData = new TypeInfo[]
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

        static StringBuilder DataConvertersData;
        static StringBuilder TestMembers;
        static StringBuilder TestInitializers;

        static void Main(string[] args)
        {
            DataConvertersData = new StringBuilder();
            TestMembers = new StringBuilder();
            TestInitializers = new StringBuilder();

            // Load templates
            string dataConvertersTemplate = File.ReadAllText(Path.Combine(TemplatePath, "DataConverters.template"));
            string testDataTemplate = File.ReadAllText(Path.Combine(TemplatePath, "DataConvertersTestClass.template"));

            // Load converter code template
            CodeTemplate codeTemplate = new CodeTemplate();
            codeTemplate.LoadTemplate(Path.Combine(TemplatePath, "Converter.template"));

            // Get all type variations
            IEnumerable<CompleteType> completeTypes = BuildCompleteTypes();

            // Build converters and some additional declarations
            foreach (CompleteType type in completeTypes)
            {
                // Write convert class
                string path = Path.Combine(OutputPath, $"{type.ClassName}.cs");
                File.WriteAllText(path, codeTemplate.BuildTemplate(type));
                // Write DataConvert row
                DataConvertersData.AppendLine($"            [typeof({type.FullTypeCName})] = () => new {type.ClassName}(),");
                // Write test data member
                TestMembers.AppendLine($"        public {type.FullTypeCName} {type.TypeName}Member {{ get; set; }}");
            }

            // Create DataConverters.cs
            File.WriteAllText(Path.Combine(OutputPath, $"DataConverters.cs"),
                dataConvertersTemplate.Replace(DataConvertersTag, DataConvertersData.ToString()));

            // Create DataConvertersTestClass.cs
            for (int i = 0; i < DataConvertersTestClassArraySize; i++)
            {
                TestInitializers.AppendLine("            new DataConvertersTestClass {");
                foreach (CompleteType type in completeTypes)
                    TestInitializers.AppendLine($"                {type.TypeName}Member = {type.SampleData},");
                TestInitializers.AppendLine("            },");
            }
            string content = testDataTemplate.Replace(DataConverterMembersTag, TestMembers.ToString());
            content = content.Replace(DataConverterInitializersTag, TestInitializers.ToString());
            File.WriteAllText(Path.Combine(TestPath, $"DataConvertersTestClass.cs"), content);
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
