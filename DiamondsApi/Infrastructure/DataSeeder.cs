using CsvHelper;
using CsvHelper.Configuration;
using DiamondsApi.Models;
using System.Globalization;

namespace DiamondsApi.Infrastructure;

public class DataSeeder
{
    private readonly DiamondDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public DataSeeder(DiamondDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task SeedAsync()
    {
        if (!_context.Diamonds.Any())
        {
            var csvPath = Path.Combine(_environment.ContentRootPath, "Data", "diamonds.csv");
            
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
                HeaderValidated = null,
                MissingFieldFound = null
            };

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, config);

            // Read the first row to get the headers
            await csv.ReadAsync();
            csv.ReadHeader();

            var records = new List<Diamond>();
            while (await csv.ReadAsync())
            {
                try
                {
                    var record = new Diamond
                    {
                        Carat = csv.GetField<decimal>("carat"),
                        Cut = csv.GetField<string>("cut"),
                        Color = csv.GetField<string>("color"),
                        Clarity = csv.GetField<string>("clarity"),
                        Depth = csv.GetField<decimal>("depth"),
                        Table = csv.GetField<decimal>("table"),
                        Price = csv.GetField<decimal>("price"),
                        X = csv.GetField<decimal>("x"),
                        Y = csv.GetField<decimal>("y"),
                        Z = csv.GetField<decimal>("z")
                    };
                    records.Add(record);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing row: {ex.Message}");
                    continue;
                }
            }

            await _context.Diamonds.AddRangeAsync(records);
            await _context.SaveChangesAsync();
        }
    }
} 