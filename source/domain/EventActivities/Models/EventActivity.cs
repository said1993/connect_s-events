namespace connect_s_events_domain.EventActivities.Models;
public sealed class EventActivity : AggregateRoot
{
    public static EventActivity Create(string name, string description, Guid owner, string ownerEmail, DateTimeOffset beginAt, DateTimeOffset endAt)
    {
        return new EventActivity(Guid.NewGuid(), description, DateTimeOffset.UtcNow,
        beginAt, endAt, name,
        owner, ownerEmail);
    }
    public EventActivity(Guid id, string description, DateTimeOffset createdAt,
        DateTimeOffset beginAt, DateTimeOffset endAt, string name,
        Guid owner, string ownerEmail)
     : base(id)
    {
        Name = name;
        Description = description;
        Owner = owner;
        OwnerEmail = ownerEmail;
        CreatedAt = createdAt;
        BeginAt = beginAt;
        EndAt = endAt;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public Guid Owner { get; set; }
    public string OwnerEmail { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset BeginAt { get; set; }
    public DateTimeOffset EndAt { get; set; }

    public IEnumerable<Participant>? Participants { get; set; }
    public IEnumerable<ActivityCategory>? ActivityCategories { get; set; }

}
