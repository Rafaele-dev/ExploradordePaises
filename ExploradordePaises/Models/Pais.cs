using System.Linq;

namespace ExploradordePaises.Models;

public class Pais
{
    public Name? Name { get; set; }
    public string[]? Capital { get; set; }
    public string? Region { get; set; }
    public Dictionary<string, Moeda>? Currencies { get; set; }
    public Flags? Flags { get; set; }

    // Propriedades para binding no XAML (evita usar First() ou [0] no XAML)
    public string CapitalDisplay => (Capital != null && Capital.Length > 0) ? Capital[0] : "-";
    public string CurrencyDisplay =>
        (Currencies != null && Currencies.Count > 0)
            ? string.Join(", ", Currencies.Values.Where(m => !string.IsNullOrWhiteSpace(m?.Name)).Select(m => m!.Name))
            : "-";
}

public class Name
{
    public string? Common { get; set; }
    public string? Official { get; set; }
}

public class Moeda
{
    public string? Name { get; set; }
    public string? Symbol { get; set; }
}

public class Flags
{
    public string? Png { get; set; }
    public string? Svg { get; set; }
}
