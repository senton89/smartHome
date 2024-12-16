namespace SmartHomeSystem.APIs.Dtos;

public class RoomCreateInput
{
    public DateTime CreatedAt { get; set; }

    public List<Device>? Devices { get; set; }

    public int? Floor { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public DateTime UpdatedAt { get; set; }
}
