using DataService.Application.DTOs;
using DataService.Application.Requests;
using DataService.Domain.Entities;

namespace DataService.Application.Mappers;

public static class PaymentScheduleMapper
{
    public static PaymentScheduleDto ToDto(this PaymentSchedule entity) => new(
        entity.ScheduleId,
        entity.LoanId,
        entity.PaymentNumber,
        entity.DueDate,
        entity.PrincipalAmount,
        entity.InterestAmount,
        entity.TotalAmount,
        entity.IsPaid,
        entity.PaidDate);

    public static PaymentSchedule ToEntity(this CreatePaymentScheduleRequest request) => new()
    {
        LoanId = request.LoanId,
        PaymentNumber = request.PaymentNumber,
        DueDate = request.DueDate,
        PrincipalAmount = request.PrincipalAmount,
        InterestAmount = request.InterestAmount,
        TotalAmount = request.TotalAmount,
        IsPaid = request.IsPaid,
        PaidDate = request.PaidDate
    };

    public static void ApplyUpdate(this PaymentSchedule entity, UpdatePaymentScheduleRequest request)
    {
        entity.IsPaid = request.IsPaid;
        entity.PaidDate = request.PaidDate;
    }
}
