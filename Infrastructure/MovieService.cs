using Domain;
using Domain.Dtos;
using Npgsql;

namespace Infrastructure;

public class MovieService : IMovieService
{
    List<Movie> movies = new List<Movie>();
    string connString = "Server=localhost;Database=movie_db;User Id=postgres;Password=12345";

    public void AddMovie(Movie movie)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"INSERT INTO movies (title, director, year, duration, genre, description) VALUES
            ('The Shawshank Redemption', 'Frank Darabont', 1994, 142, 'Drama', 'История о надежде и дружбе в тюрьме.'),
            ('The Godfather', 'Francis Ford Coppola', 1972, 175, 'Crime', 'Эпическая сага о мафиозном клане.'),
            ('The Dark Knight', 'Christopher Nolan', 2008, 152, 'Action', 'Бэтмен против безумного Джокера.'),
            ('Pulp Fiction', 'Quentin Tarantino', 1994, 154, 'Crime', 'Переплетение историй о жизни преступников.'),
            ('Forrest Gump', 'Robert Zemeckis', 1994, 142, 'Drama', 'Невероятное путешествие простодушного мужчины.'),
            ('Inception', 'Christopher Nolan', 2010, 148, 'Sci-Fi', 'Крадущийся в снах преступник с уникальной миссией.'),
            ('Fight Club', 'David Fincher', 1999, 139, 'Drama', 'Подпольный бойцовский клуб как способ освобождения.'),
            ('The Matrix', 'Lana Wachowski, Lilly Wachowski', 1999, 136, 'Sci-Fi', 'Хакер открывает для себя реальность, отличную от привычной.'),
            ('Goodfellas', 'Martin Scorsese', 1990, 146, 'Crime', 'История взлёта и падения мафиозного соучастника.'),
            ('Interstellar', 'Christopher Nolan', 2014, 169, 'Sci-Fi', 'Группа исследователей отправляется в космическое путешествие.');";

            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            System.Console.WriteLine(command.ExecuteNonQuery());
        }
    }

    public List<Movie> GetAllMovies()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = "Select * from movies";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Movie movie = new Movie()
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Director = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        Duration = reader.GetInt32(4),
                        Genre = reader.GetString(5),
                        Description = reader.GetString(6),
                    };
                    movies.Add(movie);
                }
                return movies;
            }
        }
    }

    public void UpdateMovie(Movie movie)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $"Update movies set title = {movie.Title}, director = {movie.Director}, year = {movie.Year}, duration = {movie.Duration}, genre = {movie.Genre}, description = {movie.Description}";
            NpgsqlCommand command = new NpgsqlCommand();
            System.Console.WriteLine(command.ExecuteNonQuery());
        }
    }

    public void DeleteMovie(int Id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $"Delete from movies where id = {Id}";
            NpgsqlCommand command = new NpgsqlCommand();
            System.Console.WriteLine(command.ExecuteNonQuery());
        }
    }

    public List<Movie> GetAllMoviesByGenre(string genre)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $"Select * from movies where genre = {genre}";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Movie movie = new Movie()
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Director = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        Duration = reader.GetInt32(4),
                        Genre = reader.GetString(5),
                        Description = reader.GetString(6),
                    };
                    movies.Add(movie);
                }
                return movies;
            }
        }
    }

    public List<Movie> GetAllMovieDirector()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $"Select Distinct(director) from movies";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Movie movie = new Movie()
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Director = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        Duration = reader.GetInt32(4),
                        Genre = reader.GetString(5),
                        Description = reader.GetString(6),
                    };
                    movies.Add(movie);
                }
                return movies;
            }
        }
    }

    public List<Movie> GetAllMovieSortByYear()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $"Select * from movies order by year desc";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Movie movie = new Movie()
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Director = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        Duration = reader.GetInt32(4),
                        Genre = reader.GetString(5),
                        Description = reader.GetString(6),
                    };
                    movies.Add(movie);
                }
                return movies;
            }
        }
    }

    public List<GetMovieDetailDto> GetMovieDetails()
    {
        List<GetMovieDetailDto> getMovieDetailDtos = new List<GetMovieDetailDto>();
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"Select m.title, s.screening_time, th.name
            from screenings as s
            JOIN movies as m on s.movie_id = m.id
            JOIN theaters as th on s.theater_id = th.id";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    GetMovieDetailDto getMovieDetailDto = new GetMovieDetailDto()
                    {
                        MovieName = reader.GetString(0),
                        ScreeningTime = reader.GetDateTime(1),
                        TheaterName = reader.GetString(2),
                    };
                    getMovieDetailDtos.Add(getMovieDetailDto);
                }
                return getMovieDetailDtos;
            }
        }
    }

    public List<Movie> GetAllMovieMaxTime()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $"Select * from movies where duration = (Select max(duration) from movies)";
            NpgsqlCommand command = new NpgsqlCommand();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Movie movie = new Movie()
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Director = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        Duration = reader.GetInt32(4),
                        Genre = reader.GetString(5),
                        Description = reader.GetString(6),
                    };
                    movies.Add(movie);
                }
                return movies;
            }
        }
    }

    public List<Movie> GetAllMovieByTeather()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"Select m.title, m.director, m.year, m.duration, m.genre, m.description
                            from screenings as s
                            JOIN movies as m on s.movie_id = m.id
                            Group by m.title, m.director, m.year, m.duration, m.genre, m.description, s.movie_id
                            having count(*) > 1";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Movie movie = new Movie()
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Director = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        Duration = reader.GetInt32(4),
                        Genre = reader.GetString(5),
                        Description = reader.GetString(6),
                    };
                    movies.Add(movie);
                }
                return movies;
            }
        }
    }

    public List<GetMoviePriceByTicket> GetMoviePriceByTickets()
    {
        List<GetMoviePriceByTicket> getMoviePriceByTickets = new List<GetMoviePriceByTicket>();
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"Select m.title, sum(t.price) 
                            from screenings as s
                            Join movies as m on s.movie_id = m.id
                            JOIN tickets as t on s.id = t.screening_id";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    GetMoviePriceByTicket getMoviePriceByTicket = new GetMoviePriceByTicket()
                    {
                        MovieName = reader.GetString(0),
                        PriceTicket = reader.GetDecimal(1),
                    };
                    getMoviePriceByTickets.Add(getMoviePriceByTicket);
                }
                return getMoviePriceByTickets;
            }
        }
    }

    public List<Movie> GetMoviesAvgDuration()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"Select * from movies
                            where duration > (
                                select avg(duration) from movies
                                )";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Movie movie = new Movie()
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Director = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        Duration = reader.GetInt32(4),
                        Genre = reader.GetString(5),
                        Description = reader.GetString(6),
                    };
                    movies.Add(movie);
                }
                return movies;
            }
        }
    }

    public List<Movie> GetAllMovieAvgPrice()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"Select m.title, m.director, m.year, m.duration, m.genre, m.description, avg(t.price)
                            from screenings as s
                            JOIN movies as m on s.movie_id = m.id
                            Join tickets as t on t.screening_id = s.id
                            Group by m.title, m.director, m.year, m.duration, m.genre, m.description
                            ";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Movie movie = new Movie()
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Director = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        Duration = reader.GetInt32(4),
                        Genre = reader.GetString(5),
                        Description = reader.GetString(6),
                    };
                    movies.Add(movie);
                }
                return movies;
            }
        }
    }
}