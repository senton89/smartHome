using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeSystem.Core.Enums;

namespace SmartHomeSystem.Infrastructure.Models;

[Table("Devices")]
public class DeviceDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    public string? RoomId { get; set; }

    [ForeignKey(nameof(RoomId))]
    public RoomDbModel? Room { get; set; } = null;

    public List<ScheduleDbModel>? Schedules { get; set; } = new List<ScheduleDbModel>();

    public StatusEnum? Status { get; set; }

    [StringLength(1000)]
    public string? TypeField { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
