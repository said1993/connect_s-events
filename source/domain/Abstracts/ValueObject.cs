namespace connect_s_events_domain.Abstracts;
public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetAtomicValues();
    public bool Equals(ValueObject? other) => other is not null && ValuesAreEquals(other);
    public override bool Equals(object? obj) => obj is ValueObject other && ValuesAreEquals(other);
    private bool ValuesAreEquals(ValueObject other) => GetAtomicValues().SequenceEqual(other.GetAtomicValues());

    public override int GetHashCode() => GetAtomicValues().Aggregate(default(int), HashCode.Combine);
}
