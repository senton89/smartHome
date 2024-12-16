using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeSystem.Infrastructure.Models;

[Table("Rooms")]
public class RoomDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public List<DeviceDbModel>? Devices { get; set; } = new List<DeviceDbModel>();

    [Range(-999999999, 999999999)]
    public int? Floor { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
