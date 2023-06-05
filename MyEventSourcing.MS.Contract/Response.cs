namespace MyEventSourcing.MS.Contract;

public record Response<T>(T? Data, string? Error = null);