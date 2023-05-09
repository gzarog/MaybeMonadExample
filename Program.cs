public class Program
{
    public static void Main(string[] args)
    {
        int numerator = 10;
        int denominator = 2;

        Maybe<int> result = Divide(numerator, denominator);

        result.Bind<int>(value =>
        {
            Console.WriteLine($"The result of {numerator} / {denominator} is {value}");
            return Maybe<int>.Just(value);
        });

        Console.ReadLine();
    }

    public static Maybe<int> Divide(int numerator, int denominator)
    {
        return denominator == 0 ? Maybe<int>.Nothing : Maybe<int>.Just(numerator / denominator);
    }

    public abstract class Maybe<T>
    {
        public abstract Maybe<U> Bind<U>(Func<T, Maybe<U>> f);
        public static Maybe<T> Just(T value) => new Just<T>(value);
        public static Maybe<T> Nothing => new Nothing<T>();
    }

    public sealed class Just<T> : Maybe<T>
    {
        public T Value { get; }

        public Just(T value)
        {
            Value = value;
        }

        public override Maybe<U> Bind<U>(Func<T, Maybe<U>> f)
        {
            return f(Value);
        }
    }

    public sealed class Nothing<T> : Maybe<T>
    {
        public override Maybe<U> Bind<U>(Func<T, Maybe<U>> f)
        {
            return Maybe<U>.Nothing;
        }
    }
}
