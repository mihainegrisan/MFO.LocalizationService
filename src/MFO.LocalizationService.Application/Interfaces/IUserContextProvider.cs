namespace MFO.LocalizationService.Application.Interfaces;

public interface IUserContextProvider
{
    string? UserId { get; }
}