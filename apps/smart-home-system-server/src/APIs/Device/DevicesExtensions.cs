using SmartHomeSystem.APIs.Dtos;
using SmartHomeSystem.Infrastructure;
using SmartHomeSystem.Infrastructure.Models;

namespace SmartHomeSystem.APIs.Extensions;

public static class DevicesExtensions
{
    public static Device ToDto(this DeviceDbModel model)
    {
        return new Device
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Name = model.Name,
            Room = model.RoomId,
            Schedules = model.Schedules?.Select(x => x.Id).ToList(),
            Status = model.Status,
            TypeField = model.TypeField,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static DeviceDbModel ToModel(
        this DeviceUpdateInput updateDto,
        DeviceWhereUniqueInput uniqueId
    )
    {
        var device = new DeviceDbModel
        {
            Id = uniqueId.Id,
            Name = updateDto.Name,
            Status = updateDto.Status,
            TypeField = updateDto.TypeField
        };

        device.CreatedAt = DateTime.Now.ToUniversalTime();

        if (updateDto.RoomId != null)
        {
            device.RoomId = updateDto.RoomId;
        }

        device.UpdatedAt = DateTime.Now.ToUniversalTime(); //updateDto.UpdatedAt.Value.ToUniversalTime();

        return device;
    }
}
