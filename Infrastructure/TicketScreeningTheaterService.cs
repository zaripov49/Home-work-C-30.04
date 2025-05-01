using Domain;
using Npgsql;

namespace Infrastructure;

public class TicketScreeningTheaterService
{
    List<TicketScreeningTheaters> ticketScreeningTheaters = new List<TicketScreeningTheaters>();
    string connString = "Server=localhost;Database=movie_db;User Id=postgres;Password=12345";

    public List<TicketScreeningTheaterService> GetAllTicketScreeningTheaters(int theater_id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connString))
        {
            connection.Open();

            string cmd = $@"Select t.id, t.screening_id, t.customer_name, t.seat_number, t.price, s.screening_time, th.name
            from tickets as t
            Join screenings as s on s.id = t.screening_id
            Join theaters as th on s.theater_id = th.id
            Where th.id = {theater_id}";
            NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    TicketScreeningTheaters ticketScreeningTheater = new TicketScreeningTheaters()
                    {
                        Id = reader.GetInt32(0),
                        Screening_id = reader.GetInt32(1),
                        Customer_name = reader.GetString(2),
                        Seat_number = reader.GetString(3),
                        Price = reader.GetDecimal(4),
                        Screening_Time = reader.GetDateTime(5),
                        TheaterName = reader.GetString(6),
                    };
                    ticketScreeningTheaters.Add(ticketScreeningTheater);
                }
                return ticketScreeningTheaters;
            }
        }
    }
}
