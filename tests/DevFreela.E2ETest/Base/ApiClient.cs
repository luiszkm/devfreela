

using System.Text;
using System.Text.Json;

namespace DevFreela.UnitTests.Base;
public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _defaultSerializeOptions;
    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _defaultSerializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<(HttpResponseMessage?, TOutput?)>
        Post<TOutput>(
            string route,
            object payload
        ) where TOutput : class
    {
        var payloadAsJson = JsonSerializer
            .Serialize(payload, _defaultSerializeOptions);

        var response = await _httpClient.PostAsync(route,
                new StringContent(
                    payloadAsJson,
                    Encoding.UTF8,
                    "application/json"));
        var output = await GetOutput<TOutput>(response);
        return (response, output);

    }

    private async Task<TOutput?>
        GetOutput<TOutput>(HttpResponseMessage response)
        where TOutput : class
    {
        var responseAsString = await response.Content.ReadAsStringAsync();
        var output = JsonSerializer.Deserialize<TOutput>(responseAsString, _defaultSerializeOptions);
        return output;
    }
}
