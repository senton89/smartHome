using Microsoft.AspNetCore.Mvc;
using SmartHomeSystem.APIs.Common;
using SmartHomeSystem.Infrastructure.Models;

namespace SmartHomeSystem.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class UserSettingFindManyArgs : FindManyInput<UserSetting, UserSettingWhereInput> { }
