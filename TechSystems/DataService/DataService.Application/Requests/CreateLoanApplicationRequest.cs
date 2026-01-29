using System.Text.Json;

namespace DataService.Application.Requests;

public sealed record CreateLoanApplicationRequest(
    long ClientId,
    int ProductId,
    decimal RequestedAmount,
    int RequestedTerm,
    decimal? ApprovedAmount,
    int? ApprovedTerm,
    decimal? InterestRate,
    string Status,
    string? RejectionReason,
    int? OfficerId,
    JsonDocument? ScoringResult);
