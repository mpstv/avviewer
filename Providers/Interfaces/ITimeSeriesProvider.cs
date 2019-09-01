using System.Collections.Generic;
using System.Threading.Tasks;
using avviewer.DataObjects;

namespace avviewer.Providers.Interfaces
{
  public interface ITimeSeriesProvider
  {
    Task<IEnumerable<Symbol>> GetSymbols(IEnumerable<string> symbolNames);
  }
}