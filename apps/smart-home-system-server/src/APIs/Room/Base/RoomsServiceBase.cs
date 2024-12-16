using Microsoft.EntityFrameworkCore;
using SmartHomeSystem.APIs;
using SmartHomeSystem.APIs.Common;
using SmartHomeSystem.APIs.Dtos;
using SmartHomeSystem.APIs.Errors;
using SmartHomeSystem.APIs.Extensions;
using SmartHomeSystem.Infrastructure;
using SmartHomeSystem.Infrastructure.Models;

namespace SmartHomeSystem.APIs;

public abstract class RoomsServiceBase : IRoomsService
{
    protected readonly SmartHomeSystemDbContext _context;

    public RoomsServiceBase(SmartHomeSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Room
    /// </summary>
    public async Task<Room> CreateRoom(RoomCreateInput createDto)
    {
        var room = new RoomDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Floor = createDto.Floor,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            room.Id = createDto.Id;
        }
        if (createDto.Devices != null)
        {
            room.Devices = await _context
                .Devices.Where(device => createDto.Devices.Select(t => t.Id).Contains(device.Id))
                .ToListAsync();
        }

        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<RoomDbModel>(room.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Room
    /// </summary>
    public async Task DeleteRoom(RoomWhereUniqueInput uniqueId)
    {
        var room = await _context.Rooms.FindAsync(uniqueId.Id);
        if (room == null)
        {
            throw new NotFoundException();
        }

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Rooms
    /// </summary>
    public async Task<List<Room>> Rooms(RoomFindManyArgs findManyArgs)
    {
        var rooms = await _context
            .Rooms.Include(x => x.Devices)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return rooms.ConvertAll(room => room.ToDto());
    }

    /// <summary>
    /// Meta data about Room records
    /// </summary>
    public async Task<MetadataDto> RoomsMeta(RoomFindManyArgs findManyArgs)
    {
        var count = await _context.Rooms.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Room
    /// </summary>
    public async Task<Room> Room(RoomWhereUniqueInput uniqueId)
    {
        var rooms = await this.Rooms(
            new RoomFindManyArgs { Where = new RoomWhereInput { Id = uniqueId.Id } }
        );
        var room = rooms.FirstOrDefault();
        if (room == null)
        {
            throw new NotFoundException();
        }

        return room;
    }

    /// <summary>
    /// Update one Room
    /// </summary>
    public async Task UpdateRoom(RoomWhereUniqueInput uniqueId, RoomUpdateInput updateDto)
    {
        var room = updateDto.ToModel(uniqueId);

        if (updateDto.Devices != null)
        {
            room.Devices = await _context
                .Devices.Where(device => updateDto.Devices.Select(t => t).Contains(device.Id))
                .ToListAsync();
        }

        _context.Entry(room).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Rooms.Any(e => e.Id == room.Id))
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
    /// Connect multiple Devices records to Room
    /// </summary>
    public async Task ConnectDevices(
        RoomWhereUniqueInput uniqueId,
        DeviceWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Rooms.Include(x => x.Devices)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Devices.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Devices);

        foreach (var child in childrenToConnect)
        {
            parent.Devices.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Devices records from Room
    /// </summary>
    public async Task DisconnectDevices(
        RoomWhereUniqueInput uniqueId,
        DeviceWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Rooms.Include(x => x.Devices)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Devices.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Devices?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Devices records for Room
    /// </summary>
    public async Task<List<Device>> FindDevices(
        RoomWhereUniqueInput uniqueId,
        DeviceFindManyArgs roomFindManyArgs
    )
    {
        var devices = await _context
            .Devices.Where(m => m.RoomId == uniqueId.Id)
            .ApplyWhere(roomFindManyArgs.Where)
            .ApplySkip(roomFindManyArgs.Skip)
            .ApplyTake(roomFindManyArgs.Take)
            .ApplyOrderBy(roomFindManyArgs.SortBy)
            .ToListAsync();

        return devices.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Devices records for Room
    /// </summary>
    public async Task UpdateDevices(
        RoomWhereUniqueInput uniqueId,
        DeviceWhereUniqueInput[] childrenIds
    )
    {
        var room = await _context
            .Rooms.Include(t => t.Devices)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (room == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Devices.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        room.Devices = children;
        await _context.SaveChangesAsync();
    }
}
