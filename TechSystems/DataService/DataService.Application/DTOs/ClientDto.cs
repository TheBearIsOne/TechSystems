namespace DataService.Application.DTOs;

public sealed record ClientDto(
    long ClientId,
    string ExternalId,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly BirthDate,
    string? PassportSeries,
    string? PassportNumber,
    string PhoneNumber,
    string? Email,
    string? RegistrationAddress,
    string? ResidentialAddress,
    decimal? Income,
    string? EmploymentStatus,
    bool? IsActive,
    int? Score);
