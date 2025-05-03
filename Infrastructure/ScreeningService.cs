using Domain;
using Npgsql;

namespace Infrastructure;

public class ScreeningService : IScreeningService
{
    List<Screening> screenings = new List<Screening>();
    string connString = "Server=localhost;Database=movie_db;User Id=postgres;Password=12345";

    public void AddScreening(Screening screening)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"INSERT INTO screenings (modAddScreening, theater_id, screening_time, ticket_price, available_seats) VALUES
            (1, 1, '2025-03-15 18:00:00', 10.00, 150),
            (2, 2, '2025-03-15 20:00:00', 12.50, 200),
            (3, 3, '2025-03-16 19:30:00', 11.00, 250),
            (4, 4, '2025-03-16 21:00:00', 9.50, 180),
            (5, 5, '2025-03-17 17:00:00', 10.50, 220),
            (6, 6, '2025-03-17 20:30:00', 13.00, 160),
            (7, 7, '2025-03-18 18:45:00', 12.00, 300),
            (8, 8, '2025-03-18 19:00:00', 11.50, 190),
            (9, 9, '2025-03-19 20:15:00', 10.75, 210),
            (10, 10, '2025-03-19 21:30:00', 14.00, 170);";

            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            System.Console.WriteLine(command.ExecuteNonQuery());
        }
    }

    public List<Screening> GetAllScreenings()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = "Select * from screenings";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Screening screening = new Screening()
                    {
                        Id = reader.GetInt32(0),
                        MovieId = reader.GetInt32(1),
                        TheaterId = reader.GetInt32(2),
                        ScreeningTime = reader.GetDateTime(3),
                        TicketPrice = reader.GetDecimal(4),
                        AvailableSeats = reader.GetInt32(5),
                    };
                    screenings.Add(screening);
                }
                return screenings;
            }
        }
    }

    public void UpdateScreening(Screening screening)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $"Update screenings set Movie_id = {screening.MovieId}, Theater_id = {screening.TheaterId}, Screening_time = {screening.ScreeningTime}, Ticket_price = {screening.TicketPrice}, Available_seats = {screening.AvailableSeats}";
            NpgsqlCommand command = new NpgsqlCommand();
            System.Console.WriteLine(command.ExecuteNonQuery());
        }
    }

    public void DeleteScreening(int Id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $"Delete from screenings where id = {Id}";
            NpgsqlCommand command = new NpgsqlCommand();
            System.Console.WriteLine(command.ExecuteNonQuery());
        }
    }

    public List<Screening> GetAllScreeningSortByDate()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $"Select * from screenings order by screening_time";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Screening screening = new Screening()
                    {
                        Id = reader.GetInt32(0),
                        MovieId = reader.GetInt32(1),
                        TheaterId = reader.GetInt32(2),
                        ScreeningTime = reader.GetDateTime(3),
                        TicketPrice = reader.GetDecimal(4),
                        AvailableSeats = reader.GetInt32(5),
                    };
                    screenings.Add(screening);
                }
                return screenings;
            }
        }
    }

    public List<Screening> GetAllFiveScreenings()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $"Select * from screenings Limit 5";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Screening screening = new Screening()
                    {
                        Id = reader.GetInt32(0),
                        MovieId = reader.GetInt32(1),
                        TheaterId = reader.GetInt32(2),
                        ScreeningTime = reader.GetDateTime(3),
                        TicketPrice = reader.GetDecimal(4),
                        AvailableSeats = reader.GetInt32(5),
                    };
                    screenings.Add(screening);
                }
                return screenings;
            }
        }
    }

    public List<Screening> GetAllCountScreeningsByMovies()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $"Select movie_id, count(*) from screenings Group by movie_id";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Screening screening = new Screening()
                    {
                        Id = reader.GetInt32(0),
                        MovieId = reader.GetInt32(1),
                        TheaterId = reader.GetInt32(2),
                        ScreeningTime = reader.GetDateTime(3),
                        TicketPrice = reader.GetDecimal(4),
                        AvailableSeats = reader.GetInt32(5),
                    };
                    screenings.Add(screening);
                }
                return screenings;
            }
        }
    }

    public List<Screening> GetAllScreeningAvgTicket()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"Select * from screenings
                            where ticket_price > (
                                select avg(ticket_price) from screenings
                            )";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Screening screening = new Screening()
                    {
                        Id = reader.GetInt32(0),
                        MovieId = reader.GetInt32(1),
                        TheaterId = reader.GetInt32(2),
                        ScreeningTime = reader.GetDateTime(3),
                        TicketPrice = reader.GetDecimal(4),
                        AvailableSeats = reader.GetInt32(5),
                    };
                    screenings.Add(screening);
                }
                return screenings;
            }
        }
    }
}
