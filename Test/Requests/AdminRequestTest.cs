using minimal_api.Domain.DTOs;
using minimal_api.Domain.ModelView;
using System.Net;
using System.Text;
using System.Text.Json;
using Test.Helpers;

namespace Test;

[TestClass]
public class AdminRequestTest
{
    private static readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    [TestMethod]
    public async Task TestGetSetProperties()
    {
        var loginDTO = new LoginDTO
        {
            Email = "admin",
            Password = "admin"
        };

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "Application/json");

        var response = await Setup.client.PostAsync("/admin/login", content);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();

        var loggedAdmin = JsonSerializer.Deserialize<LoggedAdmin>(result, options: jsonSerializerOptions);

        Assert.IsNotNull(loggedAdmin?.Email ?? "");
        Assert.IsNotNull(loggedAdmin?.Role ?? "");
        Assert.IsNotNull(loggedAdmin?.Token ?? "");

        Console.WriteLine(loggedAdmin?.Token);
    }
}
