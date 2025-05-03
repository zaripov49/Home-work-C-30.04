using Domain;
using Domain.Dtos;

namespace Infrastructure;

public interface IMovieService
{
    public void AddMovie(Movie movie);
    public List<Movie> GetAllMovies();
    public void UpdateMovie(Movie movie);
    public void DeleteMovie(int Id);
    public List<Movie> GetAllMoviesByGenre(string genre);
    public List<Movie> GetAllMovieDirector();
    public List<Movie> GetAllMovieSortByYear();
    public List<GetMovieDetailDto> GetMovieDetails();
    public List<Movie> GetAllMovieMaxTime();
    public List<Movie> GetAllMovieByTeather();
    public List<GetMoviePriceByTicket> GetMoviePriceByTickets();
    public List<Movie> GetMoviesAvgDuration();
    public List<Movie> GetAllMovieAvgPrice();
}
