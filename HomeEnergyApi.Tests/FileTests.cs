public class FileTests
{
    private static string programFilePath = @"../../../../HomeEnergyApi/Program.cs";
    private string programContent = File.ReadAllText(programFilePath);

    [Fact]
    public void DoesProgramFileAddSingletonServiceHomeRepository()
    {
        bool containsHomeRepositorySingleton = programContent.Contains("builder.Services.AddSingleton<HomeRepository>();");
        Assert.True(containsHomeRepositorySingleton,
            "HomeEnergyApi/Program.cs does not add a Singleton Service of type `HomeRepository`");
    }

    [Fact]
    public void DoesProgramFileAddSingletonServiceIReadRepositoryWithRequiredServiceProviderHomeRepository()
    {
        bool containsIReadSingleton = programContent.Contains("builder.Services.AddSingleton<IReadRepository<int, Home>>(provider => provider.GetRequiredService<HomeRepository>());");
        Assert.True(containsIReadSingleton,
            "HomeEnergyApi/Program.cs does not add a Singleton Service of type `IReadRepository` with the required Service Provider of type `HomeRepository`");
    }

    [Fact]
    public void DoesProgramFileAddSingletonServiceIWriteRepositoryWithRequiredServiceHomeProviderRepository()
    {
        bool containsIWriteSingleton = programContent.Contains("builder.Services.AddSingleton<IWriteRepository<int, Home>>(provider => provider.GetRequiredService<HomeRepository>());");
        Assert.True(containsIWriteSingleton,
            "HomeEnergyApi/Program.cs does not add a Singleton Service of type `IWriteRepository` with the required Service Provider of type `HomeRepository`");
    }

    [Fact]
    public void DoesProgramFileAddTransientForZipLocationService()
    {
        bool containsTransient = programContent.Contains("builder.Services.AddTransient<ZipCodeLocationService>();");
        Assert.True(containsTransient,
            "HomeEnergyApi/Program.cs does not add a Transient Service of type `ZipLocationService`");
    }

    [Fact]
    public void DoesProgramFileAddHttpClientForZipLocationService()
    {
        bool containsHttpClient = programContent.Contains("builder.Services.AddHttpClient<ZipCodeLocationService>();");
        Assert.True(containsHttpClient,
            "HomeEnergyApi/Program.cs does not add a HttpClient Service of type `ZipLocationService`");
    }
}
