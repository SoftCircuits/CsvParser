﻿// Copyright (c) 2019-2024 Jonathan Wood (www.softcircuits.com)
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
        private const int DataConvertersTestClassArraySize = 10;

        // Replacement tags
        private const string DataConvertersTag = "{@DataConvertersData}";
        private const string DataConverterMembersTag = "{@DataConverterMembers}";
        private const string DataConverterInitializersTag = "{@DataConverterInitializers}";

        // Target paths
        private const string TemplatePath = @"C:\Users\jwood\source\repos\SoftCircuits\CsvParser\BuildConverters\Templates";
        private const string OutputPath = @"C:\Users\jwood\source\repos\SoftCircuits\CsvParser\CsvParser\Converters";
        private const string TestPath = @"C:\Users\jwood\source\repos\SoftCircuits\CsvParser\TestCsvParser";

        static readonly TypeInfo[] TypeData =
        [
            new(typeof(string)),
            new(typeof(char)),
            new(typeof(bool)),
            new(typeof(byte)),
            new(typeof(sbyte)),
            new(typeof(short)),
            new(typeof(ushort)),
            new(typeof(int)),
            new(typeof(uint)),
            new(typeof(long)),
            new(typeof(ulong)),
            new(typeof(float)),
            new(typeof(double)),
            new(typeof(decimal)),
            new(typeof(Guid)),
            new(typeof(DateTime)),
        ];

        static StringBuilder DataConvertersData;
        static StringBuilder TestMembers;
        static StringBuilder TestInitializers;

        static void Main(string[] _)
        {
            DataConvertersData = new StringBuilder();
            TestMembers = new StringBuilder();
            TestInitializers = new StringBuilder();

            // Load templates
            string dataConvertersTemplate = File.ReadAllText(Path.Combine(TemplatePath, "DataConverters.template"));
            string testDataTemplate = File.ReadAllText(Path.Combine(TemplatePath, "DataConvertersTestClass.template"));

            // Load converter code template
            CodeTemplate codeTemplate = new();
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
                TestMembers.AppendLine($"        public {type.FullTypeCName}{(type.IsNullable ? "?" : string.Empty)} {type.TypeName}Member {{ get; set; }}");
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
