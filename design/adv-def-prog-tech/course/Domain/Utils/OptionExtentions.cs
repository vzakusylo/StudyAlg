using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Linq
{
    public static class OptionExtentions
    {
        public static Option<T> FirstOrNone<T>(this IEnumerable<T> sequence)
        {
            using IEnumerator<T> enumerator = sequence.Take(1).ToList().GetEnumerator();

            if (!enumerator.MoveNext())
                return Option.None<T>();
            return Option.Some(enumerator.Current);
        }
    }

    public abstract class Option<T> : IEquatable<Option<T>>, IEquatable<T>
    {
        public abstract T Reduce(Func<T> whenNone);

        public bool Equals([AllowNull] Option<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Equals([AllowNull] T other)
        {
            throw new NotImplementedException();
        }
    }

    public static class Option
    {
        public static Option<T> Some<T>(T value) => new SomeImpl<T>(value);

        public static Option<T> None<T>() => new NoneImpl<T>();

        private class SomeImpl<T> : Option<T>
        {
            private T Content { get; }

            public SomeImpl(T content)
            {
                Content = content;
            }

            public override T Reduce(Func<T> whenNone) => this.Content;
            
        }

        private class NoneImpl<T> : Option<T>
        {
            public override T Reduce(Func<T> whenNone) => whenNone();
        }
    }
}
