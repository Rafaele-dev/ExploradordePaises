using System.Linq;
using System.Text.Json.Serialization;

namespace ExploradordePaises.Models;

public class Pais
{
    [JsonPropertyName("name")]
    public Name? Name { get; set; }

    [JsonPropertyName("capital")]
    public string[]? Capital { get; set; }

    [JsonPropertyName("region")]
    public string? Region { get; set; }

    [JsonPropertyName("subregion")]
    public string? Subregion { get; set; }

    [JsonPropertyName("population")]
    public long? Population { get; set; }

    [JsonPropertyName("area")]
    public double? Area { get; set; }

    [JsonPropertyName("currencies")]
    public Dictionary<string, Moeda>? Currencies { get; set; }

    [JsonPropertyName("languages")]
    public Dictionary<string, string>? Languages { get; set; }

    [JsonPropertyName("flags")]
    public Flags? Flags { get; set; }

    public string CapitalDisplay =>
        (Capital != null && Capital.Length > 0) ? Capital[0] : "Não informado";

    public string CurrencyDisplay =>
        (Currencies != null && Currencies.Count > 0)
            ? string.Join(", ", Currencies.Values.Where(m => !string.IsNullOrWhiteSpace(m?.Name)).Select(m => m!.Name))
            : "Não informado";

    public string LanguagesDisplay =>
        (Languages != null && Languages.Count > 0)
            ? string.Join(", ", Languages.Values.Where(lang => !string.IsNullOrWhiteSpace(lang)))
            : "Não informado";

    public string PopulationDisplay =>
        Population.HasValue ? Population.Value.ToString("N0") : "Não informado";

    public string AreaDisplay =>
        Area.HasValue ? $"{Area.Value:N0} km²" : "Não informado";

    public string SubregionDisplay =>
        !string.IsNullOrWhiteSpace(Subregion) ? Subregion : "Não informado";
}

public class Name
{
    [JsonPropertyName("common")]
    public string? Common { get; set; }

    [JsonPropertyName("official")]
    public string? Official { get; set; }
}

public class Moeda
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }
}

public class Flags
{
    [JsonPropertyName("png")]
    public string? Png { get; set; }

    [JsonPropertyName("svg")]
    public string? Svg { get; set; }
}