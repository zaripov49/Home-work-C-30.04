namespace Domain;

public class TicketScreeningTheaters
{
    public int Id { get; set; }
    public int Screening_id { get; set; }
    public string? Customer_name { get; set; }
    public string? Seat_number { get; set; }
    public decimal Price { get; set; }
    public DateTime Screening_Time { get; set; }
    public string? TheaterName { get; set; }
}
