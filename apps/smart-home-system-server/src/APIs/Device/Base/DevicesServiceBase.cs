using Microsoft.EntityFrameworkCore;
using SmartHomeSystem.APIs;
using SmartHomeSystem.APIs.Common;
using SmartHomeSystem.APIs.Dtos;
using SmartHomeSystem.APIs.Errors;
using SmartHomeSystem.APIs.Extensions;
using SmartHomeSystem.Infrastructure;
using SmartHomeSystem.Infrastructure.Models;

namespace SmartHomeSystem.APIs;

public abstract class DevicesServiceBase : IDevicesService
{
    protected readonly SmartHomeSystemDbContext _context;

    public DevicesServiceBase(SmartHomeSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Device
    /// </summary>
    public async Task<Device> CreateDevice(DeviceCreateInput createDto)
    {
        var device = new DeviceDbModel
        {
            CreatedAt = DateTime.Now.ToUniversalTime(),
            Name = createDto.Name,
            Status = createDto.Status,
            TypeField = createDto.TypeField,
            UpdatedAt = DateTime.Now.ToUniversalTime(),
            RoomId = createDto.RoomId,
        };

        device.Id = Guid.NewGuid().ToString();
        if (createDto.RoomId != null)
        {
            device.Room = await _context
                .Rooms.Where(room => createDto.RoomId == room.Id)
                .FirstOrDefaultAsync();
        }

        _context.Devices.Add(device);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<DeviceDbModel>(device.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Device
    /// </summary>
    public async Task DeleteDevice(DeviceWhereUniqueInput uniqueId)
    {
        var device = await _context.Devices.FindAsync(uniqueId.Id);
        if (device == null)
        {
            throw new NotFoundException();
        }

        _context.Devices.Remove(device);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Devices
    /// </summary>
    public async Task<List<Device>> Devices(DeviceFindManyArgs findManyArgs)
    {
        var devices = await _context
            .Devices.Include(x => x.Room)
            .Include(x => x.Schedules)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return devices.ConvertAll(device => device.ToDto());
    }

    /// <summary>
    /// Meta data about Device records
    /// </summary>
    public async Task<MetadataDto> DevicesMeta(DeviceFindManyArgs findManyArgs)
    {
        var count = await _context.Devices.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Device
    /// </summary>
    public async Task<Device> Device(DeviceWhereUniqueInput uniqueId)
    {
        var devices = await this.Devices(
            new DeviceFindManyArgs { Where = new DeviceWhereInput { Id = uniqueId.Id } }
        );
        var device = devices.FirstOrDefault();
        if (device == null)
        {
            throw new NotFoundException();
        }

        return device;
    }

    /// <summary>
    /// Update one Device
    /// </summary>
    public async Task UpdateDevice(DeviceWhereUniqueInput uniqueId, DeviceUpdateInput updateDto)
    {
        var device = updateDto.ToModel(uniqueId);

        if (updateDto.RoomId != null)
        {
            device.Room = await _context
                .Rooms.Where(room => updateDto.RoomId == room.Id)
                .FirstOrDefaultAsync();
        }

        // if (updateDto.Schedules != null)
        // {
        //     var searched = await _context
        //         .Schedules.Where(schedule =>
        //             updateDto.Schedules.Select(t => t.StartTime.ToString()).Contains(schedule.StartTime.ToString())
        //             && updateDto.Schedules.Select(t => t.EndTime.ToString()).Contains(schedule.EndTime.ToString())
        //         )
        //         .ToListAsync();
        //
        //     if (searched.Count == 0)
        //     {
        //         // Создаем новые расписания
        //         List<Schedule> newSchedules = new List<Schedule>();
        //
        //         foreach (var scheduleDto in updateDto.Schedules)
        //         {
        //             var newSchedule = new Schedule
        //             {
        //                 Id = Guid.NewGuid().ToString(), // Генерируем новый уникальный идентификатор
        //                 CreatedAt = DateTime.UtcNow,
        //                 UpdatedAt = DateTime.UtcNow,
        //                 Device = scheduleDto.Device,
        //                 StartTime = updateDto.Schedules[0].StartTime,
        //                 EndTime = updateDto.Schedules[0].StartTime
        //             };
        //
        //             newSchedules.Add(newSchedule);
        //         }
        //
        //         await _context.Schedules.AddRangeAsync(newSchedules);
        //         await _context.SaveChangesAsync();
        //
        //         // Присваиваем новые расписания устройству
        //         device.Schedules = newSchedules;
        //     }
        //     else
        //     {
        //         // Если расписания найдены, присваиваем их устройству
        //         device.Schedules = searched;
        //     }
        // }

        _context.Entry(device).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Devices.Any(e => e.Id == device.Id))
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
    /// Get a room record for Device
    /// </summary>
    public async Task<Room> GetRoom(DeviceWhereUniqueInput uniqueId)
    {
        var device = await _context
            .Devices.Where(device => device.Id == uniqueId.Id)
            .Include(device => device.Room)
            .FirstOrDefaultAsync();
        if (device == null)
        {
            throw new NotFoundException();
        }
        return device.Room.ToDto();
    }

    /// <summary>
    /// Connect multiple Schedules records to Device
    /// </summary>
    public async Task ConnectSchedules(
        DeviceWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Devices.Include(x => x.Schedules)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Schedules.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Schedules);

        foreach (var child in childrenToConnect)
        {
            parent.Schedules.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Schedules records from Device
    /// </summary>
    public async Task DisconnectSchedules(
        DeviceWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Devices.Include(x => x.Schedules)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Schedules.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Schedules?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Schedules records for Device
    /// </summary>
    public async Task<List<Schedule>> FindSchedules(
        DeviceWhereUniqueInput uniqueId,
        ScheduleFindManyArgs deviceFindManyArgs
    )
    {
        var schedules = await _context
            .Schedules.Where(m => m.DeviceId == uniqueId.Id)
            .ApplyWhere(deviceFindManyArgs.Where)
            .ApplySkip(deviceFindManyArgs.Skip)
            .ApplyTake(deviceFindManyArgs.Take)
            .ApplyOrderBy(deviceFindManyArgs.SortBy)
            .ToListAsync();

        return schedules.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Schedules records for Device
    /// </summary>
    public async Task UpdateSchedules(
        DeviceWhereUniqueInput uniqueId,
        ScheduleWhereUniqueInput[] childrenIds
    )
    {
        var device = await _context
            .Devices.Include(t => t.Schedules)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (device == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Schedules.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        device.Schedules = children;
        await _context.SaveChangesAsync();
    }
}
