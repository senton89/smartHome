using SmartHomeSystem.APIs.Common;
using SmartHomeSystem.APIs.Dtos;

namespace SmartHomeSystem.APIs;

public interface IUserSettingsService
{
    /// <summary>
    /// Create one UserSetting
    /// </summary>
    public Task<UserSetting> CreateUserSetting(UserSettingCreateInput usersetting);

    /// <summary>
    /// Delete one UserSetting
    /// </summary>
    public Task DeleteUserSetting(UserSettingWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many UserSettings
    /// </summary>
    public Task<List<UserSetting>> UserSettings(UserSettingFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about UserSetting records
    /// </summary>
    public Task<MetadataDto> UserSettingsMeta(UserSettingFindManyArgs findManyArgs);

    /// <summary>
    /// Get one UserSetting
    /// </summary>
    public Task<UserSetting> UserSetting(UserSettingWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one UserSetting
    /// </summary>
    public Task UpdateUserSetting(
        UserSettingWhereUniqueInput uniqueId,
        UserSettingUpdateInput updateDto
    );
}
