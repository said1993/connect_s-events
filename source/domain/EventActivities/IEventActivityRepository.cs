using connect_s_events_domain.EventActivities.Models;

namespace connect_s_events_domain.EventActivities;
public interface IEventActivityRepository
{
    public Task Add(EventActivity entity);
    public Task<IEnumerable<EventActivity>> GetAll();
    public Task AddParticipant(Participant entity);
    Task Delete(Guid eventActivityId, CancellationToken cancellationToken);
}
