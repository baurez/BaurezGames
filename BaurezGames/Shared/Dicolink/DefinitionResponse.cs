using System.Text.Json.Serialization;

namespace BaurezGames.Shared.Dicolink;

public record DefinitionResponse()
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("nature")]
    public string Nature { get; set; }

    [JsonPropertyName("source")]
    public string Source { get; set; }

    [JsonPropertyName("attributionText")]
    public string AttributionText { get; set; }

    [JsonPropertyName("attributionUrl")]
    public string AttributionUrl { get; set; }

    [JsonPropertyName("mot")]
    public string Mot { get; set; }

    [JsonPropertyName("definition")]
    public string Definition { get; set; }

    [JsonPropertyName("dicolinkUrl")]
    public string DicolinkUrl { get; set; }
}