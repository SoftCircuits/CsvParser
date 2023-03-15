namespace RandomDataTests
{
    internal class TestConfiguration
    {
        public int Passes { get; set; }
        public int RecordsPerPass { get; set; }
        public int MaxFields { get; set; }
        public bool RunAsync { get; set; }

        public TestConfiguration(int passes, int recordsPerPass, int maxFields, bool runAsync = false)
        {
            Passes = passes;
            RecordsPerPass = recordsPerPass;
            MaxFields = maxFields;
            RunAsync = runAsync;
        }
    }
}
