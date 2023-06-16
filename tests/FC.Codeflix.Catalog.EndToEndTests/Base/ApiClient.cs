using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
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
        
        var outputString = await reponse.Content.ReadAsStringAsync();
        TOutput? output = null;
            if (!string.IsNullOrWhiteSpace(outputString))
                output = JsonSerializer.Deserialize<TOutput>(outputString, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});

        return (reponse, output);
    }
}