using Domain;

namespace Infrastructure;

public interface  IScreeningService
{
    public void AddScreening(Screening screening);
    public List<Screening> GetAllScreenings();
    public void UpdateScreening(Screening screening);
    public void DeleteScreening(int Id);
    public List<Screening> GetAllScreeningSortByDate();
    public List<Screening> GetAllFiveScreenings();
    public List<Screening> GetAllCountScreeningsByMovies();
}
