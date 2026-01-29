using System.Text.Json;

namespace DataService.Domain.Entities;

public class LoanApplication
{
    public long ApplicationId { get; set; }
    public long ClientId { get; set; }
    public int ProductId { get; set; }
    public decimal RequestedAmount { get; set; }
    public int RequestedTerm { get; set; }
    public decimal? ApprovedAmount { get; set; }
    public int? ApprovedTerm { get; set; }
    public decimal? InterestRate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? RejectionReason { get; set; }
    public int? OfficerId { get; set; }
    public JsonDocument? ScoringResult { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public DateTime? ExpiredAt { get; set; }

    public Client? Client { get; set; }
    public Loan? Loan { get; set; }
}
