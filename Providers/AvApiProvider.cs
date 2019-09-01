using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using avviewer.DataObjects;
using avviewer.Providers.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace avviewer.Providers
{
  public class AvApiProvider : IAvApiProvider
  {
    private readonly string _key;
    private readonly string _baseUrl;

    public AvApiProvider(IConfiguration configuration)
    {
      var key = configuration["apiKey"];
      var baseUrl = configuration["AvApiBaseUrl"];

      if (string.IsNullOrEmpty(key)) throw new System.ArgumentException("Not pass api apiKey in command line argument");
      if (string.IsNullOrEmpty(baseUrl)) throw new System.ArgumentException("AvApiBaseUrl is not set in configuration file");

      _key = key;
      _baseUrl = baseUrl;
    }

    public async Task<Symbol> GetSymbolFromApi(string symbolName)
    {
      JObject jobj = null;
      try
      {
        var webRequest = System.Net.WebRequest.Create(new Uri($"{_baseUrl}query?function=TIME_SERIES_DAILY&symbol={symbolName}&apikey={_key}"));
        webRequest.Method = "GET";
        webRequest.ContentType = "application/json";

        var wr = await webRequest.GetResponseAsync();
        var receiveStream = wr.GetResponseStream();
        var reader = new System.IO.StreamReader(receiveStream);

        string resp = await reader.ReadToEndAsync();

        jobj = Newtonsoft.Json.Linq.JObject.Parse(resp);

        var firstTimeSeries = jobj["Time Series (Daily)"].First().Children();

        return new Symbol
        {
          Name = jobj["Meta Data"]["2. Symbol"].ToString(),
          Change = Round(ParseNumberFromString(firstTimeSeries["1. open"].First().ToString()) - ParseNumberFromString(firstTimeSeries["4. close"].First().ToString())),
          Value = ParseNumberFromString(firstTimeSeries["4. close"].First().ToString())
        };
      }
      catch (ArgumentNullException ex)
      {
        if (jobj != null)
        {
          throw new ArgumentNullException($"Error while getting data. Maybe we have api limitations?. Api response: [{jobj.ToString()}].", ex);
        }

        throw;
      }

    }

    private static decimal ParseNumberFromString(string value)
    {
      return Round(decimal.Parse(value.Replace('.', ',')));
    }

    private static decimal Round(decimal value)
    {
      return Math.Round(value, 3);
    }
  }
}