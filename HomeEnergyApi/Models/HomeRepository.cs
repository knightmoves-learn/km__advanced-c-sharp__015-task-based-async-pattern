namespace HomeEnergyApi.Models
{
    public class HomeRepository: IReadRepository<int, Home>, IWriteRepository<int, Home>
    {
        public List<Home> HomesList;

        public HomeRepository()
        {
            HomesList = new List<Home>();
        }

        public Home Save(Home home)
        {
            HomesList.Add(home);
            return home;
        }

        public Home Update(int id, Home home)
        {
            HomesList[id] = home;
            return home;
        }

        public List<Home> FindAll()
        {
            return HomesList;
        }

        public Home FindById(int id)
        {
            return HomesList[id];
        }

        public Home RemoveById(int id)
        {
            var home = HomesList[id];
            HomesList.Remove(home);
            return home;
        }

        public int Count()
        {
            return HomesList.Count();
        }
    }
}