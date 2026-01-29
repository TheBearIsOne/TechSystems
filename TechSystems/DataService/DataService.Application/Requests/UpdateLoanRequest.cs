namespace DataService.Application.Requests;

public sealed record UpdateLoanRequest(
    decimal CurrentPrincipal,
    decimal? TotalPaidPrincipal,
    decimal? TotalPaidInterest,
    string Status,
    int? OverdueDays,
    DateOnly? NextPaymentDate);
