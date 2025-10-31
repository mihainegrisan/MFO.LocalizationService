namespace MFO.LocalizationService.Application.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}