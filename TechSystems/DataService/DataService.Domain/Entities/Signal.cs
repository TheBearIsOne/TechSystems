using System.Text.Json;

namespace DataService.Domain.Entities;

public class Signal
{
    public long SignalId { get; set; }
    public string SignalType { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public long EntityId { get; set; }
    public JsonDocument SignalData { get; set; } = JsonDocument.Parse("{}");
    public int? Priority { get; set; }
    public string? Status { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
}
