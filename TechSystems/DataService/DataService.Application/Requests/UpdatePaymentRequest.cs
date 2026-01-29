namespace DataService.Application.Requests;

public sealed record UpdatePaymentRequest(
    decimal? PaidPrincipal,
    decimal? PaidInterest,
    string Status,
    string? PaymentMethod,
    string? TransactionId);
