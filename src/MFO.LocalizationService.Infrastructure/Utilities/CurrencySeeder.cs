using MFO.LocalizationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace MFO.LocalizationService.Infrastructure.Utilities;

public class CurrencySeeder
{
    public static async Task SeedAsync(DbContext dbContext, ILogger? logger = null)
    {
        var existingCurrencies = await dbContext.Set<Currency>().ToListAsync();
        var allCurrencies = GetAllCurrencies();

        var existingByIso = existingCurrencies.ToDictionary(c => c.IsoCode, StringComparer.OrdinalIgnoreCase);

        var toInsert = new List<Currency>();
        var updatedChanges = new Dictionary<Currency, List<string>>();

        foreach (var newCurrency in allCurrencies)
        {
            if (existingByIso.TryGetValue(newCurrency.IsoCode, out var existing))
            {
                var changes = new List<string>();

                if (!string.Equals(existing.Name, newCurrency.Name, StringComparison.Ordinal))
                {
                    changes.Add($"Name: \"{existing.Name}\" → \"{newCurrency.Name}\"");
                    existing.Name = newCurrency.Name;
                }

                if (!string.Equals(existing.Symbol, newCurrency.Symbol, StringComparison.Ordinal))
                {
                    changes.Add($"Symbol: \"{existing.Symbol}\" → \"{newCurrency.Symbol}\"");
                    existing.Symbol = newCurrency.Symbol;
                }

                if (existing.DecimalPlaces != newCurrency.DecimalPlaces)
                {
                    changes.Add($"DecimalPlaces: {existing.DecimalPlaces} → {newCurrency.DecimalPlaces}");
                    existing.DecimalPlaces = newCurrency.DecimalPlaces;
                }

                if (changes.Count > 0)
                {
                    existing.LastModifiedBy = "System";
                    existing.LastModifiedDate = DateTime.UtcNow;
                    updatedChanges.Add(existing, changes);
                }
            }
            else
            {
                toInsert.Add(newCurrency);
            }
        }

        if (toInsert.Count > 0)
            await dbContext.AddRangeAsync(toInsert);

        if (updatedChanges.Count > 0)
            dbContext.UpdateRange(updatedChanges.Keys);

        if (toInsert.Count > 0 || updatedChanges.Count > 0)
            await dbContext.SaveChangesAsync();

        // Logging section
        if (logger != null)
        {
            if (toInsert.Count > 0)
            {
                logger.LogInformation("Inserted {Count} new currencies: {Currencies}",
                    toInsert.Count, string.Join(", ", toInsert.Select(c => c.IsoCode)));
            }

            if (updatedChanges.Count > 0)
            {
                foreach (var (currency, changes) in updatedChanges)
                {
                    logger.LogInformation("Updated {Iso}: {Changes}",
                        currency.IsoCode,
                        string.Join("; ", changes));
                }
            }

            if (toInsert.Count == 0 && updatedChanges.Count == 0)
            {
                logger.LogInformation("Currency table is already up-to-date.");
            }
        }
        else
        {
            Console.WriteLine($"[CurrencySeeder] Inserted: {toInsert.Count}, Updated: {updatedChanges.Count}");
        }
    }


    public static List<Currency> GetAllCurrencies()
    {
        var currencies = new List<Currency>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
        {
            try
            {
                var region = new RegionInfo(culture.LCID);

                if (seen.Contains(region.ISOCurrencySymbol))
                    continue;

                seen.Add(region.ISOCurrencySymbol);

                currencies.Add(new Currency
                {
                    CurrencyId = Guid.NewGuid(),
                    IsoCode = region.ISOCurrencySymbol,
                    Name = region.CurrencyEnglishName,
                    Symbol = region.CurrencySymbol,
                    DecimalPlaces = GetDecimalPlaces(region.ISOCurrencySymbol),
                    CreatedBy = "System",
                    CreatedDate = DateTime.UtcNow
                });
            }
            catch
            {
                // Ignore invalid or neutral cultures
            }
        }

        return currencies;
    }

    private static int GetDecimalPlaces(string isoCode)
    {
        return isoCode switch
        {
            "JPY" or "KRW" or "CLP" or "VND" or "ISK" => 0,
            _ => 2
        };
    }
}
