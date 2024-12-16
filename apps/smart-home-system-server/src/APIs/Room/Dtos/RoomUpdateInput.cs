namespace SmartHomeSystem.APIs.Dtos;

public class RoomUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public List<string>? Devices { get; set; }

    public int? Floor { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
