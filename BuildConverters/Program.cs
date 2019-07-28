// Copyright (c) 2019 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
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

        const string DispatcherPlaceholder = "{@DispatchData}";
        const string TestDataPlaceholder = "{@TestData}";
        static StringBuilder DispatchData;
        static StringBuilder TestData;

        static void Main(string[] args)
        {
            // Set up target paths
            string templatePath = @"D:\Users\Jonathan\source\repos\CsvParser\BuildConverters\Templates";
            string outputPath = @"D:\Users\Jonathan\source\repos\CsvParser\CsvParser\Converters";
            string testPath = @"D:\Users\Jonathan\source\repos\CsvParser\TestCsvParser";

            // Load templates
            string dispatcherTemplate = File.ReadAllText(Path.Combine(templatePath, "Dispatcher.template"));
            string testDataTemplate = File.ReadAllText(Path.Combine(templatePath, "TestData.template"));
            DispatchData = new StringBuilder();
            TestData = new StringBuilder();

            // Load converter templates
            CodeTemplate template = new CodeTemplate();
            template.LoadTemplate(Path.Combine(templatePath, "Converter.template"));

            foreach (TypeInfo type in TypeData)
            {
                if (type.Type == typeof(string))
                {
                    // Standard
                    WriteType(type, template, TemplateMode.Standard, outputPath);
                    // Array
                    WriteType(type, template, TemplateMode.Array, outputPath);
                }
                else
                {
                    // Standard
                    WriteType(type, template, TemplateMode.Standard, outputPath);

                    if (type.IsValueType)
                    {
                        // Nullable
                        WriteType(type, template, TemplateMode.Nullable, outputPath);
                    }

                    // Array
                    WriteType(type, template, TemplateMode.Array, outputPath);

                    if (type.IsValueType)
                    {
                        // Nullable array
                        WriteType(type, template, TemplateMode.NullableArray, outputPath);
                    }
                }
            }

            // Dispatcher.cs
            File.WriteAllText(Path.Combine(outputPath, $"Dispatcher.cs"),
                dispatcherTemplate.Replace(DispatcherPlaceholder, DispatchData.ToString()));
            // TypeConverterData.cs
            File.WriteAllText(Path.Combine(testPath, $"TypeConvertersData.cs"),
                testDataTemplate.Replace(TestDataPlaceholder, TestData.ToString()));
        }

        private static void WriteType(TypeInfo type, CodeTemplate template, TemplateMode mode, string outputPath)
        {
            string className = CodeTemplate.GetClassName(type, mode);
            string path = Path.Combine(outputPath, $"{className}.cs");
            File.WriteAllText(path, template.BuildTemplate(type, mode));
            DispatchData.AppendLine($"            [typeof({CodeTemplate.GetCTypeName(type, mode)})] = () => new {className}(),");
            TestData.AppendLine($"            {type.ToTestData(mode)}");
        }
    }
}
