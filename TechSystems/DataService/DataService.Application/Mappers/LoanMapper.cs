using DataService.Application.DTOs;
using DataService.Application.Requests;
using DataService.Domain.Entities;

namespace DataService.Application.Mappers;

public static class LoanMapper
{
    public static LoanDto ToDto(this Loan entity) => new(
        entity.LoanId,
        entity.ApplicationId,
        entity.ClientId,
        entity.ContractNumber,
        entity.PrincipalAmount,
        entity.InterestRate,
        entity.TermMonths,
        entity.StartDate,
        entity.EndDate,
        entity.NextPaymentDate,
        entity.CurrentPrincipal,
        entity.TotalPaidPrincipal,
        entity.TotalPaidInterest,
        entity.Status,
        entity.OverdueDays);

    public static Loan ToEntity(this CreateLoanRequest request) => new()
    {
        ApplicationId = request.ApplicationId,
        ClientId = request.ClientId,
        ContractNumber = request.ContractNumber,
        PrincipalAmount = request.PrincipalAmount,
        InterestRate = request.InterestRate,
        TermMonths = request.TermMonths,
        StartDate = request.StartDate,
        EndDate = request.EndDate,
        NextPaymentDate = request.NextPaymentDate,
        CurrentPrincipal = request.CurrentPrincipal,
        TotalPaidPrincipal = request.TotalPaidPrincipal,
        TotalPaidInterest = request.TotalPaidInterest,
        Status = request.Status,
        OverdueDays = request.OverdueDays,
        CreatedAt = DateTime.UtcNow
    };

    public static void ApplyUpdate(this Loan entity, UpdateLoanRequest request)
    {
        entity.CurrentPrincipal = request.CurrentPrincipal;
        entity.TotalPaidPrincipal = request.TotalPaidPrincipal;
        entity.TotalPaidInterest = request.TotalPaidInterest;
        entity.Status = request.Status;
        entity.OverdueDays = request.OverdueDays;
        entity.NextPaymentDate = request.NextPaymentDate;
        entity.UpdatedAt = DateTime.UtcNow;
    }
}
