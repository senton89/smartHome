namespace SmartHomeSystem.APIs.Dtos;

public class Schedule
{
    public DateTime CreatedAt { get; set; }

    public string? Device { get; set; }

    public DateTime? EndTime { get; set; }

    public string Id { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime UpdatedAt { get; set; }
}
