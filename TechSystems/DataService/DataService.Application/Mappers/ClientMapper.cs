using DataService.Application.DTOs;
using DataService.Application.Requests;
using DataService.Domain.Entities;

namespace DataService.Application.Mappers;

public static class ClientMapper
{
    public static ClientDto ToDto(this Client entity) => new(
        entity.ClientId,
        entity.ExternalId,
        entity.FirstName,
        entity.LastName,
        entity.MiddleName,
        entity.BirthDate,
        entity.PassportSeries,
        entity.PassportNumber,
        entity.PhoneNumber,
        entity.Email,
        entity.RegistrationAddress,
        entity.ResidentialAddress,
        entity.Income,
        entity.EmploymentStatus,
        entity.IsActive,
        entity.Score);

    public static Client ToEntity(this CreateClientRequest request) => new()
    {
        ExternalId = request.ExternalId,
        FirstName = request.FirstName,
        LastName = request.LastName,
        MiddleName = request.MiddleName,
        BirthDate = request.BirthDate,
        PassportSeries = request.PassportSeries,
        PassportNumber = request.PassportNumber,
        PhoneNumber = request.PhoneNumber,
        Email = request.Email,
        RegistrationAddress = request.RegistrationAddress,
        ResidentialAddress = request.ResidentialAddress,
        Income = request.Income,
        EmploymentStatus = request.EmploymentStatus,
        IsActive = true
    };

    public static void ApplyUpdate(this Client entity, UpdateClientRequest request)
    {
        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.MiddleName = request.MiddleName;
        entity.PhoneNumber = request.PhoneNumber;
        entity.Email = request.Email;
        entity.RegistrationAddress = request.RegistrationAddress;
        entity.ResidentialAddress = request.ResidentialAddress;
        entity.Income = request.Income;
        entity.EmploymentStatus = request.EmploymentStatus;
        entity.IsActive = request.IsActive;
        entity.Score = request.Score;
        entity.UpdatedAt = DateTime.UtcNow;
    }
}
