using DataService.Application.DTOs;
using DataService.Application.Requests;
using DataService.Domain.Entities;

namespace DataService.Application.Mappers;

public static class SignalMapper
{
    public static SignalDto ToDto(this Signal entity) => new(
        entity.SignalId,
        entity.SignalType,
        entity.EntityType,
        entity.EntityId,
        entity.SignalData,
        entity.Priority,
        entity.Status,
        entity.ProcessedAt,
        entity.ExpiresAt);

    public static Signal ToEntity(this CreateSignalRequest request) => new()
    {
        SignalType = request.SignalType,
        EntityType = request.EntityType,
        EntityId = request.EntityId,
        SignalData = request.SignalData,
        Priority = request.Priority,
        Status = request.Status,
        ExpiresAt = request.ExpiresAt,
        CreatedAt = DateTime.UtcNow
    };

    public static void ApplyUpdate(this Signal entity, UpdateSignalRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.SignalType))
        {
            entity.SignalType = request.SignalType;
        }

        if (!string.IsNullOrWhiteSpace(request.EntityType))
        {
            entity.EntityType = request.EntityType;
        }

        if (request.EntityId.HasValue)
        {
            entity.EntityId = request.EntityId.Value;
        }

        if (request.SignalData is not null)
        {
            entity.SignalData = request.SignalData;
        }

        if (request.Priority.HasValue)
        {
            entity.Priority = request.Priority;
        }

        if (request.Status is not null)
        {
            entity.Status = request.Status;
        }

        entity.ProcessedAt = request.ProcessedAt;
        entity.ExpiresAt = request.ExpiresAt;
    }
}
