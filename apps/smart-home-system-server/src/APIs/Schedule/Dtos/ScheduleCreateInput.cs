namespace SmartHomeSystem.APIs.Dtos;

public class ScheduleCreateInput
{
    public string? DeviceId { get; set; }
    public DateTime? EndTime { get; set; }
    public DateTime? StartTime { get; set; }
}
