using System.Text.Json;

namespace DataService.Application.Requests;

public sealed record UpdateLoanApplicationRequest(
    decimal? ApprovedAmount,
    int? ApprovedTerm,
    decimal? InterestRate,
    string Status,
    string? RejectionReason,
    int? OfficerId,
    JsonDocument? ScoringResult);
