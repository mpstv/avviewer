using System.Threading.Tasks;
using avviewer.DataObjects;

namespace avviewer.Providers.Interfaces
{
  public interface IAvApiProvider
  {
    Task<Symbol> GetSymbolFromApi(string symbolName);
  }
}