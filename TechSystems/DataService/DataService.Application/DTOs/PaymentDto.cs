namespace DataService.Application.DTOs;

public sealed record PaymentDto(
    long PaymentId,
    long LoanId,
    long? SchedulePaymentId,
    DateOnly PaymentDate,
    DateOnly DueDate,
    decimal PrincipalAmount,
    decimal InterestAmount,
    decimal TotalAmount,
    decimal? PaidPrincipal,
    decimal? PaidInterest,
    decimal PaidTotal,
    string Status,
    string? PaymentMethod,
    string? TransactionId);
