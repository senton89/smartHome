using SmartHomeSystem.APIs.Common;
using SmartHomeSystem.APIs.Dtos;

namespace SmartHomeSystem.APIs;

public interface IRoomsService
{
    /// <summary>
    /// Create one Room
    /// </summary>
    public Task<Room> CreateRoom(RoomCreateInput room);

    /// <summary>
    /// Delete one Room
    /// </summary>
    public Task DeleteRoom(RoomWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Rooms
    /// </summary>
    public Task<List<Room>> Rooms(RoomFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Room records
    /// </summary>
    public Task<MetadataDto> RoomsMeta(RoomFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Room
    /// </summary>
    public Task<Room> Room(RoomWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Room
    /// </summary>
    public Task UpdateRoom(RoomWhereUniqueInput uniqueId, RoomUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Devices records to Room
    /// </summary>
    public Task ConnectDevices(RoomWhereUniqueInput uniqueId, DeviceWhereUniqueInput[] devicesId);

    /// <summary>
    /// Disconnect multiple Devices records from Room
    /// </summary>
    public Task DisconnectDevices(
        RoomWhereUniqueInput uniqueId,
        DeviceWhereUniqueInput[] devicesId
    );

    /// <summary>
    /// Find multiple Devices records for Room
    /// </summary>
    public Task<List<Device>> FindDevices(
        RoomWhereUniqueInput uniqueId,
        DeviceFindManyArgs DeviceFindManyArgs
    );

    /// <summary>
    /// Update multiple Devices records for Room
    /// </summary>
    public Task UpdateDevices(RoomWhereUniqueInput uniqueId, DeviceWhereUniqueInput[] devicesId);
}
