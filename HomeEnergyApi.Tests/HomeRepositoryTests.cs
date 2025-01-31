using HomeEnergyApi.Models;

[TestCaseOrderer("HomeEnergyApi.Tests.Extensions.PriorityOrderer", "HomeEnergyApi.Tests")]
public class HomeRepositoryTests
{
    private Home testHome = new Home("Test", "123 Test St.", "Test City", 123);
    private Home testHome2 = new Home("Testy", "456 Assert St.", "Unitville", 456);

    [Fact, TestPriority(1)]
    public void HomeRepositoryCanCreateANewHomeRepositoryAndPublicListHomesListOfHomes()
    {
        var homeRepository = new HomeRepository();
        Assert.True(!homeRepository.HomesList.Any(), 
            "HomeRepository failed to instantiate with an empty public list of Homes \"homesList\"");
    }

    [Fact, TestPriority(2)]
    public void HomeRepositoryCanSaveANewHome()
    {
        var homeRepository = new HomeRepository();
        homeRepository.Save(testHome);
        Assert.True(homeRepository.HomesList[0] == testHome, 
            $"HomeRepository was unable to successfully save a new home.");
    }


    [Fact, TestPriority(3)]
    public void HomeRepositoryCanUpdateAHome()
    {
        var homeRepository = new HomeRepository();

        homeRepository.Save(testHome);
        homeRepository.Update(0, testHome2);

        Assert.True(homeRepository.HomesList[0] == testHome2, 
            "HomeRepository was unable to successfully update a home at a given Id");
    }

    [Fact, TestPriority(4)]
    public void HomeRepositoryCanFindAll()
    {
        var homeRepository = new HomeRepository();

        homeRepository.Save(testHome);
        homeRepository.Save(testHome2);

        Assert.True(homeRepository.HomesList[0] == testHome && homeRepository.HomesList[1] == testHome2, 
            "HomeRepository was unable to successfully find all homes");
    }

    [Fact, TestPriority(5)]
    public void HomeRepositoryCanFindById()
    {
        var homeRepository = new HomeRepository();

        homeRepository.Save(testHome);
        homeRepository.Save(testHome2);

        homeRepository.FindById(1);

        Assert.True(homeRepository.HomesList[1] == testHome2, 
            "HomeRepository was unable to successfully find a home with a given Id");
    }

    [Fact, TestPriority(6)]
    public void HomeRepositoryCanRemoveById()
    {
        var homeRepository = new HomeRepository();

        homeRepository.Save(testHome);

        homeRepository.RemoveById(0);

        Assert.True(!homeRepository.HomesList.Any(), 
            "HomeRepository was unable to successfully remove a home at a given Id");
    }

    [Fact, TestPriority(7)]
    public void HomeRepositoryImplementsIReadRepositoryINTHOME()
    {
        Assert.True(typeof(IReadRepository<int, Home>).IsAssignableFrom(typeof(HomeRepository)), 
            "The class HomeRepository Does Not Implement \"IRepository<int, Home>\"");
    }

    [Fact, TestPriority(8)]
    public void HomeRepositoryImplementsIWriteRepositoryINTHOME()
    {
        Assert.True(typeof(IWriteRepository<int, Home>).IsAssignableFrom(typeof(HomeRepository)), 
            "The class HomeRepository Does Not Implement \"IRepository<int, Home>\"");
    }
}
