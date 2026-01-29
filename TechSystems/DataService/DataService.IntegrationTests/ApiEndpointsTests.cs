using System.Net;
using System.Net.Http.Json;
using DataService.Application.Requests;
using Xunit;

namespace DataService.IntegrationTests;

public class ApiEndpointsTests : IClassFixture<DataServiceApiFactory>
{
    private readonly HttpClient _client;

    public ApiEndpointsTests(DataServiceApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetClients_ReturnsUnauthorized_WhenMissingAuth()
    {
        var response = await _client.GetAsync("/api/clients");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateClient_ReturnsCreated_WhenAuthorized()
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, "/api/clients");
        request.Headers.Add("Authorization", "Test");
        request.Content = JsonContent.Create(new CreateClientRequest(
            ExternalId: "ext-1",
            FirstName: "Test",
            LastName: "User",
            MiddleName: null,
            BirthDate: DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-20)),
            PassportSeries: null,
            PassportNumber: null,
            PhoneNumber: "+100000000",
            Email: "test@example.com",
            RegistrationAddress: null,
            ResidentialAddress: null,
            Income: 1000m,
            EmploymentStatus: "Employed"));

        var response = await _client.SendAsync(request);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}
