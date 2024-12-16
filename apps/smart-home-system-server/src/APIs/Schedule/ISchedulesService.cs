using SmartHomeSystem.APIs.Common;
using SmartHomeSystem.APIs.Dtos;

namespace SmartHomeSystem.APIs;

public interface ISchedulesService
{
    /// <summary>
    /// Create one Schedule
    /// </summary>
    public Task<Schedule> CreateSchedule(ScheduleCreateInput schedule);

    /// <summary>
    /// Delete one Schedule
    /// </summary>
    public Task DeleteSchedule(ScheduleWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Schedules
    /// </summary>
    public Task<List<Schedule>> Schedules(ScheduleFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Schedule records
    /// </summary>
    public Task<MetadataDto> SchedulesMeta(ScheduleFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Schedule
    /// </summary>
    public Task<Schedule> Schedule(ScheduleWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Schedule
    /// </summary>
    public Task UpdateSchedule(ScheduleWhereUniqueInput uniqueId, ScheduleUpdateInput updateDto);

    /// <summary>
    /// Get a device record for Schedule
    /// </summary>
    public Task<Device> GetDevice(ScheduleWhereUniqueInput uniqueId);
}
