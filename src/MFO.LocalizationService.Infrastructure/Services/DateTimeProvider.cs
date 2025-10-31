using MFO.LocalizationService.Application.Interfaces;

namespace MFO.LocalizationService.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}