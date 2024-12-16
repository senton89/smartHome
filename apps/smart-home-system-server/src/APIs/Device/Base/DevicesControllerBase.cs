using Microsoft.AspNetCore.Mvc;
using SmartHomeSystem.APIs;
using SmartHomeSystem.APIs.Common;
using SmartHomeSystem.APIs.Dtos;
using SmartHomeSystem.APIs.Errors;

namespace SmartHomeSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class DevicesControllerBase : ControllerBase
{
    protected readonly IDevicesService _service;

    public DevicesControllerBase(IDevicesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Device
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Device>> CreateDevice(DeviceCreateInput input)
    {
        var device = await _service.CreateDevice(input);

        return CreatedAtAction(nameof(Device), new { id = device.Id }, device);
    }

    /// <summary>
    /// Delete one Device
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteDevice([FromRoute()] DeviceWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteDevice(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Devices
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Device>>> Devices([FromQuery()] DeviceFindManyArgs filter)
    {
        return Ok(await _service.Devices(filter));
    }

    /// <summary>
    /// Meta data about Device records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> DevicesMeta(
        [FromQuery()] DeviceFindManyArgs filter
    )
    {
        return Ok(await _service.DevicesMeta(filter));
    }

    /// <summary>
    /// Get one Device
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Device>> Device([FromRoute()] DeviceWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Device(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Device
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateDevice(
        [FromRoute()] DeviceWhereUniqueInput uniqueId,
        [FromQuery()] DeviceUpdateInput deviceUpdateDto
    )
    {
        try
        {
            await _service.UpdateDevice(uniqueId, deviceUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a room record for Device
    /// </summary>
    [HttpGet("{Id}/room")]
    public async Task<ActionResult<List<Room>>> GetRoom(
        [FromRoute()] DeviceWhereUniqueInput uniqueId
    )
    {
        var room = await _service.GetRoom(uniqueId);
        return Ok(room);
    }

    /// <summary>
    /// Connect multiple Schedules records to Device
    /// </summary>
    [HttpPost("{Id}/schedules")]
    public async Task<ActionResult> ConnectSchedules(
        [FromRoute()] DeviceWhereUniqueInput uniqueId,
        [FromQuery()] ScheduleWhereUniqueInput[] schedulesId
    )
    {
        try
        {
            await _service.ConnectSchedules(uniqueId, schedulesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Schedules records from Device
    /// </summary>
    [HttpDelete("{Id}/schedules")]
    public async Task<ActionResult> DisconnectSchedules(
        [FromRoute()] DeviceWhereUniqueInput uniqueId,
        [FromBody()] ScheduleWhereUniqueInput[] schedulesId
    )
    {
        try
        {
            await _service.DisconnectSchedules(uniqueId, schedulesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Schedules records for Device
    /// </summary>
    [HttpGet("{Id}/schedules")]
    public async Task<ActionResult<List<Schedule>>> FindSchedules(
        [FromRoute()] DeviceWhereUniqueInput uniqueId,
        [FromQuery()] ScheduleFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindSchedules(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Schedules records for Device
    /// </summary>
    [HttpPatch("{Id}/schedules")]
    public async Task<ActionResult> UpdateSchedules(
        [FromRoute()] DeviceWhereUniqueInput uniqueId,
        [FromBody()] ScheduleWhereUniqueInput[] schedulesId
    )
    {
        try
        {
            await _service.UpdateSchedules(uniqueId, schedulesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
