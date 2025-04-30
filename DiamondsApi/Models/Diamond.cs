namespace DiamondsApi.Models;

public class Diamond
{
    public int Id { get; set; }
    public decimal Carat { get; set; }
    public string Cut { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Clarity { get; set; } = string.Empty;
    public decimal Depth { get; set; }
    public decimal Table { get; set; }
    public decimal Price { get; set; }
    public decimal X { get; set; }
    public decimal Y { get; set; }
    public decimal Z { get; set; }
} 