namespace DataService.Application.DTOs;

public sealed record ApplicationLockDto(
    string LockId,
    long ApplicationId,
    int LockedBy,
    DateTime? LockedAt,
    DateTime? ExpiresAt);
