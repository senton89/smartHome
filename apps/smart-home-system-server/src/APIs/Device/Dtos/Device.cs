using SmartHomeSystem.Core.Enums;

namespace SmartHomeSystem.APIs.Dtos;

public class Device
{
    public DateTime CreatedAt { get; set; }

    public string Id { get; set; }

    public string? Name { get; set; }

    public string? Room { get; set; }

    public List<string>? Schedules { get; set; }

    public StatusEnum? Status { get; set; }

    public string? TypeField { get; set; }

    public DateTime UpdatedAt { get; set; }
}
