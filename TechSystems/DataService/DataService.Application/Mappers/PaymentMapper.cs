using DataService.Application.DTOs;
using DataService.Application.Requests;
using DataService.Domain.Entities;

namespace DataService.Application.Mappers;

public static class PaymentMapper
{
    public static PaymentDto ToDto(this Payment entity) => new(
        entity.PaymentId,
        entity.LoanId,
        entity.SchedulePaymentId,
        entity.PaymentDate,
        entity.DueDate,
        entity.PrincipalAmount,
        entity.InterestAmount,
        entity.TotalAmount,
        entity.PaidPrincipal,
        entity.PaidInterest,
        entity.PaidTotal,
        entity.Status,
        entity.PaymentMethod,
        entity.TransactionId);

    public static Payment ToEntity(this CreatePaymentRequest request) => new()
    {
        LoanId = request.LoanId,
        SchedulePaymentId = request.SchedulePaymentId,
        PaymentDate = request.PaymentDate,
        DueDate = request.DueDate,
        PrincipalAmount = request.PrincipalAmount,
        InterestAmount = request.InterestAmount,
        PaidPrincipal = request.PaidPrincipal,
        PaidInterest = request.PaidInterest,
        Status = request.Status,
        PaymentMethod = request.PaymentMethod,
        TransactionId = request.TransactionId,
        CreatedAt = DateTime.UtcNow
    };

    public static void ApplyUpdate(this Payment entity, UpdatePaymentRequest request)
    {
        entity.PaidPrincipal = request.PaidPrincipal;
        entity.PaidInterest = request.PaidInterest;
        entity.Status = request.Status;
        entity.PaymentMethod = request.PaymentMethod;
        entity.TransactionId = request.TransactionId;
        entity.UpdatedAt = DateTime.UtcNow;
    }
}
