using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ExternalApiService
{
    private readonly HttpClient _httpClient;

    public ExternalApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Method to call the first API (LDAP Authentication)
    public async Task<string> CallLdapAuthApiAsync(string username, string password)
    {
        var requestUrl = "https://jsonplaceholder.typicode.com/posts";
        requestUrl = "https://103.125.254.109:8088/api/User/GetUserDetails";

        // Prepare the request body as JSON
        //var requestData = new
        //{
        //    username = username,
        //    password = password
        //};
        var requestData = new
        {
            user = username,
            pwd = password
        };

        var jsonContent = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
        jsonContent = null;
        // Send POST request
        var response = await _httpClient.PostAsync(requestUrl, jsonContent);

        // Ensure the response is successful, otherwise throw an error
        response.EnsureSuccessStatusCode();

        // Get the response content as a string
        var responseData = await response.Content.ReadAsStringAsync();

        return responseData;
    }

    // Method to call the second API (Employee Info)
    public async Task<string> CallEmployeeInfoApiAsync(string username, string password, string email)
    {
        var requestUrl = "http://192.168.1.96/apigateway/api/employeeinfo";

        // Prepare the request body as JSON
        var requestData = new
        {
            username = username,
            password = password,
            email = email
        };

        var jsonContent = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");

        // Send POST request
        var response = await _httpClient.PostAsync(requestUrl, jsonContent);

        // Ensure the response is successful, otherwise throw an error
        response.EnsureSuccessStatusCode();

        // Get the response content as a string
        var responseData = await response.Content.ReadAsStringAsync();

        return responseData;
    }
}
