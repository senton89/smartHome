using Microsoft.AspNetCore.Mvc;
using SmartHomeSystem.APIs.Common;
using SmartHomeSystem.Infrastructure.Models;

namespace SmartHomeSystem.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class RoomFindManyArgs : FindManyInput<Room, RoomWhereInput> { }
