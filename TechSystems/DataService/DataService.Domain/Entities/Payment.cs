namespace DataService.Domain.Entities;

public class Payment
{
    public long PaymentId { get; set; }
    public long LoanId { get; set; }
    public long? SchedulePaymentId { get; set; }
    public DateOnly PaymentDate { get; set; }
    public DateOnly DueDate { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal InterestAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal? PaidPrincipal { get; set; }
    public decimal? PaidInterest { get; set; }
    public decimal PaidTotal { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? PaymentMethod { get; set; }
    public string? TransactionId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Loan? Loan { get; set; }
}
