using System.Collections.Generic;
using System.Threading.Tasks;
using avviewer.DataObjects;
using avviewer.Providers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace avviewer.Controllers
{
  public class SymbolsController : Controller
  {
    private readonly ITimeSeriesProvider _timeSeriesProvider;

    public SymbolsController(ITimeSeriesProvider timeSeriesProvider)
    {
      _timeSeriesProvider = timeSeriesProvider;
    }

    [HttpGet]
    public async Task<IEnumerable<Symbol>> Index(string symbols)
    {
      return await _timeSeriesProvider.GetSymbols(symbols.Split(","));
    }
  }
}