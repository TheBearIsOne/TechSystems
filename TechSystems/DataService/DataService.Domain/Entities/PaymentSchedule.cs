namespace DataService.Domain.Entities;

public class PaymentSchedule
{
    public long ScheduleId { get; set; }
    public long LoanId { get; set; }
    public int PaymentNumber { get; set; }
    public DateOnly DueDate { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal InterestAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public bool? IsPaid { get; set; }
    public DateOnly? PaidDate { get; set; }

    public Loan? Loan { get; set; }
}
