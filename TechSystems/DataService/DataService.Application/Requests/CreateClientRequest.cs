namespace DataService.Application.Requests;

public sealed record CreateClientRequest(
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
    string? EmploymentStatus);
