using System.Text.Json;

namespace DataService.Application.DTOs;

public sealed record LoanApplicationDto(
    long ApplicationId,
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
    JsonDocument? ScoringResult,
    DateTime? SubmittedAt,
    DateTime? ReviewedAt,
    DateTime? ExpiredAt);
