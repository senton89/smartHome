using Microsoft.EntityFrameworkCore;
using SmartHomeSystem.APIs;
using SmartHomeSystem.APIs.Common;
using SmartHomeSystem.APIs.Dtos;
using SmartHomeSystem.APIs.Errors;
using SmartHomeSystem.APIs.Extensions;
using SmartHomeSystem.Infrastructure;
using SmartHomeSystem.Infrastructure.Models;

namespace SmartHomeSystem.APIs;

public abstract class SchedulesServiceBase : ISchedulesService
{
    protected readonly SmartHomeSystemDbContext _context;

    public SchedulesServiceBase(SmartHomeSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Schedule
    /// </summary>
    public async Task<Schedule> CreateSchedule(ScheduleCreateInput createDto)
    {
        var schedule = new ScheduleDbModel
        {
            CreatedAt = createDto.CreatedAt,
            EndTime = createDto.EndTime,
            StartTime = createDto.StartTime,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            schedule.Id = createDto.Id;
        }
        if (createDto.Device != null)
        {
            schedule.Device = await _context
                .Devices.Where(device => createDto.Device.Id == device.Id)
                .FirstOrDefaultAsync();
        }

        _context.Schedules.Add(schedule);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ScheduleDbModel>(schedule.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Schedule
    /// </summary>
    public async Task DeleteSchedule(ScheduleWhereUniqueInput uniqueId)
    {
        var schedule = await _context.Schedules.FindAsync(uniqueId.Id);
        if (schedule == null)
        {
            throw new NotFoundException();
        }

        _context.Schedules.Remove(schedule);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Schedules
    /// </summary>
    public async Task<List<Schedule>> Schedules(ScheduleFindManyArgs findManyArgs)
    {
        var schedules = await _context
            .Schedules.Include(x => x.Device)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return schedules.ConvertAll(schedule => schedule.ToDto());
    }

    /// <summary>
    /// Meta data about Schedule records
    /// </summary>
    public async Task<MetadataDto> SchedulesMeta(ScheduleFindManyArgs findManyArgs)
    {
        var count = await _context.Schedules.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Schedule
    /// </summary>
    public async Task<Schedule> Schedule(ScheduleWhereUniqueInput uniqueId)
    {
        var schedules = await this.Schedules(
            new ScheduleFindManyArgs { Where = new ScheduleWhereInput { Id = uniqueId.Id } }
        );
        var schedule = schedules.FirstOrDefault();
        if (schedule == null)
        {
            throw new NotFoundException();
        }

        return schedule;
    }

    /// <summary>
    /// Update one Schedule
    /// </summary>
    public async Task UpdateSchedule(
        ScheduleWhereUniqueInput uniqueId,
        ScheduleUpdateInput updateDto
    )
    {
        var schedule = updateDto.ToModel(uniqueId);

        if (updateDto.Device != null)
        {
            schedule.Device = await _context
                .Devices.Where(device => updateDto.Device == device.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(schedule).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Schedules.Any(e => e.Id == schedule.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a device record for Schedule
    /// </summary>
    public async Task<Device> GetDevice(ScheduleWhereUniqueInput uniqueId)
    {
        var schedule = await _context
            .Schedules.Where(schedule => schedule.Id == uniqueId.Id)
            .Include(schedule => schedule.Device)
            .FirstOrDefaultAsync();
        if (schedule == null)
        {
            throw new NotFoundException();
        }
        return schedule.Device.ToDto();
    }
}
