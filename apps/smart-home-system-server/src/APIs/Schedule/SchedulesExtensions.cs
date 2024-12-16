using SmartHomeSystem.APIs.Dtos;
using SmartHomeSystem.Infrastructure.Models;

namespace SmartHomeSystem.APIs.Extensions;

public static class SchedulesExtensions
{
    public static Schedule ToDto(this ScheduleDbModel model)
    {
        return new Schedule
        {
            CreatedAt = model.CreatedAt,
            Device = model.DeviceId,
            EndTime = model.EndTime,
            Id = model.Id,
            StartTime = model.StartTime,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ScheduleDbModel ToModel(
        this ScheduleUpdateInput updateDto,
        ScheduleWhereUniqueInput uniqueId
    )
    {
        var schedule = new ScheduleDbModel
        {
            Id = uniqueId.Id,
            EndTime = updateDto.EndTime,
            StartTime = updateDto.StartTime
        };

        if (updateDto.CreatedAt != null)
        {
            schedule.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Device != null)
        {
            schedule.DeviceId = updateDto.Device;
        }
        if (updateDto.UpdatedAt != null)
        {
            schedule.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return schedule;
    }
}
