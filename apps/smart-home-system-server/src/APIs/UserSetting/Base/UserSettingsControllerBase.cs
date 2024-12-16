using Microsoft.AspNetCore.Mvc;
using SmartHomeSystem.APIs;
using SmartHomeSystem.APIs.Common;
using SmartHomeSystem.APIs.Dtos;
using SmartHomeSystem.APIs.Errors;

namespace SmartHomeSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class UserSettingsControllerBase : ControllerBase
{
    protected readonly IUserSettingsService _service;

    public UserSettingsControllerBase(IUserSettingsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one UserSetting
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<UserSetting>> CreateUserSetting(UserSettingCreateInput input)
    {
        var userSetting = await _service.CreateUserSetting(input);

        return CreatedAtAction(nameof(UserSetting), new { id = userSetting.Id }, userSetting);
    }

    /// <summary>
    /// Delete one UserSetting
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteUserSetting(
        [FromRoute()] UserSettingWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteUserSetting(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many UserSettings
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<UserSetting>>> UserSettings(
        [FromQuery()] UserSettingFindManyArgs filter
    )
    {
        return Ok(await _service.UserSettings(filter));
    }

    /// <summary>
    /// Meta data about UserSetting records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> UserSettingsMeta(
        [FromQuery()] UserSettingFindManyArgs filter
    )
    {
        return Ok(await _service.UserSettingsMeta(filter));
    }

    /// <summary>
    /// Get one UserSetting
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<UserSetting>> UserSetting(
        [FromRoute()] UserSettingWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.UserSetting(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one UserSetting
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateUserSetting(
        [FromRoute()] UserSettingWhereUniqueInput uniqueId,
        [FromQuery()] UserSettingUpdateInput userSettingUpdateDto
    )
    {
        try
        {
            await _service.UpdateUserSetting(uniqueId, userSettingUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
