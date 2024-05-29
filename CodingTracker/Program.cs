using CodingTracker.CodingTrackerLibrary;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using System;
using CodingTracker.DataAccess;
using System.Collections.ObjectModel;
using System.Numerics;

namespace Program;

static class Program
{
    static void Main()
    {
        DataAccess.InitizeDatabase();


        string option1 = "Exit";
        string option2 = "Stopwatch Entry";
        string option3 = "Manual Entry";
        string option4 = "View/Modify Entries";

        Dictionary<string, Action> menuOptions = new Dictionary<string, Action>();
        menuOptions.Add(option1, () => { Console.WriteLine("Exiting!"); });
        menuOptions.Add(option2, () => { DataAccess.CreateRecord(StopwatchEntry.Start()); Main(); });
        menuOptions.Add(option3, () => { DataAccess.CreateRecord(ManualEntry.Start()); Main(); });
        menuOptions.Add(option4, () => { Console.WriteLine("View/Modify Entries!"); ViewModifyMenu(); });

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Main Menu")
            .AddChoices(new[] {
                option1, option2, option3, option4
            }));

        menuOptions[choice].Invoke();
    }

    static void ViewModifyMenu()
    {
        List<string> choices = new List<string>();
        choices.Add("Back to Main Menu");
        choices.Add("List Entries");
        choices.Add("Modify Entry");
        choices.Add("Delete Entry");

        Dictionary<string, Action> menuOptions = new Dictionary<string, Action>();
        menuOptions.Add(choices[0], () => { Main(); });
        menuOptions.Add(choices[1], () => { ListAllRecords(DataAccess.GetAllRecords()); ViewModifyMenu(); });
        menuOptions.Add(choices[2], () => { DataAccess.UpdateRecord(GetUserInput("update"), ManualEntry.Start()); ViewModifyMenu(); });
        menuOptions.Add(choices[3], () => { DataAccess.DeleteRecord(GetUserInput("delete")); ViewModifyMenu(); });

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("View/Modify Menu")
            .PageSize(10)
            .AddChoices(new[] {
                choices[0], choices[1], choices[2], choices[3]
            }));

        menuOptions[choice].Invoke();
    }

    private static void ListAllRecords(List<TimeEntry> timeEntries)
    {
        foreach (var e in timeEntries) { Console.WriteLine($"{e.Id} {e.StartTime} {e.EndTime} {e.Duration}"); }
        Console.Write("Press any key to continue.");
        Console.ReadLine();
    }

    private static int GetUserInput(string action)
    {
        Console.WriteLine($"Enter ID you want to {action}");
        return Int32.Parse(Console.ReadLine());
    }
}