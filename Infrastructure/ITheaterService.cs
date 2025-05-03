using Domain;

namespace Infrastructure;

public interface ITheaterService
{
    public void AddTheater(Theater theater);
    public List<Theater> GetAllTheaters();
    public void UpdateTheater(Theater theater);
    public void DeleteTheater(int Id);
    public List<Theater> GetAllTeatherByScreening(int countScreening);
    public List<Theater> GetAllTheaterAvgPrice();
    public List<Theater> GetAllTheaterByMovieName(string movieName);
    public List<Theater> GetAllTheaterByScreening();
}
