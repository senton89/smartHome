using SmartHomeSystem.APIs.Common;
using SmartHomeSystem.APIs.Dtos;

namespace SmartHomeSystem.APIs;

public interface IDevicesService
{
    /// <summary>
    /// Create one Device
    /// </summary>
    public Task<Device> CreateDevice(DeviceCreateInput device);

    /// <summary>
    /// Delete one Device
    /// </summary>
    public Task DeleteDevice(DeviceWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Devices
    /// </summary>
    public Task<List<Device>> Devices(DeviceFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Device records
    /// </summary>
    public Task<MetadataDto> DevicesMeta(DeviceFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Device
    /// </summary>
    public Task<Device> Device(DeviceWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Device
    /// </summary>
    public Task UpdateDevice(DeviceWhereUniqueInput uniqueId, DeviceUpdateInput updateDto);

    /// <summary>
    /// Get a room record for Device
    /// </summary>
    public Task<Room> GetRoom(DeviceWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Schedules records to Device
    /// </summary>
    public Task ConnectSchedules(
        DeviceWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    );

    /// <summary>
    /// Disconnect multiple Schedules records from Device
    /// </summary>
    public Task DisconnectSchedules(
        DeviceWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    );

    /// <summary>
    /// Find multiple Schedules records for Device
    /// </summary>
    public Task<List<Schedule>> FindSchedules(
        DeviceWhereUniqueInput uniqueId,
        ScheduleFindManyArgs ScheduleFindManyArgs
    );

    /// <summary>
    /// Update multiple Schedules records for Device
    /// </summary>
    public Task UpdateSchedules(
        DeviceWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] schedulesId
    );
}
