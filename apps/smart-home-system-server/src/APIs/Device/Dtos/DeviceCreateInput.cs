using SmartHomeSystem.Core.Enums;

namespace SmartHomeSystem.APIs.Dtos;

public class DeviceCreateInput
{
    public string? Name { get; set; }

    public string? RoomId { get; set; }

    public StatusEnum? Status { get; set; }

    public string? TypeField { get; set; }
}
