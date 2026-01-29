namespace DataService.Domain.Entities;

public class Loan
{
    public long LoanId { get; set; }
    public long? ApplicationId { get; set; }
    public long ClientId { get; set; }
    public string ContractNumber { get; set; } = string.Empty;
    public decimal PrincipalAmount { get; set; }
    public decimal InterestRate { get; set; }
    public int TermMonths { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public DateOnly? NextPaymentDate { get; set; }
    public decimal CurrentPrincipal { get; set; }
    public decimal? TotalPaidPrincipal { get; set; }
    public decimal? TotalPaidInterest { get; set; }
    public string Status { get; set; } = string.Empty;
    public int? OverdueDays { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Client? Client { get; set; }
    public LoanApplication? Application { get; set; }
    public ICollection<PaymentSchedule> PaymentScheduleEntries { get; set; } = new List<PaymentSchedule>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
