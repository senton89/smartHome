using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeSystem.Infrastructure.Models;

[Table("Schedules")]
public class ScheduleDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? DeviceId { get; set; }

    [ForeignKey(nameof(DeviceId))]
    public DeviceDbModel? Device { get; set; } = null;

    public DateTime? EndTime { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public DateTime? StartTime { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
