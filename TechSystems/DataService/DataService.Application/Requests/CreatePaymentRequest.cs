namespace DataService.Application.Requests;

public sealed record CreatePaymentRequest(
    long LoanId,
    long? SchedulePaymentId,
    DateOnly PaymentDate,
    DateOnly DueDate,
    decimal PrincipalAmount,
    decimal InterestAmount,
    decimal? PaidPrincipal,
    decimal? PaidInterest,
    string Status,
    string? PaymentMethod,
    string? TransactionId);
