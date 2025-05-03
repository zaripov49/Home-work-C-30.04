using Domain;

namespace Infrastructure;

public interface ITicketService
{
    public void AddTicket(Ticket ticket);
    public List<Ticket> GetAllTickets();
    public void UpdateTicket(Ticket ticket);
    public void DeleteTicket(int Id);
    public List<Ticket> GetCountTicketMovie();
    public List<Ticket> GetAllTicketByMovie(int movie_id);
    public List<Ticket> GetAllTicketByScreening();
    public List<Ticket> GetAllTicketByMovieNameAndTeaterName();
}
