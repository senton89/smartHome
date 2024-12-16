using SmartHomeSystem.APIs.Dtos;
using SmartHomeSystem.Infrastructure.Models;

namespace SmartHomeSystem.APIs.Extensions;

public static class UserSettingsExtensions
{
    public static UserSetting ToDto(this UserSettingDbModel model)
    {
        return new UserSetting
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            NotificationsEnabled = model.NotificationsEnabled,
            Theme = model.Theme,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static UserSettingDbModel ToModel(
        this UserSettingUpdateInput updateDto,
        UserSettingWhereUniqueInput uniqueId
    )
    {
        var userSetting = new UserSettingDbModel
        {
            Id = uniqueId.Id,
            NotificationsEnabled = updateDto.NotificationsEnabled,
            Theme = updateDto.Theme
        };

        if (updateDto.CreatedAt != null)
        {
            userSetting.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            userSetting.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return userSetting;
    }
}
