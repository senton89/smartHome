using Microsoft.EntityFrameworkCore;
using SmartHomeSystem.APIs;
using SmartHomeSystem.APIs.Common;
using SmartHomeSystem.APIs.Dtos;
using SmartHomeSystem.APIs.Errors;
using SmartHomeSystem.APIs.Extensions;
using SmartHomeSystem.Infrastructure;
using SmartHomeSystem.Infrastructure.Models;

namespace SmartHomeSystem.APIs;

public abstract class UserSettingsServiceBase : IUserSettingsService
{
    protected readonly SmartHomeSystemDbContext _context;

    public UserSettingsServiceBase(SmartHomeSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one UserSetting
    /// </summary>
    public async Task<UserSetting> CreateUserSetting(UserSettingCreateInput createDto)
    {
        var userSetting = new UserSettingDbModel
        {
            CreatedAt = createDto.CreatedAt,
            NotificationsEnabled = createDto.NotificationsEnabled,
            Theme = createDto.Theme,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            userSetting.Id = createDto.Id;
        }

        _context.UserSettings.Add(userSetting);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<UserSettingDbModel>(userSetting.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one UserSetting
    /// </summary>
    public async Task DeleteUserSetting(UserSettingWhereUniqueInput uniqueId)
    {
        var userSetting = await _context.UserSettings.FindAsync(uniqueId.Id);
        if (userSetting == null)
        {
            throw new NotFoundException();
        }

        _context.UserSettings.Remove(userSetting);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many UserSettings
    /// </summary>
    public async Task<List<UserSetting>> UserSettings(UserSettingFindManyArgs findManyArgs)
    {
        var userSettings = await _context
            .UserSettings.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return userSettings.ConvertAll(userSetting => userSetting.ToDto());
    }

    /// <summary>
    /// Meta data about UserSetting records
    /// </summary>
    public async Task<MetadataDto> UserSettingsMeta(UserSettingFindManyArgs findManyArgs)
    {
        var count = await _context.UserSettings.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one UserSetting
    /// </summary>
    public async Task<UserSetting> UserSetting(UserSettingWhereUniqueInput uniqueId)
    {
        var userSettings = await this.UserSettings(
            new UserSettingFindManyArgs { Where = new UserSettingWhereInput { Id = uniqueId.Id } }
        );
        var userSetting = userSettings.FirstOrDefault();
        if (userSetting == null)
        {
            throw new NotFoundException();
        }

        return userSetting;
    }

    /// <summary>
    /// Update one UserSetting
    /// </summary>
    public async Task UpdateUserSetting(
        UserSettingWhereUniqueInput uniqueId,
        UserSettingUpdateInput updateDto
    )
    {
        var userSetting = updateDto.ToModel(uniqueId);

        _context.Entry(userSetting).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.UserSettings.Any(e => e.Id == userSetting.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
