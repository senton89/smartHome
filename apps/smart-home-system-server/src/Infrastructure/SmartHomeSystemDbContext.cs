using Microsoft.EntityFrameworkCore;
using SmartHomeSystem.Infrastructure.Models;

namespace SmartHomeSystem.Infrastructure;

public class SmartHomeSystemDbContext : DbContext
{
    public SmartHomeSystemDbContext(DbContextOptions<SmartHomeSystemDbContext> options)
        : base(options) { }

    public DbSet<DeviceDbModel> Devices { get; set; }

    public DbSet<RoomDbModel> Rooms { get; set; }

    public DbSet<UserSettingDbModel> UserSettings { get; set; }

    public DbSet<ScheduleDbModel> Schedules { get; set; }
}
