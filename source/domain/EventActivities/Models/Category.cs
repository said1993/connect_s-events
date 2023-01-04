namespace connect_s_events_domain.EventActivities.Models;
public sealed class Category : Entity
{
    public Category(Guid id, string name) : base(id)
    {
        Name = name;
    }
    public string Name { get; init; }
}