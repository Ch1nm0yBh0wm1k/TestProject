using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ExternalApiService _externalApiService;

        // Inject the service through the constructor
        public WeatherForecastController(ExternalApiService externalApiService)
        {
            _externalApiService = externalApiService;
        }
        [HttpPost("combined-response")]
        public async Task<IActionResult> GetCombinedApiResponse()
        {
            // Call the first API (LDAP Authentication)
            var ldapResponse = await _externalApiService.CallLdapAuthApiAsync("mahmood", "1212");

            // Call the second API (Employee Info)
            var employeeInfoResponse = await _externalApiService.CallEmployeeInfoApiAsync("my", "abcd", "a@b.com");

            // Combine the responses as needed (Here, we're just returning both)
            var combinedResponse = new
            {
                LdapResponse = ldapResponse,
                EmployeeInfoResponse = employeeInfoResponse
            };

            // Return the combined result
            return Ok(combinedResponse);
        }
        
    }
}
