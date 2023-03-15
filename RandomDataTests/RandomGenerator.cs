using System.Text;

namespace RandomDataTests
{
    public abstract class RandomGenerator
    {
        public abstract string GetValue();
    }

    public class RandomEmptyGenerator : RandomGenerator
    {
        public override string GetValue() => string.Empty;
    }

    public class RandomIntegerGenerator : RandomGenerator
    {
        private readonly int MinValue;
        private readonly int MaxValue;

        public RandomIntegerGenerator(int minValue = 0, int maxValue = int.MaxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public override string GetValue()
        {
            int value = Random.Shared.Next(MinValue, MaxValue);
            return value.ToString();
        }
    }

    public class RandomFloatGenerator : RandomGenerator
    {
        private readonly double MinValue;
        private readonly double MaxValue;

        public RandomFloatGenerator(double minValue = 0.0, double maxValue = 99999.99)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public override string GetValue()
        {
            double value = Random.Shared.NextDouble() * (MaxValue - MinValue) + MinValue;
            return value.ToString("0.###");
        }
    }

    public class RandomStringGenerator : RandomGenerator
    {
        private const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Lower = "abcdefghijklmnopqrstuvwxyz";
        private const string Digits = "1234567890";
        private const string Punctuation = "!@#$%^&*()-=_={}[]|\\:;\"'<>,.?/";

        private readonly int MaxLength;
        private readonly bool MakeWords;
        private readonly bool Multiline;

        private readonly string CharString;

        public RandomStringGenerator(CharType charType = CharType.Alphanumeric, int maxLength = 40, bool makeWords = false, bool multiline = false)
        {
            if (maxLength < 0)
                throw new ArgumentException("Cannot be a negative number", nameof(maxLength));

            MaxLength = maxLength;
            MakeWords = makeWords;
            Multiline = multiline;

            StringBuilder builder = new();
            if (charType.HasFlag(CharType.Upper))
                builder.Append(Upper);
            if (charType.HasFlag(CharType.Lower))
                builder.Append(Lower);
            if (charType.HasFlag(CharType.Digits))
                builder.Append(Digits);
            if (charType.HasFlag(CharType.Punctuation))
                builder.Append(Punctuation);
            if (builder.Length == 0)
                builder.Append('*');
            CharString = builder.ToString();
        }

        public override string GetValue()
        {
            int length = Random.Shared.Next(1, MaxLength);

            StringBuilder builder = new();

            for (int i = 0; i < length; i++)
                builder.Append(CharString[Random.Shared.Next(CharString.Length)]);

            if (MakeWords)
            {
                for (int i = Random.Shared.Next(24); i < builder.Length; i += Random.Shared.Next(1, 24))
                    builder.Insert(i, ' ');
            }

            if (Multiline)
            {
                for (int i = Random.Shared.Next(250); i < builder.Length; i += Random.Shared.Next(25, 250))
                    builder.Insert(i, Environment.NewLine);
            }

            return builder.ToString();
        }
    }

    public class RandomDateGenerator : RandomGenerator
    {
        private readonly DateTime RangeStart;
        private readonly double RangeMinutes;

        public RandomDateGenerator()
        {
            RangeStart = new DateTime(1900, 1, 1);
            RangeMinutes = (DateTime.Now - RangeStart).TotalMinutes;
        }

        public override string GetValue()
        {
            DateTime value = RangeStart.AddMinutes(Random.Shared.NextDouble() * RangeMinutes);
            return value.ToString();
        }
    }

    public class RandomGuidGenerator : RandomGenerator
    {
        public override string GetValue()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
