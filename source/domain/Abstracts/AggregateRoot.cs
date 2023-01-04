namespace connect_s_events_domain.Abstracts;
public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id) : base(id) { }
}
