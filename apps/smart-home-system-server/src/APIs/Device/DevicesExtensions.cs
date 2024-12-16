using SmartHomeSystem.APIs.Dtos;
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

        if (updateDto.CreatedAt != null)
        {
            device.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Room != null)
        {
            device.RoomId = updateDto.Room;
        }
        if (updateDto.UpdatedAt != null)
        {
            device.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return device;
    }
}
