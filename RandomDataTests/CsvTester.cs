using SoftCircuits.CsvParser;
using System.Diagnostics;

namespace RandomDataTests
{
    internal class CsvTester
    {
        private readonly RandomGeneratorCollection RandomGenerators;

        public event EventHandler<TestStatusEventArgs>? StatusUpdate;

        public CsvTester()
        {
            RandomGenerators = new();
        }

        public async Task RunAsync(IEnumerable<TestConfiguration> configs)
        {
            // Get working file name
            string path = Path.GetTempFileName();

            int configNumber = 0;
            int configCount = configs.Count();
            foreach (TestConfiguration config in configs)
            {
                OnReportStatus($"Configuration {++configNumber}/{configCount} (Passes={config.Passes:#,0},Records={config.RecordsPerPass:#,0},MaxFields={config.MaxFields},Async={config.RunAsync})", ConsoleColor.Magenta);

                for (int pass = 0; pass < config.Passes; pass++)
                {
                    try
                    {
                        RandomGenerators.ResetFields(config.MaxFields);
                        OnReportStatus($"Pass {pass + 1}/{config.Passes} (Fields={RandomGenerators.Count})", ConsoleColor.Cyan);
                        OnReportStatus($"[{RandomGenerators}]", ConsoleColor.Cyan);

                        //
                        OnReportStatus("Generating Test Data...");
                        List<List<string>> dataRows = RandomGenerators.GetAllRandomValues(config.RecordsPerPass);

                        // Write data
                        OnReportStatus("Writing...");
                        if (config.RunAsync)
                        {
                            using CsvWriter writer = new(path);
                            foreach (List<string> row in dataRows)
                                await writer.WriteAsync(row);
                        }
                        else
                        {
                            using CsvWriter writer = new(path);
                            foreach (List<string> row in dataRows)
                                writer.Write(row);
                        }

                        //File.Copy(path, @$"C:\Users\Jonathan\OneDrive\Desktop\CSV Files\Testing\Pass_{pass + 1}.csv", true);

                        // Read and validate data
                        OnReportStatus("Reading...");
                        CsvSettings settings = new() { EmptyLineBehavior = EmptyLineBehavior.EmptyColumn };
                        if (config.RunAsync)
                        {
                            using CsvReader reader = new(path, settings);

                            int i = 0;
                            while (await reader.ReadAsync())
                            {
                                Debug.Assert(reader.Columns != null);

                                if (i >= dataRows.Count)
                                    throw new Exception($"{dataRows.Count} row(s) written but more rows read.");

                                CompareArrays(dataRows[i], reader.Columns);
                                i++;
                            }

                            if (i < dataRows.Count)
                                throw new Exception($"{dataRows} row(s) written but only {i} row(s) read.");
                        }
                        else
                        {
                            using CsvReader reader = new(path, settings);

                            int i = 0;
                            while (reader.Read())
                            {
                                Debug.Assert(reader.Columns != null);

                                if (i >= dataRows.Count)
                                    throw new Exception($"{dataRows.Count} row(s) written but more rows read.");

                                CompareArrays(dataRows[i], reader.Columns);
                                i++;
                            }

                            if (i < dataRows.Count)
                                throw new Exception($"{dataRows} row(s) written but only {i} row(s) read.");
                        }

                        OnReportStatus("Results match!", ConsoleColor.Green);
                        OnReportStatus();
                    }
                    catch (Exception ex)
                    {
                        OnReportStatus($"ERROR : {ex.Message}", ConsoleColor.Red);
                        OnReportStatus();
                    }
                }
            }

            File.Delete(path);
        }

        private static void CompareArrays(List<string> expected, string[] actual)
        {
            if (expected.Count != actual.Length)
                throw new Exception("Number of columns don't match.");

            for (int i = 0; i < expected.Count; i++)
                if (expected[i] != actual[i])
                    throw new Exception($"Field values don't match : \"{expected[i]}\" vs \"{actual[i]}\"");
        }

        protected void OnReportStatus(string status = "", ConsoleColor color = ConsoleColor.White, bool isError = false)
        {
            StatusUpdate?.Invoke(this, new(status, color, isError));
        }

        public static void CreateRandomFile(string path, int records, int maxFields)
        {
            RandomGeneratorCollection randomGenerators = new();
            randomGenerators.ResetFields(maxFields);

            using CsvWriter writer = new(path);
            for (int i = 0; i < records; i++)
            {
                writer.Write(randomGenerators.GetRandomValues());
            }
        }
    }
}
