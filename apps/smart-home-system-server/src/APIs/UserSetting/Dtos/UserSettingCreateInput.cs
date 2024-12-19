namespace SmartHomeSystem.APIs.Dtos;

public class UserSettingCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public bool? NotificationsEnabled { get; set; }

    public string? Theme { get; set; }

    public DateTime UpdatedAt { get; set; }
}