using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using avviewer.DataObjects;
using avviewer.Providers.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace avviewer.Providers
{
  public class TimeSeriesProvider : ITimeSeriesProvider
  {
    private readonly IMemoryCache _cache;
    private readonly IAvApiProvider _apiProvider;

    public TimeSeriesProvider(IMemoryCache cache, IAvApiProvider apiProvider)
    {
      _cache = cache;
      this._apiProvider = apiProvider;
    }

    public async Task<IEnumerable<Symbol>> GetSymbols(IEnumerable<string> symbolNames)
    {
      var result = new List<Symbol>();
      
      foreach (var symbolName in symbolNames)
      {
        if (_cache.TryGetValue(symbolName, out Symbol symbol))
        {
          result.Add(symbol);
        }
        else
        {
          var newSymbol = await _apiProvider.GetSymbolFromApi(symbolName);

          _cache.Set(symbolName, newSymbol, TimeSpan.FromHours(1));

          result.Add(newSymbol);
        }
      }

      return result;
    }
  }
}