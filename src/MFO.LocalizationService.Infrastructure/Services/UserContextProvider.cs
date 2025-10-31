using MFO.LocalizationService.Application.Interfaces;

namespace MFO.LocalizationService.Infrastructure.Services;

public class UserContextProvider : IUserContextProvider
{
    public string? UserId => "system";
}