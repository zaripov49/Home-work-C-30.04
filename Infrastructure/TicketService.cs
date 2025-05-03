using Domain;
using Npgsql;

namespace Infrastructure;

public class TicketService : ITicketService
{
    List<Ticket> tickets = new List<Ticket>();
    string connString = "Server=localhost;Database=movie_db;User Id=postgres;Password=12345";

    public void AddTicket(Ticket ticket)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"INSERT INTO tickets (screening_id, customer_name, seat_number, price) VALUES
            (1, 'Ivan Petrov', 'A1', 10.00),
            (2, 'Maria Ivanova', 'B2', 12.50),
            (3, 'Alexey Sidorov', 'C3', 11.00),
            (4, 'Elena Smirnova', 'D4', 9.50),
            (5, 'Sergey Kuznetsov', 'E5', 10.50),
            (6, 'Natalia Volkova', 'F6', 13.00),
            (7, 'Dmitry Orlov', 'G7', 12.00),
            (8, 'Olga Morozova', 'H8', 11.50),
            (9, 'Pavel Fedorov', 'I9', 10.75),
            (10, 'Svetlana Nikitina', 'J10', 14.00);";

            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            System.Console.WriteLine(command.ExecuteNonQuery());
        }
    }

    public List<Ticket> GetAllTickets()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = "Select * from tickets";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Ticket ticket = new Ticket()
                    {
                        Id = reader.GetInt32(0),
                        ScreeningId = reader.GetInt32(1),
                        CustomerName = reader.GetString(2),
                        SeatNumber = reader.GetString(3),
                        Price = reader.GetInt32(4),
                    };
                    tickets.Add(ticket);
                }
                return tickets;
            }
        }
    }

    public void UpdateTicket(Ticket ticket)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $"Update tickets set Screening_id = {ticket.ScreeningId}, Customer_name = {ticket.CustomerName}, Seat_number = {ticket.SeatNumber}, Price = {ticket.Price}";
            NpgsqlCommand command = new NpgsqlCommand();
            System.Console.WriteLine(command.ExecuteNonQuery());
        }
    }

    public void DeleteTicket(int Id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $"Delete from tickets where id = {Id}";
            NpgsqlCommand command = new NpgsqlCommand();
            System.Console.WriteLine(command.ExecuteNonQuery());
        }
    }

    public List<Ticket> GetCountTicketMovie()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"Select m.title, count(t.id) from screening as s
            Join movies as m on s.movie_id = m.id
            Join tickets as t on t.screening_id = s.id
            Group by m.title";
            NpgsqlCommand command = new NpgsqlCommand();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Ticket ticket = new Ticket()
                    {
                        Id = reader.GetInt32(0),
                        ScreeningId = reader.GetInt32(1),
                        CustomerName = reader.GetString(2),
                        SeatNumber = reader.GetString(3),
                        Price = reader.GetInt32(4),
                    };
                    tickets.Add(ticket);
                }
                return tickets;
            }
        }
    }

    public List<Ticket> GetAllTicketByMovie(int movie_id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"Select t.id, t.screening_id, t.customer_name, t.seat_number, t.price
            from screenings as s
            JOIN tickets as t on s.id = t.screening_id
            JOIN movies as m on s.movie_id = m.id
            where m.id = {movie_id}";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Ticket ticket = new Ticket()
                    {
                        Id = reader.GetInt32(0),
                        ScreeningId = reader.GetInt32(1),
                        CustomerName = reader.GetString(2),
                        SeatNumber = reader.GetString(3),
                        Price = reader.GetInt32(4),
                    };
                    tickets.Add(ticket);
                }
                return tickets;
            }
        }
    }

    public List<Ticket> GetAllTicketByScreening()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"Select s.id, count(t.id)
            from tickets as t on s.id = t.screening_id
            Join screenings as s on s.id = t.screening_id
            Group by t.screening_id, s.id";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Ticket ticket = new Ticket()
                    {
                        Id = reader.GetInt32(0),
                        ScreeningId = reader.GetInt32(1),
                        CustomerName = reader.GetString(2),
                        SeatNumber = reader.GetString(3),
                        Price = reader.GetInt32(4),
                    };
                    tickets.Add(ticket);
                }
                return tickets;
            }
        }
    }

    public List<Ticket> GetAllTicketByMovieNameAndTeaterName()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"Select t.id, t.screening_id, t.customer_name, t.seat_number, t.price, m.title, th.name
                            from screenings as s
                            JOIN tickets as t on s.id = t.screening_id
                            JOIN movies as m on s.movie_id = m.id ";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Ticket ticket = new Ticket()
                    {
                        Id = reader.GetInt32(0),
                        ScreeningId = reader.GetInt32(1),
                        CustomerName = reader.GetString(2),
                        SeatNumber = reader.GetString(3),
                        Price = reader.GetInt32(4),
                    };
                    tickets.Add(ticket);
                }
                return tickets;
            }
        }
    }
}
