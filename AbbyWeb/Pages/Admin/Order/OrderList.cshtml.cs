using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace AbbyWeb.Pages.Admin.Order
{
  [BindProperties]
  public class OrderListModel : PageModel
  {
    private readonly IHttpClientFactory _httpClientFactory;
    public IEnumerable<OrderHeader> resData;
    private readonly ILogger<OrderListModel> _logger;
    public OrderListModel(IHttpClientFactory httpClientFactory, ILogger<OrderListModel> logger)
    {
      _httpClientFactory = httpClientFactory;
      _logger = logger;
    }
    public async Task OnGet()
    {
      try
      {
        var apiUrl = "https://localhost:44301/api/Order";
        var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "https://localhost:44301/api/Order")
        {
          Headers =
            {
                { HeaderNames.Accept, "application/vnd.github.v3+json" },
                { HeaderNames.UserAgent, "HttpRequestsSample" }
            }
        };

        var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        _logger.LogInformation("Outer - {0}", httpResponseMessage.IsSuccessStatusCode);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
          var content = await httpResponseMessage.Content.ReadAsStringAsync();
          _logger.LogInformation("Test - {0}", content);
          var obj = JObject.Parse(content);
          _logger.LogInformation("Obj - {0}", obj["data"]);
          var arr = obj["data"];
          resData = arr?.ToObject<IEnumerable<OrderHeader>>(); ;
        }
			} catch(Exception ex)
      {

      }
    }
  }
}
