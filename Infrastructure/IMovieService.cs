using Domain;

namespace Infrastructure;

public interface IMovieService
{
    public void AddMovie(Movie movie);
    public List<Movie> GetAllMovies();
    public void UpdateMovie(Movie movie);
    public void DeleteMovie(int Id);
}
