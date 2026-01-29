namespace DataService.Application.Requests;

public sealed record UpdateClientRequest(
    string FirstName,
    string LastName,
    string? MiddleName,
    string PhoneNumber,
    string? Email,
    string? RegistrationAddress,
    string? ResidentialAddress,
    decimal? Income,
    string? EmploymentStatus,
    bool? IsActive,
    int? Score);
