using Domain;

namespace Infrastructure;

public interface ITheaterService
{
    public void AddTheater(Theater theater);
    public List<Theater> GetAllTheaters();
    public void UpdateTheater(Theater theater);
    public void DeleteTheater(int Id);
}
