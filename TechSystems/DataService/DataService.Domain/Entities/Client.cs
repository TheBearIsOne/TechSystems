namespace DataService.Domain.Entities;

public class Client
{
    public long ClientId { get; set; }
    public string ExternalId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? PassportSeries { get; set; }
    public string? PassportNumber { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? RegistrationAddress { get; set; }
    public string? ResidentialAddress { get; set; }
    public decimal? Income { get; set; }
    public string? EmploymentStatus { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsActive { get; set; }
    public int? Score { get; set; }

    public ICollection<LoanApplication> LoanApplications { get; set; } = new List<LoanApplication>();
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
