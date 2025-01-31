using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using HomeEnergyApi.Models;

[TestCaseOrderer("HomeEnergyApi.Tests.Extensions.PriorityOrderer", "HomeEnergyApi.Tests")]
public class HomesControllersTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private string testHome = JsonSerializer.Serialize(new Home("Test", "123 Test St.", "Test City", 123));
    private string putTestHome = JsonSerializer.Serialize(new Home("Testy", "456 Assert St.", "Unitville", 456));

    public HomesControllersTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory, TestPriority(1)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturnsSuccessfulHTTPResponseCodeOnGETHomes(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        Assert.True(response.IsSuccessStatusCode,
            $"HomeEnergyApi did not return successful HTTP Response Code on GET request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");
    }

    [Theory, TestPriority(2)]
    [InlineData("/admin/Homes")]
    public async Task HomeEnergyApiReturns201CreatedHTTPResponseOnAddingValidHomeThroughPOST(string url)
    {
        var client = _factory.CreateClient();

        HttpRequestMessage sendRequest = new HttpRequestMessage(HttpMethod.Post, url);
        sendRequest.Content = new StringContent(testHome,
                                                Encoding.UTF8,
                                                "application/json");

        var response = await client.SendAsync(sendRequest);
        Assert.True((int)response.StatusCode == 201,
            $"HomeEnergyApi did not return \"201: Created\" HTTP Response Code on POST request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");

        string responseContent = await response.Content.ReadAsStringAsync();
        Assert.True(responseContent.ToLower() == testHome.ToLower(),
            $"HomeEnergyApi did not return the home being added as a response from the POST request at {url}; \n Expected : {testHome.ToLower()} \n Received : {responseContent.ToLower()} \n");
        Assert.True(response?.Headers?.Location?.ToString().Contains("/Homes/0"),
            $"Home EnergyApi did not return correct location header on POST at {url}\n Expected: /Homes/0\n Received{response?.Headers?.Location}");
    }

    [Theory, TestPriority(3)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturnsCanGETAllAndByIdAfterAddingThroughPOST(string url)
    {
        var client = _factory.CreateClient();

        var getResponse = await client.GetAsync(url);
        var getByIdResponse = await client.GetAsync(url + "/0");

        string getResponseContent = await getResponse.Content.ReadAsStringAsync();
        string getByIdResponseContent = await getByIdResponse.Content.ReadAsStringAsync();

        Assert.True(getResponseContent.ToLower() == $"[{testHome}]".ToLower(),
            $"HomeEnergyApi did not return the added home through GET at {url};\n Expected : {$"[{testHome}]".ToLower()}\n Received : {getResponseContent.ToLower()} \n");
        Assert.True(getByIdResponseContent.ToLower() == testHome.ToLower(),
            $"HomeEnergyApi did not return the added home through GET at {url + "/1"};\n Expected : {testHome.ToLower()}\n Received : {getByIdResponseContent.ToLower()} \n");
    }

    [Theory, TestPriority(4)]
    [InlineData("/Homes/99")]
    public async Task HomeEnergyApiReturnsNotFoundWhenAttemptingToFindByIdOnNonExistentHome(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        Assert.True((int)response.StatusCode == 404,
            $"HomeEnergyApi did not return \"404: Not Found\" HTTP Response Code on GET request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");
    }

    [Theory, TestPriority(5)]
    [InlineData("/admin/Homes/0")]
    public async Task HomeEnergyApiCanUpdateHomesThroughPUT(string url)
    {
        var client = _factory.CreateClient();

        HttpRequestMessage sendRequest = new HttpRequestMessage(HttpMethod.Put, url);
        sendRequest.Content = new StringContent(putTestHome,
                                                Encoding.UTF8,
                                                "application/json");

        var response = await client.SendAsync(sendRequest);
        Assert.True(response.IsSuccessStatusCode,
            $"HomeEnergyApi did not return successful HTTP Response Code on PUT request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");

        string responseContent = await response.Content.ReadAsStringAsync();
        Assert.True(responseContent.ToLower() == putTestHome.ToLower(),
            $"HomeEnergyApi did not return the updated home through PUT at {url};\n Expected : {putTestHome.ToLower()}\n Received : {responseContent.ToLower()} \n");
    }

    [Theory, TestPriority(6)]
    [InlineData("/admin/Homes/99")]
    public async Task HomeEnergyApiRespondsWithNotFoundWhenTryingToUpdateNonExistentHome(string url)
    {
        var client = _factory.CreateClient();

        HttpRequestMessage sendRequest = new HttpRequestMessage(HttpMethod.Put, url);
        sendRequest.Content = new StringContent(putTestHome,
                                                Encoding.UTF8,
                                                "application/json");

        var response = await client.SendAsync(sendRequest);
        Assert.True((int)response.StatusCode == 404,
             $"HomeEnergyApi did not return \"404: Not Found\" HTTP Response Code on PUT request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");
    }

    [Theory, TestPriority(7)]
    [InlineData("admin/Homes/0")]
    public async Task HomeEnergyApiCanRemoveHomesThroughDELETE(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.DeleteAsync(url);
        Assert.True(response.IsSuccessStatusCode, $"HomeEnergyApi did not return successful HTTP Response Code on DELETE request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");

        string responseContent = await response.Content.ReadAsStringAsync();
        Assert.True(responseContent.ToLower() == putTestHome.ToLower(), $"HomeEnergyApi did not return the deleted home through DELETE at {url};\n Expected : {putTestHome.ToLower()}\n Received : {responseContent.ToLower()} \n");
    }

    [Theory, TestPriority(8)]
    [InlineData("admin/Homes/0")]
    public async Task HomeEnergyApiRespondsWithNotFoundWhenTryingToDeleteNonExistentHome(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.DeleteAsync(url);
        Assert.True((int)response.StatusCode == 404,
             $"HomeEnergyApi did not return \"404: Not Found\" HTTP Response Code on DELETE request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");

    }

    [Theory, TestPriority(9)]
    [InlineData("admin/Homes/Location/50313")]
    public async Task ZipLocationServiceRespondsWith200CodeAndPlaceWhenGivenValidZipCode(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.PostAsync(url, null);

        Assert.True((int)response.StatusCode == 200,
             $"HomeEnergyApi did not return \"200: OK\" HTTP Response Code on POST request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");

        string responseContent = await response.Content.ReadAsStringAsync();
        bool hasPlace = responseContent.Contains("\"place name\":\"Des Moines\"");
        bool hasState = responseContent.Contains("\"state\":\"Iowa\"");


        Assert.True(hasPlace && hasState,
            $"HomeEnergyApi did not return the expected `place name` and `state` from POST request at {url}\nExpected: `place name` of `Des Moines` and `state` of `Iowa`\nReceived: {responseContent}");
    }
}
