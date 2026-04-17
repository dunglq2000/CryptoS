namespace Algebra
{
    public class Fraction
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }
        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new Exception("Denominator can not equal to 0");
            }
            int d = GCD(numerator, denominator);
            if (d < 0) d = -d;
            if (denominator > 0)
            {
                this.Numerator = numerator / d;
                this.Denominator = denominator / d;
            }
            else
            {
                this.Numerator = numerator / (-d);
                this.Denominator = denominator / (-d);
            }
        }
        public static Fraction operator+(Fraction left, Fraction right)
        {
            int numerator = left.Numerator * right.Denominator + left.Denominator * right.Numerator;
            int denominator = left.Denominator * right.Denominator;
            return new Fraction(numerator, denominator);
        }
        public static bool operator==(Fraction left, Fraction right)
        {
            return (left.Numerator == right.Numerator) && (left.Denominator == right.Denominator);   
        }
        public static bool operator!=(Fraction left, Fraction right)
        {
            return (left.Numerator != right.Numerator) || (left.Denominator != right.Denominator);
        }
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            Fraction other = (Fraction)obj;
            return (Numerator == other.Numerator) && (Denominator == other.Denominator);
        }
        public override int GetHashCode()
        {
            return Numerator * Denominator;
        }
        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }
        private int GCD(int a, int b)
        {
            while (b != 0)
            {
                (a, b) = (b, a % b);
            }
            return a;
        }
    }
}