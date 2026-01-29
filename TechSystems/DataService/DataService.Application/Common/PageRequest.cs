namespace DataService.Application.Common;

public sealed record PageRequest(int PageNumber = 1, int PageSize = 20);
