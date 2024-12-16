using Microsoft.AspNetCore.Mvc;
using SmartHomeSystem.APIs;
using SmartHomeSystem.APIs.Common;
using SmartHomeSystem.APIs.Dtos;
using SmartHomeSystem.APIs.Errors;

namespace SmartHomeSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class SchedulesControllerBase : ControllerBase
{
    protected readonly ISchedulesService _service;

    public SchedulesControllerBase(ISchedulesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Schedule
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Schedule>> CreateSchedule(ScheduleCreateInput input)
    {
        var schedule = await _service.CreateSchedule(input);

        return CreatedAtAction(nameof(Schedule), new { id = schedule.Id }, schedule);
    }

    /// <summary>
    /// Delete one Schedule
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteSchedule([FromRoute()] ScheduleWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteSchedule(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Schedules
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Schedule>>> Schedules(
        [FromQuery()] ScheduleFindManyArgs filter
    )
    {
        return Ok(await _service.Schedules(filter));
    }

    /// <summary>
    /// Meta data about Schedule records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> SchedulesMeta(
        [FromQuery()] ScheduleFindManyArgs filter
    )
    {
        return Ok(await _service.SchedulesMeta(filter));
    }

    /// <summary>
    /// Get one Schedule
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Schedule>> Schedule(
        [FromRoute()] ScheduleWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Schedule(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Schedule
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateSchedule(
        [FromRoute()] ScheduleWhereUniqueInput uniqueId,
        [FromQuery()] ScheduleUpdateInput scheduleUpdateDto
    )
    {
        try
        {
            await _service.UpdateSchedule(uniqueId, scheduleUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a device record for Schedule
    /// </summary>
    [HttpGet("{Id}/device")]
    public async Task<ActionResult<List<Device>>> GetDevice(
        [FromRoute()] ScheduleWhereUniqueInput uniqueId
    )
    {
        var device = await _service.GetDevice(uniqueId);
        return Ok(device);
    }
}
