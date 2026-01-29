using System.Text.Json;

namespace DataService.Application.DTOs;

public sealed record SignalDto(
    long SignalId,
    string SignalType,
    string EntityType,
    long EntityId,
    JsonDocument SignalData,
    int? Priority,
    string? Status,
    DateTime? ProcessedAt,
    DateTime? ExpiresAt);
