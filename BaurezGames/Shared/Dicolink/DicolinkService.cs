using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace BaurezGames.Shared.Dicolink;

public class DicolinkService : IDicolinkService
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "5kH4tq73lhHGxRYt7nfziWCV3KRgBWwh";

    public DicolinkService(HttpClient httpClient)
    {
        _httpClient = httpClient;

    }

    public async Task<IEnumerable<DefinitionResponse>?> GetDefinitionAsync(string mot, int limite)
    {
        var response = await _httpClient.GetAsync($"/v1/mot/{mot}/definitions?limite={limite}&api_key={ApiKey}");

        if (response.StatusCode != HttpStatusCode.OK) return null;


        var byteArray = await response.Content.ReadAsByteArrayAsync();
        var responseString = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);


        return responseString switch
        {
            "{\"error\":\"pas de résultats\"}" => null,
            _ => JsonConvert.DeserializeObject<IEnumerable<DefinitionResponse>>(responseString)
        };
    }

}