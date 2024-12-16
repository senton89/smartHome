using Microsoft.AspNetCore.Mvc;
using SmartHomeSystem.APIs;
using SmartHomeSystem.APIs.Common;
using SmartHomeSystem.APIs.Dtos;
using SmartHomeSystem.APIs.Errors;

namespace SmartHomeSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class RoomsControllerBase : ControllerBase
{
    protected readonly IRoomsService _service;

    public RoomsControllerBase(IRoomsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Room
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Room>> CreateRoom(RoomCreateInput input)
    {
        var room = await _service.CreateRoom(input);

        return CreatedAtAction(nameof(Room), new { id = room.Id }, room);
    }

    /// <summary>
    /// Delete one Room
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteRoom([FromRoute()] RoomWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteRoom(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Rooms
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Room>>> Rooms([FromQuery()] RoomFindManyArgs filter)
    {
        return Ok(await _service.Rooms(filter));
    }

    /// <summary>
    /// Meta data about Room records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> RoomsMeta([FromQuery()] RoomFindManyArgs filter)
    {
        return Ok(await _service.RoomsMeta(filter));
    }

    /// <summary>
    /// Get one Room
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Room>> Room([FromRoute()] RoomWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Room(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Room
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateRoom(
        [FromRoute()] RoomWhereUniqueInput uniqueId,
        [FromQuery()] RoomUpdateInput roomUpdateDto
    )
    {
        try
        {
            await _service.UpdateRoom(uniqueId, roomUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Devices records to Room
    /// </summary>
    [HttpPost("{Id}/devices")]
    public async Task<ActionResult> ConnectDevices(
        [FromRoute()] RoomWhereUniqueInput uniqueId,
        [FromQuery()] DeviceWhereUniqueInput[] devicesId
    )
    {
        try
        {
            await _service.ConnectDevices(uniqueId, devicesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Devices records from Room
    /// </summary>
    [HttpDelete("{Id}/devices")]
    public async Task<ActionResult> DisconnectDevices(
        [FromRoute()] RoomWhereUniqueInput uniqueId,
        [FromBody()] DeviceWhereUniqueInput[] devicesId
    )
    {
        try
        {
            await _service.DisconnectDevices(uniqueId, devicesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Devices records for Room
    /// </summary>
    [HttpGet("{Id}/devices")]
    public async Task<ActionResult<List<Device>>> FindDevices(
        [FromRoute()] RoomWhereUniqueInput uniqueId,
        [FromQuery()] DeviceFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindDevices(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Devices records for Room
    /// </summary>
    [HttpPatch("{Id}/devices")]
    public async Task<ActionResult> UpdateDevices(
        [FromRoute()] RoomWhereUniqueInput uniqueId,
        [FromBody()] DeviceWhereUniqueInput[] devicesId
    )
    {
        try
        {
            await _service.UpdateDevices(uniqueId, devicesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
