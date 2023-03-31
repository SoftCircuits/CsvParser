using RandomDataTests;

const int Passes = 2;

List<TestConfiguration> Configurations = new()
{
    new(Passes, 1_000, 30),
    new(Passes, 500_000, 30),
    new(Passes, 1_000_000, 30),
    new(Passes, 1_000, 100),
    new(Passes, 1_000, 30, true),
    new(Passes, 500_000, 30, true),
    new(Passes, 1_000_000, 30, true),
    new(Passes, 1_000, 100, true),

    //new(Passes, 100_000, 100),

};

int Errors = 0;
CsvTester Tester = new();
Tester.StatusUpdate += Tester_StatusUpdate;
await Tester.RunAsync(Configurations);

if (Errors > 0)
    Write($"{Errors} ERROR(S) DETECTED!", ConsoleColor.Red);
else
    Write("NO ERRORS DETECTED!", ConsoleColor.Green);

void Tester_StatusUpdate(object? sender, TestStatusEventArgs e)
{
    if (e.IsError)
        Errors++;
    Write(e.Status, e.Color);
}

void Write(string s, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.WriteLine(s);
}
