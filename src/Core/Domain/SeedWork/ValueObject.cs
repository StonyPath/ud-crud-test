using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SeedWork;

public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetAtomicValues();

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != GetType()) return false;

        var other = (ValueObject)obj;
        using var thisValues = GetAtomicValues().GetEnumerator();
        using var otherValues = other.GetAtomicValues().GetEnumerator();
        while (thisValues.MoveNext() && otherValues.MoveNext())
        {
            if (thisValues.Current == null ^ otherValues.Current == null)
                return false;
            if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                return false;
        }
        return !thisValues.MoveNext() && !otherValues.MoveNext();
    }

    public override int GetHashCode() =>
        GetAtomicValues().Select(x => x != null ? x.GetHashCode() : 0)
                         .Aggregate(0, (hash, next) => hash ^ next);
}
