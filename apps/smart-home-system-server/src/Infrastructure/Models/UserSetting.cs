using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeSystem.Infrastructure.Models;

[Table("UserSettings")]
public class UserSettingDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public bool? NotificationsEnabled { get; set; }

    [StringLength(1000)]
    public string? Theme { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
