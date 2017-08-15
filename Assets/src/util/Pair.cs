using System;
using System.Collections.Generic;

/// <summary>
/// Unity uses .net framework 3.5 -> has no Tuple class -> veloceepaedim!
/// </summary>
public sealed class Pair<T1, T2> : IEquatable<Pair<T1, T2>>
{
    private T1 _first;
    private T2 _second;

    public Pair(T1 first, T2 second)
    {
        _first = first;
        _second = second;
    }

    public T1 First
    {
        get { return _first; }
    }

    public T2 Second
    {
        get { return _second; }
    }

    public bool Equals(Pair<T1, T2> other)
    {
        if (other == null)
        {
            return false;
        }

        return EqualityComparer<T1>.Default.Equals(this.First, other.First) &&
               EqualityComparer<T2>.Default.Equals(this.Second, other.Second);
    }

    public override bool Equals(object o)
    {
        return Equals(o as Pair<T1, T2>);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<T1>.Default.GetHashCode(_first) * 37 +
               EqualityComparer<T2>.Default.GetHashCode(_second);
    }
}
