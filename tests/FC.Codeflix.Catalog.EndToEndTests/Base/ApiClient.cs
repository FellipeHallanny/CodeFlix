using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FC.Codeflix.Catalog.EndToEndTests.Base;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(HttpResponseMessage?, TOutput?)> Post<TOutput>(string route, object payload) where TOutput : class
    {
        var reponse = await _httpClient.PostAsync(
            route,
            new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            )
        );

        TOutput? output = await GetOutput<TOutput>(reponse);

        return (reponse, output);
    }

    public async Task<(HttpResponseMessage?, TOutput?)> Get<TOutput>(string route)
        where TOutput : class
    {
        var reponse = await _httpClient.GetAsync(route);

        TOutput? output = await GetOutput<TOutput>(reponse);

        return (reponse, output);
    }

    public async Task<(HttpResponseMessage?, TOutput?)> Delete<TOutput>(string route)
        where TOutput : class
    {
        var reponse = await _httpClient.DeleteAsync(route);
        TOutput? output = await GetOutput<TOutput>(reponse);

        return (reponse, output);
    }

    public async Task<(HttpResponseMessage?, TOutput?)> Put<TOutput>(string route, object payload) where TOutput : class
    {
        var reponse = await _httpClient.PutAsync(
            route,
            new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            )
        );

        TOutput? output = await GetOutput<TOutput>(reponse);

        return (reponse, output);
    }

    private async Task<TOutput?> GetOutput<TOutput>(HttpResponseMessage reponse) where TOutput : class
    {
        var outputString = await reponse.Content.ReadAsStringAsync();
        TOutput? output = null;
        if (!string.IsNullOrWhiteSpace(outputString))
            output = JsonSerializer.Deserialize<TOutput>(outputString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return output;
    }
}