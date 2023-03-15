namespace RandomDataTests
{
    internal class TestStatusEventArgs : EventArgs
    {
        public string Status { get; set; }
        public ConsoleColor Color { get; set; }
        public bool IsError { get; set; }

        public TestStatusEventArgs(string status, ConsoleColor color, bool isError)
        {
            Status = status;
            Color = color;
            IsError = isError;
        }
    }
}
