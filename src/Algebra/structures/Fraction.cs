namespace CryptoS.Algebra
{
    /// <summary>
    /// Fraction class.
    /// </summary>
    public class Fraction
    {
        /// <summary>
        /// Numerator of fraction.
        /// </summary>
        /// <remarks>
        /// Numerator is any integer.
        /// </remarks>
        public int Numerator { get; set; }
        /// <summary>
        /// Denominator of fraction.
        /// </summary>
        /// <remarks>
        /// Denominator is a positive integer.
        /// </remarks>
        public int Denominator { get; set; }
        /// <summary>
        /// Constructor from numerator and denominator.
        /// </summary>
        /// <param name="numerator"></param>
        /// <param name="denominator"></param>
        /// <exception cref="Exception"></exception>
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
                Numerator = numerator / d;
                Denominator = denominator / d;
            }
            else
            {
                Numerator = numerator / (-d);
                Denominator = denominator / (-d);
            }
        }
        /// <summary>
        /// Operator for adding two fractions.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Fraction operator+(Fraction left, Fraction right)
        {
            int numerator = left.Numerator * right.Denominator + left.Denominator * right.Numerator;
            int denominator = left.Denominator * right.Denominator;
            return new Fraction(numerator, denominator);
        }
        /// <summary>
        /// Operator for subtracting two fractions.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Fraction operator-(Fraction left, Fraction right)
        {
            int numerator = left.Numerator * right.Denominator - left.Denominator * right.Numerator;
            int denominator = left.Denominator * right.Denominator;
            return new Fraction(numerator, denominator);
        }
        /// <summary>
        /// Operator for multiplying two fractions.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Fraction operator*(Fraction left, Fraction right)
        {
            return new Fraction(left.Numerator * right.Numerator, left.Denominator * right.Denominator);
        }
        /// <summary>
        /// Operator for verifying equality of two fractions.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator==(Fraction left, Fraction right)
        {
            return (left.Numerator == right.Numerator) && (left.Denominator == right.Denominator);   
        }
        /// <summary>
        /// Operator for verifying inequality of two fractions.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator!=(Fraction left, Fraction right)
        {
            return (left.Numerator != right.Numerator) || (left.Denominator != right.Denominator);
        }
        /// <summary>
        /// Verify equality with other fractions.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            Fraction other = (Fraction)obj;
            return (Numerator == other.Numerator) && (Denominator == other.Denominator);
        }
        /// <summary>
        /// Get hash value of fraction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Numerator * Denominator;
        }
        /// <summary>
        /// Convert fraction to string.
        /// </summary>
        /// <returns></returns>
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
        private int LCM(int a, int b)
        {
            return a * b / GCD(a, b);
        }
    }
}