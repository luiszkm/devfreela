

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DevFreela.API.ApiModels.Response;
using DevFreela.Application.UseCases.Session;
using Microsoft.AspNetCore.WebUtilities;

namespace DevFreela.E2ETest.Base;
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
        if (output is null)
            return (response, null);

        return (response, output);

    }

    public async Task<(HttpResponseMessage?, TOutput?)>
        Put<TOutput>(
            string route,
            object payload
        ) where TOutput : class
    {
        var payloadAsJson = JsonSerializer
            .Serialize(payload, _defaultSerializeOptions);

        var response = await _httpClient.PutAsync(route,
            new StringContent(
                payloadAsJson,
                Encoding.UTF8,
                "application/json"));
        var output = await GetOutput<TOutput>(response);
        if (output is null)
            return (response, null);

        return (response, output);

    }

    public async Task<(HttpResponseMessage?, TOutput?)>
        Patch<TOutput>(
            string route,
            object payload
        ) where TOutput : class
    {
        var payloadAsJson = JsonSerializer
            .Serialize(payload, _defaultSerializeOptions);

        var response = await _httpClient.PatchAsync(route,
            new StringContent(
                payloadAsJson,
                Encoding.UTF8,
                "application/json"));
        var output = await GetOutput<TOutput>(response);
        if (output is null)
            return (response, null);

        return (response, output);

    }

    public async Task<(HttpResponseMessage?, TOutput?)> Get<TOutput>(
        string route,
        object? queryStringParametersObject = null
    ) where TOutput : class
    {
        var url = PrepareGetRoute(route, queryStringParametersObject);
        var response = await _httpClient.GetAsync(url);
        var output = await GetOutput<TOutput>(response);
        return (response, output);
    }
    public async Task<(HttpResponseMessage?, TOutput?)> Delete<TOutput>(
        string route
    ) where TOutput : class
    {
        var response = await _httpClient.DeleteAsync(route);
        var output = await GetOutput<TOutput>(response);
        return (response, output);
    }

    public async Task<bool> AddAuthorizationHeader(string email, string pwd)
    {
        var accessToken = await GetTokenAuthenticate(email, pwd);

        try
        {
            if (accessToken != null
                && accessToken.Data != null
                && !string.IsNullOrEmpty(accessToken.Data.Token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Data.Token);
                return true;

            }

            return false;
        }
        catch (Exception e)
        {
            throw new Exception("Algo deu errado no header", e);
        }

    }
    public void RemoveAuthorizationHeader()
    {
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    private async Task<ApiResponse<SessionOutput>> GetTokenAuthenticate(string email, string pwd)
    {
        var inputModel = new SessionInput(
            email,
            pwd);
        var payloadAsJson = JsonSerializer
            .Serialize(inputModel, _defaultSerializeOptions);

        try
        {
            var response = await _httpClient
                .PostAsync("/Session",
                    new StringContent(
                        payloadAsJson,
                        Encoding.UTF8,
                        "application/json"));
            var output = await GetOutput<ApiResponse<SessionOutput>>(response);

            return output;
        }
        catch (Exception ex)
        {
            throw new Exception("Token não gerado", ex);
        }

    }





    private async Task<TOutput?>
        GetOutput<TOutput>(HttpResponseMessage response)
        where TOutput : class
    {
        var responseAsString = await response.Content.ReadAsStringAsync();

        try
        {
            var output = JsonSerializer.Deserialize<TOutput>(responseAsString, _defaultSerializeOptions);
            return output;
        }
        catch (JsonException)
        {
            // Trate o caso onde a desserialização falhou devido a um JSON inválido
            // Por exemplo, lançando uma exceção, retornando null ou lidando de outra forma
            // Aqui estou retornando null, mas você pode fazer de acordo com sua lógica
            return null;
        }
    }


    private string PrepareGetRoute(
        string route,
        object? queryStringParametersObject
    )
    {
        if (queryStringParametersObject is null)
            return route;
        var parametersJson = JsonSerializer.Serialize(
            queryStringParametersObject,
            _defaultSerializeOptions
        );
        var parametersDictionary = Newtonsoft.Json.JsonConvert
            .DeserializeObject<Dictionary<string, string>>(parametersJson);
        return QueryHelpers.AddQueryString(route, parametersDictionary!);
    }
}
