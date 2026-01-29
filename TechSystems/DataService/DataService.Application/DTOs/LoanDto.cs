namespace DataService.Application.DTOs;

public sealed record LoanDto(
    long LoanId,
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
