using Spectre.Console;
using System.Text;

namespace CodingTracker.CodingTrackerLibrary;

public class ManualEntry
{
    static public TimeEntry Start()
    {
        TimeEntry timeEntry = new TimeEntry();

        AnsiConsole.Markup("Enter [green]start[/] date and time. mm/dd/yyyy hh:mm");
        DateTime startTimeInput = DateTime.Parse(Console.ReadLine());

        AnsiConsole.Markup("Enter [green]end[/] date and time. mm/dd/yyyy hh:mm");
        DateTime endTimeInput = DateTime.Parse(Console.ReadLine());

        return new TimeEntry(startTimeInput, endTimeInput);
    }
}
