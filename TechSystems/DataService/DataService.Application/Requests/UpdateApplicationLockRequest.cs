namespace DataService.Application.Requests;

public sealed record UpdateApplicationLockRequest(
    int LockedBy,
    DateTime? LockedAt,
    DateTime? ExpiresAt);
