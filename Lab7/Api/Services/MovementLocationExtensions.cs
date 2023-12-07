using Api.Data.Entities;

namespace Api.Services;


public static class MovementLocationExtensions
{
    private static readonly TimeZoneInfo _ukrainianTimeZone = TimeZoneInfo
        .FindSystemTimeZoneById("Europe/Kiev");


    public static void TimeZoneToUkrainian(this MovementLocation movementLocation)
    {
        movementLocation.DateStarted = TimeZoneInfo.ConvertTime(
            movementLocation.DateStarted,
            _ukrainianTimeZone
        );
        movementLocation.DateCompleted = TimeZoneInfo.ConvertTime(
            movementLocation.DateCompleted,
            _ukrainianTimeZone
        );
    }
}