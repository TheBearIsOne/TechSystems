namespace DataService.Application.Requests;

public sealed record CreateLoanRequest(
    long? ApplicationId,
    long ClientId,
    string ContractNumber,
    decimal PrincipalAmount,
    decimal InterestRate,
    int TermMonths,
    DateOnly StartDate,
    DateOnly EndDate,
    DateOnly? NextPaymentDate,
    decimal CurrentPrincipal,
    decimal? TotalPaidPrincipal,
    decimal? TotalPaidInterest,
    string Status,
    int? OverdueDays);
