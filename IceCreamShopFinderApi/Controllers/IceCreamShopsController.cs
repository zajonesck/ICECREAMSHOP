using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class IceCreamShopsController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly string _googlePlacesApiKey = "AIzaSyC9n9tGYtXAaZjLb-wIhWWdTcKjC58jFvc"; 

    public IceCreamShopsController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetIceCreamShops()
    {
        var location = "30.2672,-97.7431"; // Latitude and longitude for Austin, TX
        var radius = 5000; // 5 km radius
        var keyword = "ice cream";
        var url = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={location}&radius={radius}&keyword={keyword}&key={_googlePlacesApiKey}";

        var response = await _httpClient.GetFromJsonAsync<GooglePlacesResponse>(url);

        if (response == null || response.Results == null)
        {
            return NotFound();
        }

        return Ok(response.Results);
    }

    public class GooglePlacesResponse
    {
        public Result[]? Results { get; set; }
    }

    public class Result
    {
        public string? Name { get; set; }
        public string? Vicinity { get; set; }
    }
}
