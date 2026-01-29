using System.Text.Json;

namespace DataService.Application.Requests;

public sealed record CreateSignalRequest(
    string SignalType,
    string EntityType,
    long EntityId,
    JsonDocument SignalData,
    int? Priority,
    string? Status,
    DateTime? ExpiresAt);
