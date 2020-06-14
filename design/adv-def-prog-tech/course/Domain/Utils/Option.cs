using System.Diagnostics.CodeAnalysis;

namespace System.Linq
{
    public abstract class Option<T> : IEquatable<Option<T>>, IEquatable<T>
    {
        public abstract T Reduce(Func<T> whenNone);

        public abstract T Reduce(T whenNone);

        public abstract Option<TNew> Map<TNew>(Func<T, TNew> mapping);

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
            public override T Reduce(T whenNone) => this.Content;

            public override Option<TNew> Map<TNew>(Func<T, TNew> mapping)
                => new SomeImpl<TNew>(mapping(this.Content));
        }

        private class NoneImpl<T> : Option<T>
        {
            public override T Reduce(Func<T> whenNone) => whenNone();
            public override T Reduce(T whenNone) => whenNone;

            public override Option<TNew> Map<TNew>(Func<T, TNew> mapping)
                => new NoneImpl<TNew>();
        }
    }
}
