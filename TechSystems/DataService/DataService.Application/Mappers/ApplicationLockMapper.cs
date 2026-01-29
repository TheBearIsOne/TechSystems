using DataService.Application.DTOs;
using DataService.Application.Requests;
using DataService.Domain.Entities;

namespace DataService.Application.Mappers;

public static class ApplicationLockMapper
{
    public static ApplicationLockDto ToDto(this ApplicationLock entity) => new(
        entity.LockId,
        entity.ApplicationId,
        entity.LockedBy,
        entity.LockedAt,
        entity.ExpiresAt);

    public static ApplicationLock ToEntity(this CreateApplicationLockRequest request) => new()
    {
        LockId = request.LockId,
        ApplicationId = request.ApplicationId,
        LockedBy = request.LockedBy,
        LockedAt = request.LockedAt,
        ExpiresAt = request.ExpiresAt
    };

    public static void ApplyUpdate(this ApplicationLock entity, UpdateApplicationLockRequest request)
    {
        entity.LockedBy = request.LockedBy;
        entity.LockedAt = request.LockedAt;
        entity.ExpiresAt = request.ExpiresAt;
    }
}
