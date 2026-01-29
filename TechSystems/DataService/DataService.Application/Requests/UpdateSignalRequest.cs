using System.Text.Json;

namespace DataService.Application.Requests;

public sealed record UpdateSignalRequest(
    string? SignalType,
    string? EntityType,
    long? EntityId,
    JsonDocument? SignalData,
    int? Priority,
    string? Status,
    DateTime? ProcessedAt,
    DateTime? ExpiresAt);
