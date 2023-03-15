using SoftCircuits.CsvParser;
using System.Data;

namespace RandomDataTests
{
    internal class RandomGeneratorCollection
    {
        private class FieldType
        {
            public string Description { get; private set; }
            public Func<RandomGenerator> GetGenerator { get; private set; }
            public FieldType(string description, Func<RandomGenerator> getGenerator)
            {
                Description = description;
                GetGenerator = getGenerator;
            }
        }

        private class Field
        {
            public string Description { get; set; }
            public RandomGenerator Generator { get; set; }

            public Field(string description, RandomGenerator generator)
            {
                Description = description;
                Generator = generator;
            }
        }

        private readonly FieldType[] FieldTypes;
        private readonly List<Field> Fields;

        public RandomGeneratorCollection()
        {
            // Define available field types
            FieldTypes = new FieldType[]
            {
                new("Empty", () => new RandomEmptyGenerator()),
                new("SmallInt", () => new RandomIntegerGenerator(0, 9999)),
                new("LargeInt", () => new RandomIntegerGenerator()),
                new("SmallFloat", () => new RandomFloatGenerator(9999)),
                new("LargeFloat", () => new RandomFloatGenerator(float.MaxValue)),
                new("SmallString", () => new RandomStringGenerator(maxLength: 10)),
                new("MediumString", () => new RandomStringGenerator(CharType.Alphanumeric | CharType.Punctuation, 120, true)),
                new("LargeString", () => new RandomStringGenerator(CharType.Alphanumeric | CharType.Punctuation, 999, true, true)),
                new("Date", () => new RandomDateGenerator()),
                new("Guid", () => new RandomGuidGenerator()),
            };
            Fields = new();
        }

        public void ResetFields(int maxFields)
        {
            int fields = Random.Shared.Next(1, maxFields);
            Fields.Clear();
            for (int i = 0; i < fields; i++)
            {
                int index = Random.Shared.Next(FieldTypes.Length);
                FieldType fieldType = FieldTypes[index];
                Fields.Add(new(fieldType.Description, fieldType.GetGenerator()));
            }
        }

        /// <summary>
        /// Returns random values for one record.
        /// </summary>
        public List<string> GetRandomValues()
        {
            return Fields
                .Select(f => f.Generator.GetValue())
                .ToList();
        }

        /// <summary>
        /// Returns random values for the specified number of records.
        /// </summary>
        /// <param name="records">Number of records.</param>
        public List<List<string>> GetAllRandomValues(int records)
        {
            List<List<string>> dataRows = new();

            for (int i = 0; i < records; i++)
                dataRows.Add(GetRandomValues());

            return dataRows;
        }

        /// <summary>
        /// Generates CSV files with random data using the current settings.
        /// </summary>
        /// <param name="path">The directory where the files should be placed.</param>
        /// <param name="files">The number of files to create.</param>
        /// <param name="recordsPerFile">The number of records written to each file.</param>
        /// <param name="nameTemplate">A template for forming the file names.</param>
        public void CreateGenerateRandomFiles(string path, int files, int recordsPerFile, string nameTemplate = "DataFile_{0}.csv")
        {
            for (int i = 0; i < files; i++)
            {
                string file = Path.Combine(path, string.Format(nameTemplate, i + 1));
                using CsvWriter writer = new(file);
                for (int j = 0; j < recordsPerFile; j++)
                    writer.Write(GetRandomValues());
            }
        }

        public int Count => Fields.Count;

        public override string ToString() => string.Join(",", Fields.Select(f => f.Description));
    }
}
