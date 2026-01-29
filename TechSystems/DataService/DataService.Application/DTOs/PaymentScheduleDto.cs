namespace DataService.Application.DTOs;

public sealed record PaymentScheduleDto(
    long ScheduleId,
    long LoanId,
    int PaymentNumber,
    DateOnly DueDate,
    decimal PrincipalAmount,
    decimal InterestAmount,
    decimal TotalAmount,
    bool? IsPaid,
    DateOnly? PaidDate);
