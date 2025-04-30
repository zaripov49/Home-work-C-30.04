using Domain;

namespace Infrastructure;

public interface ITicketService
{
    public void AddTicket(Ticket ticket);
    public List<Ticket> GetAllTickets();
    public void UpdateTicket(Ticket ticket);
    public void DeleteTicket(int Id);
}
