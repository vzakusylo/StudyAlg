using System.Collections.Generic;

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

        public static Option<T> FirstOrNone<T>(this IEnumerable<T> sequence, Func<T, bool> predicate) =>
            sequence.Where(predicate).FirstOrNone();
    }
}
