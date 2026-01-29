namespace DataService.Application.Requests;

public sealed record UpdatePaymentScheduleRequest(
    bool? IsPaid,
    DateOnly? PaidDate);
