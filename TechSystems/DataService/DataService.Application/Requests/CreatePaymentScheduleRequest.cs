namespace DataService.Application.Requests;

public sealed record CreatePaymentScheduleRequest(
    long LoanId,
    int PaymentNumber,
    DateOnly DueDate,
    decimal PrincipalAmount,
    decimal InterestAmount,
    decimal TotalAmount,
    bool? IsPaid,
    DateOnly? PaidDate);
