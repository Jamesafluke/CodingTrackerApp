namespace CodingTracker.CodingTrackerLibrary;

public class TimeEntry
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration { get; set; }

    public TimeEntry()
    {

    }

    public TimeEntry(DateTime startTime, DateTime endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
        Duration = endTime - startTime;
    }


}
