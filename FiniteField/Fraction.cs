namespace FiniteFieldService
{
    public class Fraction
    {
        public int numerator { get; set; }
        public int denominator { get; set; }
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
                this.numerator = numerator / d;
                this.denominator = denominator / d;
            }
            else
            {
                this.numerator = numerator / (-d);
                this.denominator = denominator / (-d);
            }
        }
        public static Fraction operator+(Fraction left, Fraction right)
        {
            int numerator = left.numerator * right.denominator + left.denominator * right.numerator;
            int denominator = left.denominator * right.denominator;
            return new Fraction(numerator, denominator);
        }
        public static bool operator==(Fraction left, Fraction right)
        {
            return (left.numerator == right.numerator) && (left.denominator == right.denominator);   
        }
        public static bool operator!=(Fraction left, Fraction right)
        {
            return (left.numerator != right.numerator) || (left.denominator != right.denominator);
        }
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            Fraction other = (Fraction)obj;
            return (numerator == other.numerator) && (denominator == other.denominator);
        }
        public override int GetHashCode()
        {
            return numerator * denominator;
        }
        public override string ToString()
        {
            return $"{numerator}/{denominator}";
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