namespace connect_s_events_domain.EventActivities.Models;
public class ActivityCategory : ValueObject
{
    public ActivityCategory(Guid eventActivityId, Guid categoryId)
    {
        EventActivityId = eventActivityId;
        CategoryId = categoryId;
    }

    public Guid EventActivityId { get; }
    public Guid CategoryId { get; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return new { EventActivityId, CategoryId };
    }
}
