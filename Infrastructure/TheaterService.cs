using Domain;
using Npgsql;

namespace Infrastructure;

public class TheaterService : ITheaterService
{
    List<Theater> theaters = new List<Theater>();
    string connString = "Server=localhost;Database=movie_db;User Id=postgres;Password=12345";

    public void AddTheater(Theater theater)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"INSERT INTO theaters (name, location, manager, phone, capacity) VALUES
            ('Cineplex 1', 'Душанбе', 'Ali Rahimov', '+9921234567', 150),
            ('Cinema Star', 'Худжанд', 'Leyla Karimova', '+9922345678', 200),
            ('Movie Palace', 'Бухара', 'Rashid Mirza', '+9923456789', 250),
            ('Golden Screen', 'Самарканд', 'Olga Ivanova', '+9924567890', 180),
            ('Film House', 'Ташкент', 'Akmal Saidov', '+998901234567', 220),
            ('Silver Cinema', 'Бишкек', 'Aida Bekova', '+996555123456', 160),
            ('Premiere Theater', 'Алма-Ата', 'Nursultan Nazarbayev', '+77271234567', 300),
            ('Star Movies', 'Душанбе', 'Farhod Saidov', '+9925678901', 190),
            ('Cinema World', 'Худжанд', 'Marina Petrova', '+9926789012', 210),
            ('Epic Screen', 'Душанбе', 'Rustam Aliyev', '+9927890123', 170);";

            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            System.Console.WriteLine(command.ExecuteNonQuery());
        }
    }

    public List<Theater> GetAllTheaters()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = "Select * from theaters";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Theater theater = new Theater()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Location = reader.GetString(2),
                        Manager = reader.GetString(3),
                        Phone = reader.GetString(4),
                        CapaCity = reader.GetInt32(5),
                    };
                    theaters.Add(theater);
                }
                return theaters;
            }
        }
    }

    public void UpdateTheater(Theater theater)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $"Update theaters set name = {theater.Name}, location = {theater.Location}, manager = {theater.Manager}, phone = {theater.Phone}, Capacity = {theater.CapaCity}";
            NpgsqlCommand command = new NpgsqlCommand();
            System.Console.WriteLine(command.ExecuteNonQuery());
        }
    }

    public void DeleteTheater(int Id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $"Delete from theaters where id = {Id}";
            NpgsqlCommand command = new NpgsqlCommand();
            System.Console.WriteLine(command.ExecuteNonQuery());
        }
    }

    public List<Theater> GetAllTeatherByScreening(int countScreening)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"Select th.name, th.location, th.manager, th.capacity
                            from screenings as s
                            JOIN theaters as th on s.theater_id = th.id
                            Group by s.theater_id, th.name, th.location, th.manager, th.capacity
                            having count(*) > {countScreening}";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Theater theater = new Theater()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Location = reader.GetString(2),
                        Manager = reader.GetString(3),
                        Phone = reader.GetString(4),
                        CapaCity = reader.GetInt32(5),
                    };
                    theaters.Add(theater);
                }
                return theaters;
            }
        }
    }

    public List<Theater> GetAllTheaterAvgPrice()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"Select th.name, th.location, th.manager, th.capacity, Avg(t.price)
                            from screenings as s
                            JOIN theaters as th on s.theater_id = th.id
                            Join tickets as t on s.screening_id =  t.id
                            Group by s.theater_id, th.name, th.location, th.manager, th.capacity
                            ";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Theater theater = new Theater()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Location = reader.GetString(2),
                        Manager = reader.GetString(3),
                        Phone = reader.GetString(4),
                        CapaCity = reader.GetInt32(5),
                    };
                    theaters.Add(theater);
                }
                return theaters;
            }
        }
    }

    public List<Theater> GetAllTheaterByMovieName(string movieName)
    {
         using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"Select th.name, th.location, th.manager, th.capacity
                            from screenings as s
                            JOIN theaters as th on s.theater_id = th.id
                            Join movies as m on s.movie_id = m.id
                            where s.movie_id = (
                                select id from movies
                                where title = {movieName})
                            ";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Theater theater = new Theater()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Location = reader.GetString(2),
                        Manager = reader.GetString(3),
                        Phone = reader.GetString(4),
                        CapaCity = reader.GetInt32(5),
                    };
                    theaters.Add(theater);
                }
                return theaters;
            }
        }
    }

    public List<Theater> GetAllTheaterByScreening()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"Select th.name, th.location, th.manager, th.capacity, count(s.id)
                            from screenings as s
                            JOIN theaters as th on s.theater_id = th.id
                            Join movies as m on s.movie_id = m.id
                            Group by th.id, th.name, th.location, th.manager, th.capacity
                            ";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Theater theater = new Theater()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Location = reader.GetString(2),
                        Manager = reader.GetString(3),
                        Phone = reader.GetString(4),
                        CapaCity = reader.GetInt32(5),
                    };
                    theaters.Add(theater);
                }
                return theaters;
            }
        }
    }
}
