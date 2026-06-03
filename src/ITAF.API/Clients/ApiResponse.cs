using System.Net;

namespace ITAF.API.Clients;

public sealed record ApiResponse<T>(HttpStatusCode StatusCode, T Body, string RawBody)
{
    public bool IsSuccess => (int)StatusCode is >= 200 and <= 299;
}

