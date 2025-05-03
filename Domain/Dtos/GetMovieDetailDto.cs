namespace Domain.Dtos;

public class GetMovieDetailDto
{
    public string? MovieName { get; set; }
    public DateTime ScreeningTime { get; set; }
    public string? TheaterName { get; set; }
}
