using SmartHomeSystem.APIs.Dtos;
using SmartHomeSystem.Infrastructure.Models;

namespace SmartHomeSystem.APIs.Extensions;

public static class RoomsExtensions
{
    public static Room ToDto(this RoomDbModel model)
    {
        return new Room
        {
            CreatedAt = model.CreatedAt,
            Devices = model.Devices?.Select(x => x.Id).ToList(),
            Floor = model.Floor,
            Id = model.Id,
            Name = model.Name,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static RoomDbModel ToModel(this RoomUpdateInput updateDto, RoomWhereUniqueInput uniqueId)
    {
        var room = new RoomDbModel
        {
            Id = uniqueId.Id,
            Floor = updateDto.Floor,
            Name = updateDto.Name
        };

        if (updateDto.CreatedAt != null)
        {
            room.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            room.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return room;
    }
}
