namespace connect_s_events_domain.EventActivities.Models;
public class Participant : ValueObject
{
    private Participant(Guid userId, Guid eventActivityId, string email, string name, DateTimeOffset participatedAt)
    {
        UserId = userId;
        EventActivityId = eventActivityId;
        Email = email;
        Name = name;
        ParticipatedAt = participatedAt;
    }

    public Guid UserId { get; init; }
    public Guid EventActivityId { get; init; }
    public string Email { get; set; }
    public string Name { get; set; }
    public DateTimeOffset ParticipatedAt { get; set; }

    public static Participant Create(Guid userId, Guid eventActivityId, string email, string name)
    {
        return new Participant(userId, eventActivityId, email, name, DateTimeOffset.UtcNow);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return new { EventActivityId, UserId, Email, Name };
    }
}
