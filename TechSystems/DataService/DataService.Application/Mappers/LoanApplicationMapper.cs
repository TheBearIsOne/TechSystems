using DataService.Application.DTOs;
using DataService.Application.Requests;
using DataService.Domain.Entities;

namespace DataService.Application.Mappers;

public static class LoanApplicationMapper
{
    public static LoanApplicationDto ToDto(this LoanApplication entity) => new(
        entity.ApplicationId,
        entity.ClientId,
        entity.ProductId,
        entity.RequestedAmount,
        entity.RequestedTerm,
        entity.ApprovedAmount,
        entity.ApprovedTerm,
        entity.InterestRate,
        entity.Status,
        entity.RejectionReason,
        entity.OfficerId,
        entity.ScoringResult,
        entity.SubmittedAt,
        entity.ReviewedAt,
        entity.ExpiredAt);

    public static LoanApplication ToEntity(this CreateLoanApplicationRequest request) => new()
    {
        ClientId = request.ClientId,
        ProductId = request.ProductId,
        RequestedAmount = request.RequestedAmount,
        RequestedTerm = request.RequestedTerm,
        ApprovedAmount = request.ApprovedAmount,
        ApprovedTerm = request.ApprovedTerm,
        InterestRate = request.InterestRate,
        Status = request.Status,
        RejectionReason = request.RejectionReason,
        OfficerId = request.OfficerId,
        ScoringResult = request.ScoringResult,
        CreatedAt = DateTime.UtcNow
    };

    public static void ApplyUpdate(this LoanApplication entity, UpdateLoanApplicationRequest request)
    {
        entity.ApprovedAmount = request.ApprovedAmount;
        entity.ApprovedTerm = request.ApprovedTerm;
        entity.InterestRate = request.InterestRate;
        entity.Status = request.Status;
        entity.RejectionReason = request.RejectionReason;
        entity.OfficerId = request.OfficerId;
        entity.ScoringResult = request.ScoringResult;
        entity.UpdatedAt = DateTime.UtcNow;
    }
}
