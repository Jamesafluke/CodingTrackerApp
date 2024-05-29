
using Spectre.Console;
using Program;

namespace CodingTracker.CodingTrackerLibrary;

public class StopwatchEntry
{
    static public TimeEntry Start()
    {
        DateTime startTime = DateTime.Now;
        AnsiConsole.MarkupInterpolated($"Time started at [bold green]{startTime}[/].");
        AnsiConsole.WriteLine("Press any key to end.");
        Console.ReadLine();

        DateTime endTime = DateTime.Now;
        AnsiConsole.MarkupInterpolated($"\nStart time was [bold green]{startTime}[/].");
        AnsiConsole.MarkupInterpolated($"\nEnd time was [bold green]{endTime}[/].");
        TimeSpan duration = endTime - startTime;
        AnsiConsole.MarkupInterpolated($"\nDuration was [bold green]{duration}[/].");
        Console.ReadLine();

        return new TimeEntry(startTime, endTime);
    }

}
